using UnityEngine;
using System.Collections;

public class RaceSelectViewController : MonoBehaviour {
	
	private int screenWidth;
	private int screenHeight;
	
	private int RACEBTN_ONE_MARGIN_LEFT;
	private int RACEBTN_ONE_MARGIN_UP;
	private int RACEBTN_ONE_WIDTH = 120;
	private int RACEBTN_ONE_HEIGHT = 120;
	
	private int RACEBTN_TWO_MARGIN_LEFT;
	private int RACEBTN_TWO_MARGIN_UP;
    private int RACEBTN_TWO_WIDTH = 120;
	private int RACEBTN_TWO_HEIGHT = 120;
	
	private int RETURN_MARGIN_LEFT;
	private int RETURN_MARGIN_UP;
    private int RETURN_WIDTH = 90;
	private int RETURN_HEIGHT = 40;
	
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
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_ONE_WIDTH,RACEBTN_ONE_HEIGHT), "Race1"))
		{
			//load Race 1
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Race2"))
		{
			//load Race 2
			Application.LoadLevel("speedway");
		}
		if(GUI.Button(new Rect(RETURN_MARGIN_LEFT, RETURN_MARGIN_UP,RETURN_WIDTH,RETURN_HEIGHT), "Return"))
		{
			//return
			Application.LoadLevel(0);
		}
	}
	
	void initUIPosition()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		RACEBTN_ONE_MARGIN_LEFT = screenWidth/2 - RACEBTN_ONE_WIDTH - 20;
		RACEBTN_ONE_MARGIN_UP = screenHeight/2 - RACEBTN_ONE_HEIGHT/2;
		
		RACEBTN_TWO_MARGIN_LEFT = screenWidth/2 + 20;
		RACEBTN_TWO_MARGIN_UP = screenHeight/2 - RACEBTN_ONE_HEIGHT/2;
		
		RETURN_MARGIN_LEFT = screenWidth/2 - RETURN_WIDTH/2;
		RETURN_MARGIN_UP = screenHeight - RETURN_HEIGHT - 30;
	}
}
