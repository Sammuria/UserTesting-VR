using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour {

	private float speed;
    private float rotationX;
    private float rotationZ;


    private void Start()
    {
        speed = Random.Range(10f, 20f);
        rotationX = Random.Range(0.0f, 2.0f);
        rotationZ = Random.Range(0.0f, 2.0f);
    }


    void Update()  {

		transform.Rotate(new Vector3 (rotationX, 1f, rotationZ) * Time.deltaTime*speed);

	}
}
