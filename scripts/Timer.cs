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
	public string timeStringToPass;
	
	public bool countDownHasEnded = false;
    public bool fakeCountDownHasEnded = false;
	
	//car script
	private Car carScript;
	private selectCar scScript;
	private scoreManager smScript;
	
	private int currentCarID;
	private GameObject mainCar;
	
	private Texture countDownTexture;
	
	public GUISkin mySkin;
	
	//gameover?
	private bool gameOver = false;
	
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
		smScript = (scoreManager)Camera.main.GetComponent(typeof(scoreManager));
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
        case 2:
            mainCar = GameObject.Find("che003");
            carScript = (Car)mainCar.GetComponent("Car");
            break;
        case 3:
            mainCar = GameObject.Find("che004");
            carScript = (Car)mainCar.GetComponent("Car");
            break;
        case 4:
            mainCar = GameObject.Find("che005");
            carScript = (Car)mainCar.GetComponent("Car");
            break;
        case 5:
            mainCar = GameObject.Find("che006");
            carScript = (Car)mainCar.GetComponent("Car");
            break;
        case 6:
            mainCar = GameObject.Find("che007");
            carScript = (Car)mainCar.GetComponent("Car");
            break;
        case 7:
            mainCar = GameObject.Find("che008");
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
		GUI.skin = mySkin;
		gameOver = smScript.getGameOver();
		countDownToStart();
        fakeCountDownToStart();
		if(countDownHasEnded)
		{
			//GUI
			countDownTexture = countDownTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			countDownText = string.Format(" ");
			float guiTime = Time.time - beginTime;
			int minutes = (int)(guiTime / 60);
			int seconds = (int)(guiTime % 60);
			int fraction = (int)((guiTime * 100) % 100);
			if(!gameOver)
			{
				textTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
				timeStringToPass = textTime;
			}
			else
			{
				textTime = string.Format(" ");
			}
			GUI.Label (new Rect (screenWidth/2+10, 16, 350, 60), textTime);
			GUI.skin.label.fontSize = 62;
		}
		else
		{
			//GUI
			if(countDownSeconds == 0) countDownTexture = (Texture)Resources.Load("LabelThree",typeof(Texture));
			else if(countDownSeconds == 1) countDownTexture = (Texture)Resources.Load("LabelTwo",typeof(Texture));
			else if(countDownSeconds == 2) countDownTexture = (Texture)Resources.Load("LabelOne",typeof(Texture));
			else countDownTexture = countDownTexture = (Texture)Resources.Load("LabelStart",typeof(Texture));
			GUI.skin.label.fontSize = 62;
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

    void fakeCountDownToStart()
    {
        if (!fakeCountDownHasEnded)
        {
            float assumeTime = Time.time - startTime;
            int fakeCountDownSeconds = (int)(assumeTime % 60);
            if (fakeCountDownSeconds >= 10)
            {
                fakeCountDownHasEnded = true;
            }
        }
    }
	
	//public interfaces
	public string getTimeString()
	{
		return timeStringToPass;
	}
	
	public void clearTimeString()
	{
		textTime = string.Format(" ");
	}

    public bool threeSecondsEnd()
    {
        return countDownHasEnded;
    }

    public bool hasCountDownEnded() 
    {
        return fakeCountDownHasEnded;
    }

    public void endFakeCountDown()
    {
        fakeCountDownHasEnded = true;
    }
}
