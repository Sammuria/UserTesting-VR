using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrackedTaskController : MonoBehaviour
{
    public TMP_Text taskTitle;
    public TMP_Text taskDescription;
    public RawImage statusImage;
    public GameObject[] allElements;
    public bool displayIsVisible;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDisplay()
    {
        displayIsVisible = !displayIsVisible;
        SetAllElements(displayIsVisible);
    }

    public void TrackTask(string titleInput, string descriptionInput)
    {
        displayIsVisible = true;
        SetAllElements(displayIsVisible);
        taskTitle.text = titleInput;
        taskDescription.text = descriptionInput;
    }


    public void SetAllElements(bool ifIsTrue)
    {
        foreach (GameObject element in allElements)
        {
            element.SetActive(ifIsTrue);
        }
    }

    
}
