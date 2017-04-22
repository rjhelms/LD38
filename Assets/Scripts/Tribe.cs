using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tribe : MonoBehaviour {

    public List<Boid> Members;

	// Use this for initialization
	void Start () {
        Members = new List<Boid>();
        Debug.Log(Members.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector2 GetAveragePosition()
    {
        Vector2 average_position = new Vector2();

        foreach (Boid boid in Members)
        {
            average_position += (Vector2)boid.transform.position;
        }

        average_position /= Members.Count;

        return average_position;
    }

    public List<Boid> GetNearbyBoids(Boid target, float max_distance)
    {
        List<Boid> nearby_boids = new List<Boid>();
        foreach (Boid boid in Members)
        {
            if (boid != target)
            {
                Vector2 position_difference = (Vector2)boid.transform.position - (Vector2)target.transform.position;
                if (position_difference.magnitude < max_distance)
                {
                    nearby_boids.Add(boid);
                }
            }
        }
        return nearby_boids;
    }
}
