using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class webcamMotionHeatmap : MonoBehaviour {

	public float updating;

	[Range(0.0f, 1.0f)]
	public float sensibility;
	public enum Size
	{
		veryLow,
		low,
		medium,
		high,
		veryHigh
	};
	public Size TexResolution = Size.low;

	public GameObject motionShowUI;
	public GameObject heatmapShowUI;

	public enum Mode{
		contrast,
		color
	};
	public Mode detectionMode = Mode.contrast;

	private int pixelsAffected;
	private WebCamTexture webcamTextureInitial;
	private WebCamTexture webcamTexture;
	private int optimization;

	private Color[] pixelColor;
	private float ratio;
	private int webcamResizeX = 160;
	private int webcamResizeY;
	private Color colorUpdate;
	private Sprite mySprite;
	private Sprite mySpriteHeatmap;

	public Gradient gradientMotion;
	public Gradient gradientHeatmap;

	private float differencePixel;
	private float totalDifference;

	private Texture2D texHeatmap;
	private Texture2D textureUI;

	//HEATMAP
	public bool OpacityColor;

	private float[] pixel;
	private float maxPixel = 1f;

	public bool hiddenMode;
	public KeyCode hiddenModeKey = KeyCode.H;

	
	void Awake() {

		switch (TexResolution)
		{
			case Size.veryLow:
				optimization = 12;
				break;
			case Size.low:
				optimization = 8;
				break;
			case Size.medium:
				optimization = 4;
				break;
			case Size.high:
				optimization = 2;
				break;
			case Size.veryHigh:
				optimization = 1;
				break;
		}

		//create webcamTexture with default resolution
		webcamTextureInitial = new WebCamTexture();
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = webcamTextureInitial;
		webcamTextureInitial.Play();

		ratio = (float)webcamTextureInitial.width/(float)webcamTextureInitial.height;
		print("RATIO: " + ratio);


		//resize webcamtexture
		webcamResizeX = webcamTextureInitial.width / optimization;
		webcamResizeY = webcamTextureInitial.height / optimization;

		//if resolution < 160 don't work fine
		if (webcamResizeX < 160)
        {
			webcamResizeX = 160;
			float webcamResizeYFloat = webcamResizeX / ratio;
			webcamResizeY = (int)webcamResizeYFloat;

		}

		float scaleX = ((float)webcamResizeX * (Screen.height / (float)webcamResizeY) / Screen.width);

		//scale webCam plane
		gameObject.transform.localScale = new Vector3 (1f*ratio,1f, 1f);

		//scale motionShowUI
		motionShowUI.transform.localScale = new Vector3 (scaleX,1f, 1f);


		float newfloatY = webcamResizeX/ratio;
		webcamResizeY = (int) newfloatY;

		webcamTextureInitial.Stop();

		//resize webcamTexture with predeterminate width and height (to optimize calculations)
		webcamTexture = new WebCamTexture(webcamResizeX,webcamResizeY,25);
		renderer.material.mainTexture = webcamTexture;
		webcamTexture.Play();

		//create colors
		pixelColor = new Color[webcamResizeX*webcamResizeY];


		//HEATMAP
		texHeatmap = new Texture2D(webcamResizeX, webcamResizeY);
		heatmapShowUI.GetComponent<Image>().material.mainTexture = texHeatmap;

		//create pixel variable depending on size texture
		pixel = new float[texHeatmap.width * texHeatmap.height];

		//scale heatmapShowUI		
		heatmapShowUI.GetComponent<RectTransform>().localScale = new Vector3 (scaleX, 1f,1f);
		
		textureUI = new Texture2D(webcamResizeX, webcamResizeY);

		print("RESOLUTION SCREEN: " + Screen.width + " x " + Screen.height);
		print("RESOLUTION INITIAL: " + webcamTextureInitial.width + " x " + webcamTextureInitial.height);
		print("RESOLUTION: " + webcamResizeX + " x " + webcamResizeY);

	}

	void Start (){

		InvokeRepeating("FindMotion", 0, updating);
		Invoke("ResetValues", updating + 0.1f);
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
		mySpriteHeatmap = Sprite.Create(texHeatmap, new Rect(0.0f, 0.0f, webcamResizeX, webcamResizeY), new Vector2(0.5f, 0.5f), 100.0f);
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

		for (int x = 0; x < webcamResizeX; x++){
			for (int y = 0; y < webcamResizeY; y++){
				textureUI.SetPixel(x, y, Color.clear);
			}
		}

		motionShowUI.GetComponent<Image>().sprite = mySprite;

		//for each pixel of texture 
		for (int x = 0; x < webcamResizeX; x++){
			for (int y = 0; y < webcamResizeY; y++){

				//order the pixels with number
				int i = x+webcamResizeX*y;   

				switch (detectionMode){
					case Mode.contrast:
						float lastPixel = pixelColor[i].grayscale;
						float currentPixel = webcamTexture.GetPixel (x,y).grayscale;
						differencePixel = Mathf.Abs(lastPixel - currentPixel);
						break;
					case Mode.color:
						float lastPixel_R = pixelColor[i].r;
						float currentPixel_R = webcamTexture.GetPixel (x,y).r;
						float lastPixel_G = pixelColor[i].r+pixelColor[i].g;
						float currentPixel_G = webcamTexture.GetPixel (x,y).g;
						float lastPixel_B = pixelColor[i].b;
						float currentPixel_B = webcamTexture.GetPixel (x,y).b;
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
				pixelColor[i] = webcamTexture.GetPixel (x,y);

			}
		}

		textureUI.Apply();
		mySprite = Sprite.Create(textureUI, new Rect(0.0f, 0.0f, webcamResizeX, webcamResizeY), new Vector2(0.5f, 0.5f), 100.0f);

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