using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float rotation = 360f;
	public float gravity;
	public float jump = 7f;

	private CharacterController player;
	private float groundDistance;

	float dr = 0f;

	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController> ();
		groundDistance = player.bounds.extents.y;
	}

	// Update is called once per frame
	void Update () {

 // get input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
		//ssVector3 oldLookDir;
        // create our movement vector for player movement, and use it to set our look vector for rotation
        Vector3 movement = new Vector3 (h, gravity, v) * speed * Time.deltaTime;
        Vector3 lookDir = new Vector3 (movement.x, 0f, movement.z);
 
        // determine method of rotation
        if (h != 0 || v != 0) {
 
            // create a smooth direction to look at using Slerp()
            Vector3 smoothDir = Vector3.Slerp(transform.forward, lookDir, speed * Time.deltaTime);
 
            transform.rotation = Quaternion.LookRotation (smoothDir);
 
            // store the current smooth direction to use when the player is not providing input, providing consistency
            lookDir = smoothDir;
        } else {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
 
        // move the player using its CharacterController.Move method
        player.Move (movement);
 
        // Apply our gravity to the player
        ApplyGravity ();
	}

	void Jump() {
		if (isGrounded ()) {
			if (Input.GetButtonDown ("Jump")) {
				Debug.Log ("I was pressed");
				gravity += jump;
			}
		}
	}

	void ApplyGravity() {
		// apply gravity effect
		if (!isGrounded ()) {
			Debug.Log ("I am NOT grounded. Gravity = " + gravity.ToString());
			gravity += (Physics.gravity.y) * Time.deltaTime;
		} else {
			gravity = 0f;
			Debug.Log ("Grounded. Gravity = " + gravity.ToString()); 
		}
	}
		
	bool isGrounded() {
		return Physics.Raycast (transform.position, -Vector3.up, groundDistance + 0.1f);
	}
}

