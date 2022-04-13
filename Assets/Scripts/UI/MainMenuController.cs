using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    
    
    public GameObject[] menuItems;
    public GameObject login;
    public GameObject settings;
    public GameObject info;
    public GameObject areaSelect;
    private GameObject menuToShow;

    public GameObject loginMenuButton;
    public GameObject areasMenuButton;
    public GameObject loginStatusPanel;

    public TMP_Text nameTextDisplay;
    public TMP_Text emailTextDisplay;

    public TMP_InputField nameInputField;
    public TMP_InputField emailInputField;

    
    void Start()
    {
        HideAll();

        loginStatusPanel.SetActive(GameManager.instance.loggedIn);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            emailInputField.Select();
            emailInputField.ActivateInputField();
        }
    }

    public void HideAll()
    {

        foreach(GameObject menu in menuItems)
        {
            menu.SetActive(false);
        }
    }

    public void ShowMenu(GameObject menuInput)
    {
        menuToShow = menuInput;
        foreach (GameObject menu in menuItems)
        {
            if (menu.name == menuToShow.name && !menu.activeInHierarchy == true)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }

    public void LoadArea(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LogIn()
    {
        Debug.Log(nameInputField.text);
        if (nameInputField.text == "")
        {
            return;
        }

        if (emailInputField.text == "")
        {
            return;
        }

        
        GameManager.instance.LogIn(nameInputField.text, emailInputField.text);
        SetLoginPanelDetails();

        loginMenuButton.SetActive(false);
        areasMenuButton.SetActive(true);
        loginStatusPanel.SetActive(true);

        ShowMenu(areaSelect);
    }

    public void LogOut()
    {
        HideAll();
        loginMenuButton.SetActive(true);
        areasMenuButton.SetActive(false);
        loginStatusPanel.SetActive(false);
        GameManager.instance.LogOut();
    }

    public void SetLoginPanelDetails()
    {
        nameTextDisplay.text = GameManager.instance.currentName;
        emailTextDisplay.text = GameManager.instance.currentEmail;
    }
}
