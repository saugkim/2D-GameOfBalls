using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform playerPosition;
    public float smoothTime = 10F;
    public Vector3 velocity;


    // Use this for initialization
    void Start () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;	
	}
	
	// Update is called once per frame
	void Update () {

        
      //1 x and y direction;   transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y+3.5f, transform.position.z);
      //2 only y direction;    transform.position = new Vector3(transform.position.x, playerPosition.position.y + 3.5f, transform.position.z);


        // homework!
        // try to create a smooth movement for the camera, where camera moves 
        // little bit after the ball and tried to reach to the ball all the time
        // if ball stops, camera smoothlyl goed to the location of the player
        // use vector3 SmootheDamp

        // my solution below Question: velocity???//

        Vector3 targetPosition = new Vector3(transform.position.x, playerPosition.position.y + 1f, transform.position.z);
      
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime*Time.deltaTime) ;

    }
}
    