using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ThumbIconController : MonoBehaviour
{
    public Color defaultColor;
    public Color highlightedColor;
    public RawImage iconImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver");
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }





    
}
