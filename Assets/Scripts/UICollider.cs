using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollider : MonoBehaviour
{
    public GameObject UI;

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
    }
}
