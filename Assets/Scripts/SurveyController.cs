using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyController : MonoBehaviour
{

    public static SurveyController instance;

    public GameObject questionOne;
    public GameObject questionTwo;
    public GameObject completedScreen;
    public GameObject introScreen;
    public Slider progressSlider;

    private bool canOpenTasks = false;

    public GameObject[] allQuestions;

    private bool showingTasks;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        ShowScreen("intro");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LockCursor(bool isBeingLocked)
    {
        if (isBeingLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }



    public void ShowScreen(string screenName)
    {
        foreach (GameObject question in allQuestions)
        {
            question.SetActive(false);
        }

        if (screenName == "")
        {
            questionOne.SetActive(true);
            LockCursor(false);
            PlayerKBController.instance.GivePlayerControl(false);
            canOpenTasks = false;
        }

        if (screenName == "intro")
        {
            introScreen.SetActive(true);
            LockCursor(false);
            PlayerKBController.instance.GivePlayerControl(false);
            canOpenTasks = false;
        }


        if (screenName == "Question1")
        {
            questionOne.SetActive(true);
            LockCursor(false);
            PlayerKBController.instance.GivePlayerControl(false);
            canOpenTasks = false;
        }



        if (screenName == "Question2")
        {
            questionTwo.SetActive(true);
            LockCursor(false);
            PlayerKBController.instance.GivePlayerControl(false);
            canOpenTasks = false;
        }

        if (screenName == "Completed")
        {
            completedScreen.SetActive(true);
            LockCursor(false);
            PlayerKBController.instance.GivePlayerControl(false);
            canOpenTasks = false;
        }

    }

    public void IncreaseSlider()
    {
        progressSlider.value += 3;

    }

    public void ResetAfterSurvey()
    {
        GameManager.instance.ResetGame();
    }

    public void ContinueAfterSurvey()
    {
        GameMenuController.instance.ContinueGame();
    }

}
