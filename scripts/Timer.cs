using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	private float startTime;
	private float beginTime;
	private int countDownSeconds;
	
	private float screenWidth;
	private float screenHeight;
	
	private string countDownText;
	public string textTime;
	
	public bool countDownHasEnded = false;
	
	//car script
	private Car carScript;
	private selectCar scScript;
	
	private int currentCarID;
	private GameObject mainCar;
	
	private Texture countDownTexture;
	
	void Awake()
	{
		startTime = Time.time;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Use this for initialization
	void Start () 
	{
		mainCar = GameObject.Find("che001");
		carScript = (Car)mainCar.GetComponent("Car");
		scScript = (selectCar)Camera.main.GetComponent(typeof(selectCar));
		countDownTexture = (Texture)Resources.Load("LabelThree",typeof(Texture));
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentCarID = scScript.getCurrentCarID();
		switch(currentCarID)
		{
		case 0:
			break;
		case 1:
			mainCar = GameObject.Find("che002");
			carScript = (Car)mainCar.GetComponent("Car");
			break;
		default:
			break;
		}
		if(countDownHasEnded)
		{
			carScript.setThrottle(true);
		}
		else
		{
			carScript.setThrottle(false);
		}
	}
	
	void OnGUI()
	{
		countDownToStart();
		if(countDownHasEnded)
		{
			//GUI
			countDownTexture = countDownTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			countDownText = string.Format(" ");
			float guiTime = Time.time - beginTime;
			int minutes = (int)(guiTime / 60);
			int seconds = (int)(guiTime % 60);
			int fraction = (int)((guiTime * 100) % 100);
			textTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
			GUI.Label (new Rect (screenWidth/2+10, 25, 200, 60), textTime);
			GUI.skin.label.fontSize = 30;
		}
		else
		{
			//GUI
			if(countDownSeconds == 0) countDownTexture = (Texture)Resources.Load("LabelThree",typeof(Texture));
			else if(countDownSeconds == 1) countDownTexture = (Texture)Resources.Load("LabelTwo",typeof(Texture));
			else if(countDownSeconds == 2) countDownTexture = (Texture)Resources.Load("LabelOne",typeof(Texture));
			else countDownTexture = countDownTexture = (Texture)Resources.Load("LabelStart",typeof(Texture));
			GUI.skin.label.fontSize = 50;
		}
		//GUI.Label (new Rect (screenWidth/2 - 100, 150, 200, 60),countDownText);
		//count Down UI
		GUI.DrawTexture(new Rect(screenWidth/2 - countDownTexture.width/2,screenHeight/2 - countDownTexture.height/2,
			countDownTexture.width,countDownTexture.height), countDownTexture, ScaleMode.StretchToFill, true, 0);
	}
	
	//Count Down to Start
	void countDownToStart()
	{
		if(!countDownHasEnded)
		{	
			float assumeTime = Time.time - startTime;
			countDownSeconds = (int)(assumeTime % 60);
			if(countDownSeconds >= 4)
			{
				beginTime = Time.time;
				countDownHasEnded = true;
			}
		}
	}
	
	//public interfaces
	public string getTimeString()
	{
		return textTime;
	}
}
