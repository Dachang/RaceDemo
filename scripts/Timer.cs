using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	private float startTime;
	private float screenWidth;
	private float screenHeight;
	
	public string textTime;
	
	void Awake()
	{
		startTime = Time.time;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		float guiTime = Time.time - startTime;
		int minutes = (int)(guiTime / 60);
		int seconds = (int)(guiTime % 60);
		int fraction = (int)((guiTime * 100) % 100);
		textTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
		GUI.Label (new Rect (screenWidth/2-100, 25, 200, 60), textTime);
		GUI.skin.label.fontSize = 30;
	}
}
