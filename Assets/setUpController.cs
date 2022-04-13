using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class setUpController : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public Slider playerHeightSlider;
  

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMouseSensitivity()
    {
        PlayerKBController.instance.SetMouseSensitivity(mouseSensitivitySlider.value);
        //mouseSensitivityText.text = mouseSensitivitySlider.value.ToString();
    }

    public void SetPlayerHeight()
    {
        PlayerKBController.instance.SetViewPointHeight(playerHeightSlider.value);
        //HeightText.text = playerHeightSlider.value.ToString();

    }

 

}
