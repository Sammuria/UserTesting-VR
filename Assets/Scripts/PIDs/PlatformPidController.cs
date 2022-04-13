using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPidController : MonoBehaviour
{
    public string destination;
    public string currentTime;
    public string minutesLeft;
    public string platformNumber;
    public string description;
    public Color bgColor;
    [Multiline(18)] public string stopsList;

    public UpcomingTrainInfo[] upcomingTrains;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
