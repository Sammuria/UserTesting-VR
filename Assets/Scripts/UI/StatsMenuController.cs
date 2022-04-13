using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsMenuController : MonoBehaviour
{
    public static StatsMenuController instance;



    public Animator anim;
    public Animator mapAnim;
    public bool statsVisible;

    public TMP_Text completedText;
    public TMP_Text timeText;
    public TMP_Text distanceText;

    public bool mapVisible = true;
    public bool mapLarge = false;
    public bool heatmapOn = false;
    public bool lightOn = true;


    public RawImage mapImage;
    public RenderTexture minimapRenderTexture;
    public RenderTexture heatmapRenderTexture;

    public GameObject mapObject;
    public GameObject light;


    private void Awake()
    {
        instance = this;
    }

    public void ToggleStatsMenu()
    {
        statsVisible = !statsVisible;
        anim.SetBool("statsVisible", statsVisible);
    }

    public void ToggleHeatmap()
    {
        heatmapOn = !heatmapOn;

        if (heatmapOn)
        {
            mapImage.texture = heatmapRenderTexture;
        }
        else
        {
            mapImage.texture = minimapRenderTexture;
        }
    }

    public void ToggleMapVisible()
    {
        mapVisible = !mapVisible;
        mapObject.SetActive(mapVisible);

    }


    public void TogglelightOn()
        {
            lightOn = !lightOn;
            light.SetActive(lightOn);

        }


    public void ToggleTrailsVisible()
    {
        StatsManager.instance.ToggleTrails();

    }

    public void ToggleMarkersVisible()
    {
        StatsManager.instance.TogglePositionMarkers();

    }

    public void ToggleMapSize()
    {
        mapLarge = !mapLarge;
        mapAnim.SetBool("isEnlarged", mapLarge);
    }

    public void UpdateDisplays(float completedJourneys, float averageTime, float averageDistance)
    {
        completedText.text = completedJourneys.ToString();
        timeText.text = averageTime.ToString();
        distanceText.text = averageDistance.ToString();
    }
        

}
