using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskListController : MonoBehaviour
{
    public static TaskListController instance;

    public Animator anim;
    private bool listIsUp = false;
    private bool answerScreenVisible = false;

    public TaskController[] tasks;
    private int tasksComplete;
    public int currentTask;
    public AnswerController answerPage;
    public int answerSubmitRefInt;
    public bool objectiveTracked = false;
    public int trackedObjectiveInt = 0;
    public GameObject trackedObjectiveDisplay;

    public TMP_Text answerTitle;
    public TMP_Text questionDescription;
    public GameObject inputFields;

    public TMP_InputField platformInput;
    public TMP_InputField timeInput;
    public TMP_InputField commentsInput;

    public TrackedTaskController trackedTask;

    public GameObject trackButton;
    public GameObject unTrackButton;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        trackedObjectiveDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoneCompleteTask(int taskNumber)
    {
        currentTask = taskNumber;
        CompleteTask();
    }

    public void CompleteTask()
    {
        tasksComplete++;

        tasks[currentTask].SetTaskComplete();

        if (tasksComplete == tasks.Length - 1)
        {
            Debug.Log("Completed All!!!");
        }
        else
        {
            Debug.Log("Completed One!!");

        }

    }

    public void ToggleTaskVisibility()
    {
        listIsUp = !listIsUp;
        anim.SetBool("listIsUp", listIsUp);
    }


    public void ShowAnswerSceen()
    {
        inputFields.SetActive(tasks[currentTask].displayInputFields);
        

        answerScreenVisible = true;
        anim.SetBool("AnswerInputShowing", answerScreenVisible);

        
        platformInput.text = "";
        timeInput.text = "";
        commentsInput.text = "";
        answerTitle.text = tasks[currentTask].title.text;
        questionDescription.text = tasks[currentTask].description.text;

        if (trackedObjectiveInt == currentTask && objectiveTracked)
        {
            trackButton.SetActive(false);
            unTrackButton.SetActive(true);
        }
        else
        {
            trackButton.SetActive(true);
            unTrackButton.SetActive(false);
        }
        
        
    }

    public void GoToTaskButton(int taskNumber)
    {
        currentTask = taskNumber;
        ShowAnswerSceen();
    }

    public void SubmitAnswerButton()
    {
        CompleteTask();
        HideAnswerScreen();
    }

    public void HideAnswerScreen()
    {
        answerScreenVisible = false;
        anim.SetBool("AnswerInputShowing", answerScreenVisible);
    }


    public void SetAllInactive()
    {
        foreach (TaskController task in tasks)
        {
            //  task.SetTaskInactive();
        }
    }

    public void TrackTask()
    {
        objectiveTracked = true;

        trackedObjectiveInt = currentTask;

        trackedTask.TrackTask(tasks[currentTask].title.text, tasks[currentTask].description.text);
        
        trackedObjectiveDisplay.SetActive(true);
        for (int i=0; i<tasks.Length; i++)
        {
            if (i == currentTask)
            {
                tasks[i].statusDisplay.gameObject.SetActive(true);
            }
            else
            {
                tasks[i].statusDisplay.gameObject.SetActive(false);
            }
        }
        
        trackButton.SetActive(false);
        unTrackButton.SetActive(true);
    }

    public void UntrackTask()
    {
        objectiveTracked = false;
        trackedObjectiveDisplay.SetActive(false);
        trackButton.SetActive(true);
        unTrackButton.SetActive(false);
        tasks[currentTask].statusDisplay.gameObject.SetActive(false);

    }
}
