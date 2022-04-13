using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ThumbstickLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
     
    public Rigidbody player;

    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var thumbStickaxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick,OVRInput.Controller.LTouch);

        float fixedY = player.position.y;
        player.position += (transform.right * thumbStickaxis.x + transform.forward * thumbStickaxis.y) * Time.deltaTime * speed;
        player.position = new Vector3(player.position.x, fixedY, player.position.z);
    }
}
