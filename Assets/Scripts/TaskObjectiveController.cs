using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObjectiveController : MonoBehaviour
{

    public int taskNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            TaskListController.instance.ZoneCompleteTask(taskNumber);
            Destroy(gameObject);
        }
    }

   


}
