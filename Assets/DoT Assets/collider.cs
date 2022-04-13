using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    public GameObject UI, rating;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(true);
            rating.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
        rating.SetActive(false);
    }
}
