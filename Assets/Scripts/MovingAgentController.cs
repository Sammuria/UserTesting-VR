using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingAgentController : MonoBehaviour

{
    public GameObject[] skins;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    //public Animator anim;

    public Transform targetDestination;

    public bool hasFare;

    public bool paused;
    public float pauseDuration;

    public int spawnGroup;
    public Animator anim;
    public EntityTracker tracker;
    public TrailController trailCont;


    


    // Start is called before the first frame update
    void Start()
    {

        if (Random.Range(0f, 1f ) > .5)
        {
            hasFare = true;
        }


        startPoint = transform.position;
        
        if (hasFare)
        {
            targetDestination = CrowdManager.instance.GetRandomDestination();
        }

        else
        {
            if (spawnGroup == 1)
            {
            targetDestination = CrowdManager.instance.topUpMachinesGroup1[Random.Range(0, CrowdManager.instance.topUpMachinesGroup1.Length)].transform;
            }
            if (spawnGroup == 2)
            {
                targetDestination = CrowdManager.instance.topUpMachinesGroup2[Random.Range(0, CrowdManager.instance.topUpMachinesGroup2.Length)].transform;
            }

        }

        foreach (GameObject skin in skins)
        {
            skin.SetActive(false);
        }
        skins[Random.Range(0, skins.Length)].SetActive(true);

        targetPoint = targetDestination.transform.position;
        agent.destination = targetPoint;
    }

    // Update is called once per frame
    void Update()
    {

        var distanceTest = Vector3.Distance(targetPoint, transform.position);
        if (distanceTest < 5f)
        {
            if (hasFare) {

                tracker.CompleteRun();
                StatsManager.instance.renderedTrails.Remove(trailCont);
                trailCont.StartDestroyTimer();
                trailCont.gameObject.transform.SetParent(null);
                Destroy(gameObject);
            }
            else
            {
                
                
                anim.SetTrigger("Using");
                targetPoint = CrowdManager.instance.GetRandomDestination().position;
                paused = true;
                pauseDuration = 3f;
                  
                hasFare = true;
            }
            

        }

        if (paused)
        {
            pauseDuration -= Time.deltaTime;
            if (pauseDuration <= 0)
            {
                agent.destination = targetPoint;
                paused = false;
            }
        }

    }


}
