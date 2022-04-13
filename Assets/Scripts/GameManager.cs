using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool loggedIn;
    public string currentName = "";
    public string currentEmail = "";
    public bool cursorLocked = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
        


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerKBController.instance != null)
        {

            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                cursorLocked = !cursorLocked;
                LockCursor(cursorLocked);

   
                PlayerKBController.instance.GivePlayerControl(cursorLocked);
            }
        }
    }

    public void LockCursor(bool isBeingLocked)
    {
        if (isBeingLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

        }

    }


    public void ResetGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void LogIn(string nameInput, string emailInput)
    {
        loggedIn = true;
        currentName = nameInput;
        currentEmail = emailInput;
    }
    
    public void LogOut()
    {
        loggedIn = false;
        currentName = "";
        currentEmail = "";
    }

    public void LoadArea(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
