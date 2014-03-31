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
	public Texture roundNumTexture;
	//result Label
	private string resultTimeLabel;
	private string resultScoreLabel;
	//button area
	private int buttonListHeight = 150;
	private int buttonListWidth = 450;
	//gameover?
	private bool gameIsOver = false;
	private int gameOverCount = 0;
	//startTrigger
	private GameObject startTrigger;
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		roundCount = 0;
		roundNum = 1;
		gameIsOver = false;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		scScript = (selectCar)Camera.main.GetComponent(typeof(selectCar));
		tScript = (Timer)Camera.main.GetComponent(typeof(Timer));
		resultTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
		resultTimeLabel = string.Format("");
		resultScoreLabel = string.Format("");
		startTrigger = GameObject.Find("startTrigger");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!gameIsOver)
			Time.timeScale = 1.0f;
		currentCarID = scScript.getCurrentCarID();
		updateGameState();
        //Debug.Log(roundNum);
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		if(!gameIsOver)
		{
			textScore = string.Format(score.ToString());
			switch(currentCarID)
			{
			case 0:
				if(startTrigger.tag == "raceOne")
				{
					textRoundNum = string.Format((roundNum-1).ToString() + " / 1");
					if(roundNum == 2) roundCount = 3;
				}
				else
				{
					if(roundNum == 1)
					{
						textRoundNum = string.Format("1 / 2");
						roundCount = 1;
					}
					else if(roundNum == 7)
					{
						textRoundNum = string.Format("2 / 2");
						roundCount = 2;
					}
					else if(roundNum == 13)
					{
						//load Result Scene
						roundCount = 3;
					}
				}
				break;
			case 1:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 2) roundCount = 3;
                }
                else
                {
                    textRoundNum = string.Format(roundNum.ToString() + " / 2");
                    if (roundNum == 3) roundCount = 3;
                }
				break;
            case 2:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 2) roundCount = 3;
                }
                else
                {
                    textRoundNum = string.Format(roundNum.ToString() + " / 2");
                    if (roundNum == 3) roundCount = 3;
                }
                break;
            case 3:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 2) roundCount = 3;
                }
                else
                {
                    textRoundNum = string.Format(roundNum.ToString() + " / 2");
                    if (roundNum == 3) roundCount = 3;
                }
                break;
            case 4:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 2) roundCount = 3;
                }
                else
                {
                    textRoundNum = string.Format(roundNum.ToString() + " / 2");
                    if (roundNum == 3) roundCount = 3;
                }
                break;
            case 5:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 6) roundCount = 3;
                }
                else
                {
                    if (roundNum == 1)
                    {
                        textRoundNum = string.Format("1 / 2");
                        roundCount = 1;
                    }
                    else if (roundNum == 6)
                    {
                        textRoundNum = string.Format("2 / 2");
                        roundCount = 2;
                    }
                    else if (roundNum == 10 || roundNum == 11)
                    {
                        //load Result Scene
                        roundCount = 3;
                    }
                }
                break;
            case 6:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 2) roundCount = 3;
                }
                else
                {
                    textRoundNum = string.Format(roundNum.ToString() + " / 2");
                    if (roundNum == 3) roundCount = 3;
                }
                break;
            case 7:
                if (startTrigger.tag == "raceOne")
                {
                    textRoundNum = string.Format((roundNum - 1).ToString() + " / 1");
                    if (roundNum == 6) roundCount = 3;
                }
                else
                {
                    if (roundNum == 1)
                    {
                        textRoundNum = string.Format("1 / 2");
                        roundCount = 1;
                    }
                    else if (roundNum == 6)
                    {
                        textRoundNum = string.Format("2 / 2");
                        roundCount = 2;
                    }
                    else if (roundNum == 10 || roundNum == 11)
                    {
                        //load Result Scene
                        roundCount = 3;
                    }
                }
                break;
			default:
				break;
			}
		}
		GUI.Label (new Rect (screenWidth - 170, 20, 200, 60), textScore);
		GUI.Label (new Rect (180,20,500,60), textRoundNum);
		GUI.skin.label.fontSize = 62;
		//exitButton
        //if(GUI.Button(new Rect(screenWidth - 60,screenHeight-50,40,40),"ESC"))
        //{
        //    Application.Quit();
        //}
		//static UI
		GUI.DrawTexture(new Rect(screenWidth/2 - timeTexture.width/2 - 40,20,timeTexture.width*2/3,timeTexture.height*2/3), timeTexture, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(new Rect(screenWidth - scoreTexture.width - 80,20,scoreTexture.width*2/3,scoreTexture.height), scoreTexture, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(new Rect(20,20,roundNumTexture.width*2/3,roundNumTexture.height), roundNumTexture, ScaleMode.StretchToFill, true, 0);
		//Dynamic UI
		GUI.DrawTexture(new Rect(screenWidth/2 - resultTexture.width/2 - 100,screenHeight/2 - resultTexture.height/2,resultTexture.width*4/5,resultTexture.height*4/5), resultTexture, ScaleMode.StretchToFill, true, 0);
		GUI.Label(new Rect (screenWidth/2 - resultTexture.width/2 + 330, screenHeight/2 - 120, 350, 60), resultTimeLabel);
		GUI.Label(new Rect (screenWidth/2 - resultTexture.width/2 + 330, screenHeight/2, 200, 60), resultScoreLabel);
		if(gameIsOver)
		{
			textRoundNum = string.Format(" ");
			textScore = string.Format(" ");
			tScript.clearTimeString();
			GUILayout.BeginArea(new Rect(screenWidth/2-128,screenHeight/2 + 100,
				buttonListWidth,buttonListHeight));
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(" ",GUILayout.Height(128),GUILayout.Width(256)))
			{
				scScript.setIsCarSelected(false);
				scScript.setIsColorSelected(false);
				Application.LoadLevel(0);
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
	}
	
	void updateGameState()
	{
		if(roundCount == 3)
		{
			gameIsOver = true;
			textRoundNum = string.Format(" ");
			textScore = string.Format(" ");
			//tScript.clearTimeString();
			resultScoreLabel = string.Format(score.ToString());
			resultTimeLabel = tScript.getTimeString();
			//invisible UI
			timeTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			scoreTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			roundNumTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			resultTexture = (Texture)Resources.Load("LabelResult",typeof(Texture));
			gameOverCount++;
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
	
	public bool getGameOver()
	{
		return gameIsOver;
	}
}
