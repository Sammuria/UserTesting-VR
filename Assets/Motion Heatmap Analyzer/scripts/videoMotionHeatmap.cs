using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class videoMotionHeatmap : MonoBehaviour {

	public float updating;

	[Range(0.0f, 1.0f)]
	public float sensibility;

	public GameObject motionShowUI;
	public GameObject heatmapShowUI;

	public enum Mode{
		contrast,
		color
	};
	public Mode detectionMode = Mode.contrast;

	private int pixelsAffected;
	private int optimization;

	private int videoTextureX;
	private int videoTextureY;

	private Color[] pixelColor;
	private float ratio;
	private Color colorUpdate;
	private Sprite mySprite;
	private Sprite mySpriteHeatmap;

	public Gradient gradientMotion;
	public Gradient gradientHeatmap;

	private float differencePixel;
	private float totalDifference;

	public RenderTexture myRenderTexture;
	private Texture2D myTexture2D;
	private Texture2D textureUI;
	private Texture2D texHeatmap;


	//HEATMAP
	public bool OpacityColor;

	private float[] pixel;
	private float maxPixel = 1f;

	public bool hiddenMode;
	public KeyCode hiddenModeKey = KeyCode.H;

	
	void Awake() {

		videoTextureX = myRenderTexture.width;
		videoTextureY = myRenderTexture.height;

		ratio = (float)videoTextureX / (float)videoTextureY;

		//scale webCam plane
		gameObject.transform.localScale = new Vector3 (1f*ratio,1f, 1f);

		float scaleX = ((float)videoTextureX * (Screen.height / (float)videoTextureY) / Screen.width);
		//scale motionShowUI
		motionShowUI.transform.localScale = new Vector3 (scaleX, 1f, 1f);

		//create texture2D
		myTexture2D = new Texture2D(videoTextureX, videoTextureY, TextureFormat.ARGB32, false);


		//create colors
		pixelColor = new Color[videoTextureX * videoTextureY];


		//HEATMAP
		texHeatmap = new Texture2D(videoTextureX, videoTextureY);
		heatmapShowUI.GetComponent<Image>().material.mainTexture = texHeatmap;

		//create pixel variable depending on size texture
		pixel = new float[texHeatmap.width * texHeatmap.height];

		//scale heatmapShowUI		
		heatmapShowUI.GetComponent<RectTransform>().localScale = new Vector3 (scaleX, 1f,1f);
		
		textureUI = new Texture2D(videoTextureX, videoTextureY);

	}

	void Start (){

		InvokeRepeating("FindMotion", 0, updating);
		Invoke("ResetValues", updating + 0.5f);
	}

	public void ResetValues()
	{
		print("RESET");
		//value of pixels 0 
		for (int i = 0; i < texHeatmap.width * texHeatmap.height; i++)
		{
			pixel[i] = 0f;
		}
		maxPixel = 1f;
		Draw();
	}

	public void StopFindMotion()
    {
		CancelInvoke("FindMotion");
	}
	public void PlayFindMotion()
	{
		InvokeRepeating("FindMotion", 0, updating);
	}


	void Draw() {

		heatmapShowUI.GetComponent<Image>().sprite = mySpriteHeatmap;

		//if texture size is 64x32 , does 2048 calculations
		for (int x = 0; x < texHeatmap.width; x++)
		{
			for (int y = 0; y < texHeatmap.height; y++)
			{
				int i = x + texHeatmap.width * y;

				//put the value of pixel in alpha or in ramp, relative to maximum value of all reticule
				if (OpacityColor == true)
				{
					Color colorUpdate = new Color(0f, 1f, 0f, pixel[i] / maxPixel);
					texHeatmap.SetPixel(x, y, colorUpdate);
				}
				else
				{
					Color colorUpdate = gradientHeatmap.Evaluate(pixel[i] / maxPixel);
					texHeatmap.SetPixel(x, y, colorUpdate);
				}

			}
		}
		//apply texture
		texHeatmap.Apply();
		mySpriteHeatmap = Sprite.Create(texHeatmap, new Rect(0.0f, 0.0f, videoTextureX, videoTextureY), new Vector2(0.5f, 0.5f), 100.0f);
	}

	public void hiddenModeSwitch()
	{

		if (hiddenMode == true)
		{
			hiddenMode = false;
			heatmapShowUI.GetComponent<Image>().enabled = true;
		}
		else
		{
			hiddenMode = true;
			heatmapShowUI.GetComponent<Image>().enabled = false;
		}
	}

	void FindMotion (){

		RenderTexture.active = myRenderTexture;
		myTexture2D.ReadPixels(new Rect(0, 0, videoTextureX, videoTextureY), 0, 0);


		for (int x = 0; x < videoTextureX; x++){
			for (int y = 0; y < videoTextureY; y++){
				textureUI.SetPixel(x, y, Color.clear);
			}
		}

		motionShowUI.GetComponent<Image>().sprite = mySprite;

		//for each pixel of texture 
		for (int x = 0; x < videoTextureX; x++){
			for (int y = 0; y < videoTextureY; y++){

				//order the pixels with number
				int i = x+ videoTextureX * y;   

				switch (detectionMode){
					case Mode.contrast:
						float lastPixel = pixelColor[i].grayscale;
						float currentPixel = myTexture2D.GetPixel (x,y).grayscale;
						differencePixel = Mathf.Abs(lastPixel - currentPixel);
						break;
					case Mode.color:
						float lastPixel_R = pixelColor[i].r;
						float currentPixel_R = myTexture2D.GetPixel (x,y).r;
						float lastPixel_G = pixelColor[i].r+pixelColor[i].g;
						float currentPixel_G = myTexture2D.GetPixel (x,y).g;
						float lastPixel_B = pixelColor[i].b;
						float currentPixel_B = myTexture2D.GetPixel (x,y).b;
						differencePixel = (Mathf.Abs(lastPixel_R - currentPixel_R) + Mathf.Abs(lastPixel_G - currentPixel_G) + Mathf.Abs(lastPixel_B - currentPixel_B))/3;
						break;
				}

				if (differencePixel > Mathf.Abs(sensibility-1.0f)){
					++pixelsAffected;
					colorUpdate = gradientMotion.Evaluate((differencePixel));
					totalDifference = totalDifference+differencePixel;
					textureUI.SetPixel(x, y, colorUpdate);

					//add value to the pixel heatmap
					pixel[i] += 1f;

					//take the highest value of the pixels heatmap
					if (pixel[i] > maxPixel)
					{
						maxPixel = pixel[i];
					}

				}

				//update pixel color
				pixelColor[i] = myTexture2D.GetPixel (x,y);

			}
		}

		textureUI.Apply();
		mySprite = Sprite.Create(textureUI, new Rect(0.0f, 0.0f, videoTextureX, videoTextureY), new Vector2(0.5f, 0.5f), 100.0f);

		totalDifference = 0;
		pixelsAffected = 0;

		if (hiddenMode == false)
		{
			Draw();
		}
	}

    private void Update()
    {
		//HEATMAP
		if (Input.GetKeyDown(hiddenModeKey))
		{
			hiddenModeSwitch();
		}
	}



}