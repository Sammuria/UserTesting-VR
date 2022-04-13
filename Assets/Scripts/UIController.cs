using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject startUI, prototypeUI;
    
    // Start is called before the first frame update
    void Start()
    {
        startUI.SetActive(true);
        prototypeUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideUI()
    {
        startUI.SetActive(false);
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
