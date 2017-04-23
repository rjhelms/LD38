using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    // Use this for initialization
    void Awake()
    {
        // register ourselves with the camera controller
        FindObjectOfType<CameraController>().RegisterPlayer(GetComponent<Rigidbody2D>());
        FindObjectOfType<GameController>().RegisterPlayer(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
