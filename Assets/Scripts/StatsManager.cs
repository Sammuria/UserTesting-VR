using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static StatsManager instance;
    public List <float> distanceRecord;
    public List<float> timeTakenRecord;
    
    public List<PositionMarkerController> positionMarkers;
    public List<TrailController> renderedTrails;
    public bool positionMarkersVisible = false;
    public bool trailsVisible = false;
    public bool isDroppingMarkers = false;

    public Slider trailHeightSlider;
    public Slider markerHeightSlider;

    public float trailOffset = 0;
    public float markerOffset = 0;


    public float totalTimeOfRecords;
    public float totalDistanceOfRecords;
    public float numberOfRecords;

    public float totalDistance = 0;

    public Vector3 prevPosition, currentPosition;

    public float distanceDelayCounter = 0;


    public float distanceCountDelay = 5f;
    

    public bool countingDistance;


    public void TogglePositionMarkers()
    {
        positionMarkersVisible = !positionMarkersVisible;
        foreach (PositionMarkerController positionMarker in positionMarkers)
        {
            
            positionMarker.ToggleLayer(positionMarkersVisible);
        }
    }

    public void ToggleTrails()
    {
        trailsVisible = !trailsVisible;
        foreach (TrailController trail  in renderedTrails)
        {

            trail.ToggleLayer(trailsVisible);
        }
    }

    public void CompleteRun() {
        distanceRecord.Add(totalDistance);
        countingDistance = false;
    }

    public void AICompletedRun(float distanceTravelled, float timeTaken)
    {
        distanceRecord.Add(distanceTravelled);
        timeTakenRecord.Add(timeTaken);
        StatsMenuController.instance.UpdateDisplays(distanceRecord.Count, totalTimeOfRecords / distanceRecord.Count, totalDistanceOfRecords / distanceRecord.Count);
    }

    public void StartNewRun() {
        currentPosition = transform.position;
        prevPosition = transform.position;
        totalDistance = 0;
        countingDistance = true;
    }

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            TogglePositionMarkers();
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ToggleTrails();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isDroppingMarkers = !isDroppingMarkers;
        }

        if (countingDistance) 
        {
            distanceDelayCounter += Time.deltaTime;
        
            if (distanceDelayCounter >= distanceCountDelay) 
            {
                prevPosition = currentPosition;
                currentPosition = transform.position;
                totalDistance += Vector3.Distance(prevPosition, currentPosition);
                distanceDelayCounter = 0f;
            }
        }
    }
}
