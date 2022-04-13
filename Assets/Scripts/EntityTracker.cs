using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTracker : MonoBehaviour
{
    public float totalDistance = 0;

    public Vector3 prevPosition, currentPosition;

    public float distanceDelayCounter = 0;
    public int timesPolled;

    public float distanceCountDelay = 5f;
    public GameObject winZone;

    public bool countingDistance;
    public GameObject trackerMarkerObject;

    public void CompleteRun()
    {
        StatsManager.instance.AICompletedRun(totalDistance, timesPolled * distanceCountDelay + distanceDelayCounter);
        StatsManager.instance.totalDistanceOfRecords += totalDistance;
        StatsManager.instance.totalTimeOfRecords += (timesPolled * distanceCountDelay + distanceDelayCounter);
        countingDistance = false;
    }

    public void StartNewRun()
    {
        currentPosition = transform.position;
        prevPosition = transform.position;
        totalDistance = 0;
        countingDistance = true;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //    if (Input.GetKeyDown(KeyCode.F)) 
        //    {
        //        CompleteRun();
        //   }
        //    if (Input.GetKeyDown(KeyCode.R)) 
        //    {
        //        StartNewRun();
        //    }

        if (countingDistance)
        {
            distanceDelayCounter += Time.deltaTime;

            if (distanceDelayCounter >= distanceCountDelay)
            {
                prevPosition = currentPosition;
                currentPosition = transform.position;
                totalDistance += Vector3.Distance(prevPosition, currentPosition);
                distanceDelayCounter = 0f;
                timesPolled++;
                
                if (StatsManager.instance.isDroppingMarkers)
                {
                    Instantiate(trackerMarkerObject, transform.position, transform.rotation);
                }

            }
        }
    }
}