using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenshot : MonoBehaviour
{
    public KeyCode captureKey = KeyCode.E;

    public float continuousShootingDelay = 0.2f;
    private int numScreenshots;

    public GameObject[] hideObjects;

     public IEnumerator SaveScreenshot()
    {
        // string TwoStepScreenshotPath = MobileNativeShare.SaveScreenshot("Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second);
        // Debug.Log("A new screenshot was saved at " + TwoStepScreenshotPath);

        //string myFileName = "Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + ".png";
        string myFileName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "_" + numScreenshots + ".png";

        //string myDefaultLocation = Application.persistentDataPath + "/" + myFileName;
        //string myFolderLocation = "/storage/emulated/0/DCIM/Camera/";  //EXAMPLE OF DIRECTLY ACCESSING A CUSTOM FOLDER OF THE GALLERY
        //string myScreenshotLocation = myFolderLocation + myFileName;

        //ENSURE THAT FOLDER LOCATION EXISTS
        //if (!System.IO.Directory.Exists(myFolderLocation)) {
        //   System.IO.Directory.CreateDirectory(myFolderLocation);
        //}

        for (int i = 0; i < hideObjects.Length; i++)
        {
            hideObjects[i].SetActive(false);
        }

        ScreenCapture.CaptureScreenshot(myFileName);
        print("screenshot");

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < hideObjects.Length; i++)
        {
            hideObjects[i].SetActive(true);
        }


        /*
        //MOVE THE SCREENSHOT WHERE WE WANT IT TO BE STORED

        yield return new WaitForSeconds(1);

        System.IO.File.Move(myDefaultLocation, myScreenshotLocation);

        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS BEGUN
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + myScreenshotLocation) });
        objActivity.Call("sendBroadcast", objIntent);
        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS COMPLETE

        */
    }

    public void doScreenshot()
    {
        numScreenshots = 0;
        StartCoroutine(SaveScreenshot());
    }


    public void Update()
    {
        if (Input.GetKeyDown(captureKey))
        {
            doScreenshot();
        }
    }

    public void doScreenshotContinuous()
    {
        ++numScreenshots;
        StartCoroutine(SaveScreenshot());
    }

    public void playContinuousShooting()
    {
        numScreenshots = 0;
        InvokeRepeating("doScreenshotContinuous", 0, continuousShootingDelay);
    }

    public void stopContinuousShooting()
    {
        CancelInvoke("doScreenshotContinuous");
    }

}
