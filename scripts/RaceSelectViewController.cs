using UnityEngine;
using System.Collections;

public class RaceSelectViewController : MonoBehaviour {
	
	private int screenWidth;
	private int screenHeight;
	
	private int RACEBTN_ONE_MARGIN_LEFT;
	private int RACEBTN_ONE_MARGIN_UP;
	private int RACEBTN_ONE_WIDTH = 560;
	private int RACEBTN_ONE_HEIGHT = 400;
	
	private int RACEBTN_TWO_MARGIN_LEFT;
	private int RACEBTN_TWO_MARGIN_UP;
    private int RACEBTN_TWO_WIDTH = 560;
	private int RACEBTN_TWO_HEIGHT = 400;
	
	private int RETURN_MARGIN_LEFT;
	private int RETURN_MARGIN_UP;
    private int RETURN_WIDTH = 288;
	private int RETURN_HEIGHT = 142;

    public GUISkin returnBtnSkin;
	
	// Use this for initialization
	void Start () 
	{
		initUIPosition();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
        GUI.skin = returnBtnSkin;
        if (GUI.Button(new Rect(RETURN_MARGIN_LEFT, RETURN_MARGIN_UP, RETURN_WIDTH, RETURN_HEIGHT), " "))
        {
            //return
            Application.LoadLevel(0);
        }
		GUI.color = new Color(1.0f,1.0f,1.0f,.0f);
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_ONE_WIDTH,RACEBTN_ONE_HEIGHT), "Race1"))
		{
			//load Race 1
			Application.LoadLevel("RoadCourse");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Race2"))
		{
			//load Race 2
			Application.LoadLevel("speedway");
		}
		//exitButton
		if(GUI.Button(new Rect(screenWidth - 60,screenHeight-50,40,40),"ESC"))
		{
			Application.Quit();
		}
	}
	
	void initUIPosition()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		RACEBTN_ONE_MARGIN_LEFT = 70;
		RACEBTN_ONE_MARGIN_UP = screenHeight/2 - RACEBTN_ONE_HEIGHT/2;
		
		RACEBTN_TWO_MARGIN_LEFT = screenWidth/2 + 90;
		RACEBTN_TWO_MARGIN_UP = screenHeight/2 - RACEBTN_ONE_HEIGHT/2;
		
		RETURN_MARGIN_LEFT = screenWidth/2 - RETURN_WIDTH/2;
		RETURN_MARGIN_UP = screenHeight - RETURN_HEIGHT - 30;
	}
}
