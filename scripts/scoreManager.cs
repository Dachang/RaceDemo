using UnityEngine;
using System.Collections;

public class scoreManager : MonoBehaviour {
	
	private int score;
	private string textScore;
	private float screenWidth;
	private float screenHeight;
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnGUI()
	{
		textScore = string.Format("Score:" + score.ToString());
		GUI.Label (new Rect (screenWidth-150, 25, 200, 60), textScore);
		GUI.skin.label.fontSize = 30;
	}
	
	//public interfaces
	public void addScore()
	{
		score += 10;
	}
}
