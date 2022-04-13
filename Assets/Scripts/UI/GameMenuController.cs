using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    public static GameMenuController instance;

    public Animator settingsAnim;
    public bool settingsIsOpen;
    public GameObject settingsMenuObject;
    public GameObject surveyScreen;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        settingsIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSettingsMenu()
    {
        settingsIsOpen = !settingsIsOpen;
        settingsAnim.SetBool("isOpen", settingsIsOpen);
    }


    public void ContinueGame()
    {
        surveyScreen.SetActive(false);
    }
}
