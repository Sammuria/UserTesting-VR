using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour
{
    public Rigidbody trackerSphere;
    public PlayerKBController player;
    public Vector3 directionToPlayer;

    void Start()
    {
        player = PlayerKBController.instance;
        trackerSphere.transform.position = new Vector3(player.transform.position.x, trackerSphere.transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        directionToPlayer = player.transform.position - trackerSphere.transform.position;
        Debug.Log(directionToPlayer);
        directionToPlayer.y = 0f;

        trackerSphere.transform.Translate(directionToPlayer);

        //trackerSphere.transform.position = new Vector3(player.transform.position.x, trackerSphere.transform.pos)

        //trackerSphere.AddForce(new Vect)
    }
}
