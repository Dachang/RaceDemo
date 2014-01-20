using UnityEngine;
using System.Collections;

public class planeSelectViewController : MonoBehaviour {
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
	
	private int RACEBTN_THREE_MARGIN_LEFT;
	
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
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_ONE_WIDTH,RACEBTN_ONE_HEIGHT), "Plane1"))
		{
			PlayerPrefs.SetInt("planeID",0);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane2"))
		{
			PlayerPrefs.SetInt("planeID",1);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_THREE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane3"))
		{
			PlayerPrefs.SetInt("planeID",2);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane4"))
		{
			PlayerPrefs.SetInt("planeID",3);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane5"))
		{
			PlayerPrefs.SetInt("planeID",4);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_THREE_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane6"))
		{
			PlayerPrefs.SetInt("planeID",5);
			Application.LoadLevel("Scene1");
		}
	}
	
	void initUIPosition()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		RACEBTN_ONE_MARGIN_LEFT = screenWidth/2 - RACEBTN_ONE_WIDTH - 160;
		RACEBTN_ONE_MARGIN_UP = screenHeight/2 - RACEBTN_ONE_HEIGHT - 20;
		
		RACEBTN_TWO_MARGIN_LEFT = screenWidth/2 - 60;
		RACEBTN_TWO_MARGIN_UP = screenHeight/2 + 20;
		
		RACEBTN_THREE_MARGIN_LEFT = screenWidth/2 + 160;
		
		RETURN_MARGIN_LEFT = screenWidth/2 - RETURN_WIDTH/2;
		RETURN_MARGIN_UP = screenHeight - RETURN_HEIGHT - 30;
	}
}
