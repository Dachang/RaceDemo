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
	//sound
	public AudioClip coinSound;
	//UI texture
	public Texture scoreTexture;
	public Texture timeTexture;
	
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
		textScore = string.Format(score.ToString());
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
		GUI.Label (new Rect (screenWidth - 70, 25, 200, 60), textScore);
		GUI.Label (new Rect (20,25,500,60), textRoundNum);
		GUI.skin.label.fontSize = 50;
		//exitButton
		if(GUI.Button(new Rect(screenWidth - 60,screenHeight-50,40,40),"ESC"))
		{
			Application.Quit();
		}
		//static UI
		GUI.DrawTexture(new Rect(screenWidth/2 - timeTexture.width/2 - 40,20,timeTexture.width*2/3,timeTexture.height), timeTexture, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(new Rect(screenWidth - scoreTexture.width,20,scoreTexture.width*2/3,scoreTexture.height), scoreTexture, ScaleMode.StretchToFill, true, 0);
	}
	
	//public interfaces
	public void addScore()
	{
		score += 5;
		audio.PlayOneShot(coinSound);
	}
	
	public void addRoundNum()
	{
		roundNum++;
	}
}
