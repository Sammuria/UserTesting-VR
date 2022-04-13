using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskController : MonoBehaviour
{
    
    public static TaskController instance;
    public TMP_Text title;
    public TMP_Text description;
    public int taskNumber;
    public GameObject questionType;
    public TMP_Text instructionText;

    public Button trackButton;

    public bool active;
    public bool complete;

    public Image taskMarker;
    public Color incompleteColor;
    public Color completeColor;

    public RawImage statusDisplay;
    public Texture activeTexture;
    public Texture completeTexture;

    public bool displayInputFields;

    private void Awake()
    {
        instance = this;
    }



    void Start()
    {
        complete = false;
        statusDisplay.gameObject.SetActive(false);
        taskMarker.color = incompleteColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTaskComplete()
    {
        taskMarker.color = completeColor;
        statusDisplay.texture = completeTexture;
        
    }


    public void ViewAnswerScreen(int questionNumber)
    {

    }
    public void TrackTask()
    {
        statusDisplay.gameObject.SetActive(true);
    }

}
