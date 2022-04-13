using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMarkerController : MonoBehaviour
{
    public bool isVisible;
    public GameObject visual;
    void Start()
    {
        
        StatsManager.instance.positionMarkers.Add(this);
        Initialize();


    }

    public void ToggleLayer(bool isShowing)
    {
        isVisible = isShowing;
        if (isVisible)
        {
            visual.layer = 0;
        }
        else
        {
            visual.layer = 7;
        }
    }

    public void Initialize()
    {
        if (StatsManager.instance.positionMarkersVisible)
        {
            visual.layer = 0;
        }
        else
        {
            visual.layer = 7;
        }
    }

    public void AdjustHeight(float viewPointOffset)
    {
        visual.transform.position = transform.position + new Vector3(0, viewPointOffset, 0);
    }
}
