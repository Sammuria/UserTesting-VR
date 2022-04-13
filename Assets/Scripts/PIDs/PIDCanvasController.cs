using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PIDCanvasController : MonoBehaviour
{
    private PlatformPidController pidManager;
    
    [SerializeField] private TMP_Text destinationText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text minsLeftText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text[] stopsText;
    [SerializeField] private TMP_Text platformText;
    
    public Image platformBG;
    public Image topBar;
    public int platformNumber;
    public Image blackPanel;
    public GameObject viewTarget;
    public bool hasColour;
    private Color tempColor;

    public bool initialized;

    public UpcomingTrainsController[] upcomingTrains;

    void Start()
    {
        platformNumber--;

        pidManager = PIDManager.instance.platformPids[platformNumber];




        UpdatePID();

        
    }

    void Update()
    {
        
        
        if (!initialized)
        {
            UpdatePID();
            initialized = true;
        }
        //Debug.Log(Vector3.Angle(viewTarget.transform.position, PlayerKBController.instance.transform.position));
        //Debug.Log(PlayerKBController.instance.transform.position);
        //Debug.Log(transform.position);
        //Vector3 direction = (viewTarget.transform.position - PlayerKBController.instance.transform.position).normalized;
        //var calc =  direction.x - direction.z;

        //float dot = Vector3.Dot(transform.forward, (PlayerKBController.instance.transform.position - transform.position).normalized);


        //if (blackPanel == null)
        //{
        //    return;
        //}
        //tempColor = blackPanel.color;
        //tempColor.a = 2 + dot * UIController.instance.viewAngleMultiplier;

        //blackPanel.color = tempColor;
    }

    public void UpdatePID()
    {
        destinationText.text = pidManager.destination;
        timeText.text = pidManager.currentTime;

        if (hasColour)
        {
            platformBG.color = pidManager.bgColor;
            topBar.color = pidManager.bgColor;
        }

        foreach (TMP_Text stopsPage in stopsText)
        {
        stopsPage.text = pidManager.stopsList;

        }
        descriptionText.text = pidManager.description;
        platformText.text = pidManager.platformNumber;
        minsLeftText.text = pidManager.minutesLeft + " mins";

        for (int i = 0; i < upcomingTrains.Length; i++)
        {
            upcomingTrains[i].arrivalTime.text = pidManager.upcomingTrains[i].arrivalTime;
            upcomingTrains[i].destination.text = pidManager.upcomingTrains[i].destination;
            upcomingTrains[i].platform.text = pidManager.upcomingTrains[i].platform;
            upcomingTrains[i].minsLeft.text = pidManager.upcomingTrains[i].minsLeft;

            if (hasColour)
            {
                upcomingTrains[i].platformBG.color = pidManager.bgColor;
                upcomingTrains[i].leftBar.color = pidManager.bgColor;
            }

        }
    }
}
