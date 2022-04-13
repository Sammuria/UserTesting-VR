using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPoint : MonoBehaviour
{
    public GameObject displayObject;
    public DetectionZone detector;

    public RawImage thumbUpImage;
    public RawImage thumbDownImage;
    public Color transparentColor;
    public Color selectedColor;

    public Animator savedAnim;
    

    private void Start()
    {
        
    }

    private void Update()
    {
        if (detector.playerDetected)
        {
            displayObject.SetActive(true);
            displayObject.transform.forward = PlayerKBController.instance.cam.transform.forward;
        }
        else
        {
            displayObject.SetActive(false);
        }
    }

    public void ClickLikeButton()
    {
        thumbUpImage.color = selectedColor;
        thumbUpImage.gameObject.SetActive(true);
        thumbDownImage.gameObject.SetActive(false);
        savedAnim.SetTrigger("saved");
    }
    public void ClickDislikeButton()
    {
        thumbUpImage.gameObject.SetActive(false);
        thumbDownImage.gameObject.SetActive(true);
        savedAnim.SetTrigger("saved");

    }

    public void TriggerSavedText()
    {
        savedAnim.SetTrigger("saved");
    }
}
