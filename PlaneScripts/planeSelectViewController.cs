using UnityEngine;
using System.Collections;

public class planeSelectViewController : MonoBehaviour {
	private int screenWidth;
	private int screenHeight;
	
	private int RACEBTN_ONE_MARGIN_LEFT;
	private int RACEBTN_ONE_MARGIN_UP;
	private int RACEBTN_ONE_WIDTH = 410;
	private int RACEBTN_ONE_HEIGHT = 340;
	
	private int RACEBTN_TWO_MARGIN_LEFT;
	private int RACEBTN_TWO_MARGIN_UP;
    private int RACEBTN_TWO_WIDTH = 410;
	private int RACEBTN_TWO_HEIGHT = 340;
	
	private int RACEBTN_THREE_MARGIN_LEFT;
	
	private int RETURN_MARGIN_LEFT;
	private int RETURN_MARGIN_UP;
    private int RETURN_WIDTH = 90;
	private int RETURN_HEIGHT = 40;

    private int SWITCH_BUTTON_LEFT_MARGIN;
    private int SWITCH_BUTTON_RIGHT_MARGIN;
    private int SWITCH_BUTTON_WIDTH;
    private int SWITCH_BUTTON_HEIGHT;
    private int SWITCH_BUTTON_MARGIN_HEIGHT;
	
	//sound effect
	public AudioClip clickSound;
	
	// Use this for initialization
	void Start () 
	{
		initUIPosition();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Time.timeScale = 1.0f;
	}
	
	void OnGUI()
	{
		GUI.color = new Color(1.0f,1.0f,1.0f,.0f);
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_ONE_WIDTH,RACEBTN_ONE_HEIGHT), "Plane1"))
		{
			audio.PlayOneShot(clickSound);
			PlayerPrefs.SetInt("planeID",9);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane2"))
		{
			audio.PlayOneShot(clickSound);
			PlayerPrefs.SetInt("planeID",1);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_THREE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane3"))
		{
			audio.PlayOneShot(clickSound);
			PlayerPrefs.SetInt("planeID",2);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane4"))
		{
			audio.PlayOneShot(clickSound);
			PlayerPrefs.SetInt("planeID",3);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane5"))
		{
			audio.PlayOneShot(clickSound);
			PlayerPrefs.SetInt("planeID",4);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_THREE_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane6"))
		{
			audio.PlayOneShot(clickSound);
			PlayerPrefs.SetInt("planeID",5);
			Application.LoadLevel("Scene1");
		}
        //switch button
        if (GUI.Button(new Rect(SWITCH_BUTTON_LEFT_MARGIN, SWITCH_BUTTON_MARGIN_HEIGHT, SWITCH_BUTTON_WIDTH, SWITCH_BUTTON_HEIGHT), "<"))
        {
            //prev
        }
        if (GUI.Button(new Rect(SWITCH_BUTTON_RIGHT_MARGIN, SWITCH_BUTTON_MARGIN_HEIGHT, SWITCH_BUTTON_WIDTH, SWITCH_BUTTON_HEIGHT), ">"))
        {
            //next
        }
		//exitButton
		if(GUI.Button(new Rect(10,screenHeight-60,50,50),"ESC"))
		{
			Application.Quit();
		}
	}
	
	//void OnApplicationQuit()
	//{
	//	Application.CancelQuit();
	//	System.Diagnostics.Process.GetCurrentProcess().Kill();
	//}
	
	void initUIPosition()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		RACEBTN_ONE_MARGIN_LEFT = screenWidth/2 - RACEBTN_ONE_WIDTH - 290;
		RACEBTN_ONE_MARGIN_UP = screenHeight/2 - RACEBTN_ONE_HEIGHT;
		
		RACEBTN_TWO_MARGIN_LEFT = screenWidth/2 - 185;
		RACEBTN_TWO_MARGIN_UP = screenHeight/2 + 75;
		
		RACEBTN_THREE_MARGIN_LEFT = screenWidth/2 + 270;
		
		RETURN_MARGIN_LEFT = screenWidth/2 - RETURN_WIDTH/2;
		RETURN_MARGIN_UP = screenHeight - RETURN_HEIGHT - 30;

        SWITCH_BUTTON_WIDTH = 100;
        SWITCH_BUTTON_HEIGHT = 200;
        SWITCH_BUTTON_LEFT_MARGIN = 60;
        SWITCH_BUTTON_RIGHT_MARGIN = screenWidth - SWITCH_BUTTON_WIDTH - 60;
        SWITCH_BUTTON_MARGIN_HEIGHT = screenHeight / 2 - SWITCH_BUTTON_HEIGHT / 2;
	}
}
