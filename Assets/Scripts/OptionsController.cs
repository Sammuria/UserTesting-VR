using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OptionsController : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public Slider playerHeightSlider;
    public Slider viewingAngleSlider;
    public Slider FOVSlider;
    private Camera cam;

    public TMP_Text mouseSensitivityText;
    public TMP_Text HeightText;
    public TMP_Text viewingAngleText;
    public TMP_Text FOVText;


    void Start()
    {
        cam = Camera.main;
        UpdateAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleInvertMouse()
    {

        PlayerKBController.instance.invertLook = !PlayerKBController.instance.invertLook;
    }

    public void SetMouseSensitivity()
    {
        PlayerKBController.instance.SetMouseSensitivity(mouseSensitivitySlider.value);
        mouseSensitivityText.text = mouseSensitivitySlider.value.ToString();
    }

    public void SetPlayerHeight()
    {
        PlayerKBController.instance.SetViewPointHeight(playerHeightSlider.value);
        HeightText.text = playerHeightSlider.value.ToString();
    }

    public void SetViewingAngle()
    {
        //UIController.instance.viewAngleMultiplier = viewingAngleSlider.value;
        //viewingAngleText.text = viewingAngleSlider.value.ToString();
    }

    public void SetCameraFOV()
    {
        cam.fieldOfView = FOVSlider.value;
        //FOVText.text = FOVSlider.value.ToString();
    }

    public void UpdateAll()
    {
        mouseSensitivityText.text = mouseSensitivitySlider.value.ToString();
        HeightText.text = playerHeightSlider.value.ToString();
        //viewingAngleText.text = viewingAngleSlider.value.ToString();
        //FOVText.text = FOVSlider.value.ToString();
    }
}

