using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour
{
    public static AnswerController instance;

    public GameObject questionName;
    public GameObject questionDesc;

    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SetAnswer(GameObject taskName)
    //{
  
    //    questionName.GetComponent<Text>().text = taskName.GetComponent<Text>().text;
        
    //}


}
