using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDManager : MonoBehaviour
{
    public static PIDManager instance;
    public PlatformPidController[] platformPids;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
