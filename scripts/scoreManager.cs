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
	private int roundCount;
	
	private selectCar scScript;
	private Timer tScript;
	
	private int currentCarID;
	//sound
	public AudioClip coinSound;
	//UI texture
	public Texture scoreTexture;
	public Texture timeTexture;
	public Texture resultTexture;
	//result Label
	private string resultTimeLabel;
	private string resultScoreLabel;
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		roundCount = 0;
		roundNum = 1;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		scScript = (selectCar)Camera.main.GetComponent(typeof(selectCar));
		tScript = (Timer)Camera.main.GetComponent(typeof(Timer));
		resultTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
		resultTimeLabel = string.Format("");
		resultScoreLabel = string.Format("");
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentCarID = scScript.getCurrentCarID();
		updateGameState();
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
				roundCount = 1;
			}
			else if(roundNum == 7)
			{
				textRoundNum = string.Format("Round: 2 / 2");
				roundCount = 2;
			}
			else if(roundNum == 13)
			{
				//load Result Scene
				roundCount = 3;
			}
			break;
		case 1:
			textRoundNum = string.Format("Round: " + roundNum.ToString() + " / 2");
			if(roundNum == 3) roundCount = 3;
			break;
		default:
			break;
		}
		Debug.Log(roundNum);
		GUI.Label (new Rect (screenWidth - 90, 25, 200, 60), textScore);
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
		//Dynamic UI
		GUI.DrawTexture(new Rect(screenWidth/2 - resultTexture.width/2 - 100,screenHeight/2 - resultTexture.height/2,resultTexture.width,resultTexture.height), resultTexture, ScaleMode.StretchToFill, true, 0);
		GUI.Label(new Rect (screenWidth/2 - resultTexture.width/2 + 420, screenHeight/2 - 100, 200, 60), resultTimeLabel);
		GUI.Label(new Rect (screenWidth/2 - resultTexture.width/2 + 420, screenHeight/2 + 40, 200, 60), resultScoreLabel);
	}
	
	void updateGameState()
	{
		if(roundCount == 3)
		{
			textRoundNum = string.Format(" ");
			textScore = string.Format(" ");
			resultScoreLabel = string.Format(score.ToString());
			resultTimeLabel = tScript.getTimeString();
			//invisible UI
			timeTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			scoreTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			resultTexture = (Texture)Resources.Load("LabelResult",typeof(Texture));
			Time.timeScale = 0;
		}
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
