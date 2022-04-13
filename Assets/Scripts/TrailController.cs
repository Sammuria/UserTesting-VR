using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public bool isVisible;
    public GameObject visual;
    public TrailRenderer trailEffect;
    public Color[] colors;

    public bool isSetToDestroy;
    public float destroyTimer;

    void Start()
    {
        StatsManager.instance.renderedTrails.Add(this);
        Initialize();
        destroyTimer = trailEffect.time + 1f;
    }

    public void ToggleLayer(bool isShowing)
    {
        isVisible = isShowing;
        if (isVisible)
        {
            visual.gameObject.layer = 0;
        }
        else
        {
            visual.gameObject.layer = 7;
        }
    }

    public void Initialize()
    {
        trailEffect.material.color = colors[Random.Range(0, colors.Length)];

        if (StatsManager.instance.trailsVisible)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 7;
        }
    }

    public void AdjustHeight(float viewPointOffset)
    {
        visual.transform.position = transform.position + new Vector3(0, viewPointOffset, 0);
    }

    public void StartDestroyTimer()
    {
        isSetToDestroy = true;
    }

    private void Update()
    {
        if (isSetToDestroy)
        {
            destroyTimer -= Time.deltaTime;
                if (destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
