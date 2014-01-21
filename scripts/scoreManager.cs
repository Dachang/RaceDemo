using UnityEngine;
using System.Collections;

public class scoreManager : MonoBehaviour {
	
	public GUISkin mySkin;
	
	private int score;
	private string textScore;
	
	private float screenWidth;
	private float screenHeight;
	
	private int roundNum;
	private string textRoundNum;
	
	private selectCar scScript;
	private int currentCarID;
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		roundNum = 1;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		scScript = (selectCar)Camera.main.GetComponent(typeof(selectCar));
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentCarID = scScript.getCurrentCarID();
	}
	
	void OnGUI()
	{
		textScore = string.Format("Score:" + score.ToString());
		switch(currentCarID)
		{
		case 0:
			if(roundNum == 1)
			{
				textRoundNum = string.Format("Round: 1 / 2");
			}
			else if(roundNum == 7)
			{
				textRoundNum = string.Format("Round: 2 / 2");
			}
			else if(roundNum == 13)
			{
				//load Result Scene
			}
			break;
		case 1:
			textRoundNum = string.Format("Round: " + roundNum.ToString() + " / 2");
			break;
		default:
			break;
		}
		Debug.Log(roundNum);
		GUI.Label (new Rect (screenWidth-150, 25, 200, 60), textScore);
		GUI.Label (new Rect (20,25,500,60), textRoundNum);
		GUI.skin.label.fontSize = 30;
	}
	
	//public interfaces
	public void addScore()
	{
		score += 10;
	}
	
	public void addRoundNum()
	{
		roundNum++;
	}
}
