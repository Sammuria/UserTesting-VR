using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introUI : MonoBehaviour
{
    public GameObject UI, hitBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
        hitBox.SetActive(false);
    }
}
