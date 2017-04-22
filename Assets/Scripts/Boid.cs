using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public bool Logging = false;
    public float ScaleTowardCentre = 0.01f;
    public float MovementForce = 1f;
    public float TurnForce = 1f;
    public float NearbyMaxDistance = 20f;
    public float TimeBetweenPulses = 0.5f;
    public Tribe MyTribe;

    private Rigidbody2D my_rigidbody;
	private float nextPulse;
    private Quaternion target_rotation;

    // Use this for initialization
	void Start () {
        my_rigidbody = GetComponent<Rigidbody2D>();
        MyTribe.Members.Add(this);
        transform.position = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
        nextPulse = Time.fixedTime + Random.Range(0, TimeBetweenPulses);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (nextPulse <= Time.fixedTime)
        {
            Vector2 movement_vector = new Vector2();
            List<Boid> nearby_boids = MyTribe.GetNearbyBoids(this, NearbyMaxDistance);
            movement_vector += TowardCentre();
            movement_vector += Avoid(nearby_boids);
            movement_vector += Align(nearby_boids);

            float sign = Mathf.Sign(my_rigidbody.velocity.x * movement_vector.x - my_rigidbody.velocity.y * movement_vector.x);
            float target_angle = Vector2.Angle(my_rigidbody.velocity, movement_vector) * sign;
            if (Logging)
                Debug.Log(string.Format("{0}: {1}", this, target_angle));

            my_rigidbody.AddRelativeForce(Vector2.right * MovementForce, ForceMode2D.Impulse);
            if (target_angle > 0)
                my_rigidbody.AddTorque(TurnForce, ForceMode2D.Impulse);
            if (target_angle < 0)
                my_rigidbody.AddTorque(-TurnForce, ForceMode2D.Impulse);
            nextPulse += TimeBetweenPulses;
        }



        //movement_vector.Normalize();
        //my_rigidbody.
        //Vector2 movement_force = movement_vector * MovementForce;

        //float angle = Mathf.Atan2(movement_force.y, movement_force.x) * Mathf.Rad2Deg;
        //target_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //nextPulse += TimeBetweenPulses;

        //transform.rotation = Quaternion.Lerp(transform.rotation, target_rotation, 0.1f);
    }

    private Vector2 TowardCentre()
    {
        Vector2 velocity_toward_centre = new Vector2();
        Vector2 centre = MyTribe.GetAveragePosition();
        velocity_toward_centre = centre - (Vector2)transform.position;
        velocity_toward_centre *= ScaleTowardCentre;

        return velocity_toward_centre;
    }

    private Vector2 Avoid(List<Boid> to_avoid)
    {
        Vector2 avoid_vector = new Vector2();
        if (to_avoid.Count > 0)
        {
            foreach (Boid boid in to_avoid)
            {
                avoid_vector -= ((Vector2)boid.transform.position - (Vector2)transform.position);
            }
            return avoid_vector;
        } else
        {
            return Vector2.zero;
        }
    }

    private Vector2 Align(List<Boid> to_align)
    {
        Vector2 align_vector = new Vector2();
        if (to_align.Count > 0)
        {
            foreach (Boid boid in to_align)
            {
                align_vector += boid.GetComponent<Rigidbody2D>().velocity;
            }
            align_vector /= to_align.Count;
            return align_vector;
        } else
        {
            return Vector2.zero;
        }
    }
}
