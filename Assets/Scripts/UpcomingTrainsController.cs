using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpcomingTrainsController : MonoBehaviour
{
    public TMP_Text arrivalTime;
    public TMP_Text destination;
    public TMP_Text platform;
    public TMP_Text minsLeft;
    public Image leftBar;
    public Image platformBG;

    private PIDManager pidManager;

    void Start()
    {
        pidManager = PIDManager.instance;

        leftBar.color = pidManager.platformPids[0].bgColor;
        platformBG.color = pidManager.platformPids[0].bgColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
