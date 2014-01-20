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
	
	void Awake()
	{
		startTime = Time.time;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Use this for initialization
	void Start () 
	{
		GameObject mainCar = GameObject.Find("che001");
		carScript = (Car)mainCar.GetComponent("Car");
	}
	
	// Update is called once per frame
	void Update () 
	{
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
			countDownText = string.Format(" ");
			float guiTime = Time.time - beginTime;
			int minutes = (int)(guiTime / 60);
			int seconds = (int)(guiTime % 60);
			int fraction = (int)((guiTime * 100) % 100);
			textTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
			GUI.Label (new Rect (screenWidth/2-100, 25, 200, 60), textTime);
			GUI.skin.label.fontSize = 30;
		}
		else
		{
			//GUI
			if(countDownSeconds == 0) countDownText = string.Format("3");
			else if(countDownSeconds == 1) countDownText = string.Format("2");
			else countDownText = string.Format("1");
			GUI.skin.label.fontSize = 50;
		}
		GUI.Label (new Rect (screenWidth/2 - 100, 150, 200, 60),countDownText);
	}
	
	//Count Down to Start
	void countDownToStart()
	{
		if(!countDownHasEnded)
		{	
			float assumeTime = Time.time - startTime;
			countDownSeconds = (int)(assumeTime % 60);
			if(countDownSeconds >= 3)
			{
				beginTime = Time.time;
				countDownHasEnded = true;
			}
		}
	}
}
