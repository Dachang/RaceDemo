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
    //page tag
    private int pageTag = 0;
    //UI BG
    private GameObject UIBG;
    public Texture pageOneTexture;
    public Texture pageTwoTexture;
    public Texture pageThreeTexture;
    public Texture pageFourTexture;
	
	// Use this for initialization
	void Start () 
	{
		initUIPosition();
        UIBG = GameObject.Find("Plane");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Time.timeScale = 1.0f;
        UIBG.renderer.material.mainTexture = changeUIBG(pageTag);
	}
	
	void OnGUI()
	{
		GUI.color = new Color(1.0f,1.0f,1.0f,.0f);
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_ONE_WIDTH,RACEBTN_ONE_HEIGHT), "Plane1"))
		{
			audio.PlayOneShot(clickSound);
            if (pageTag == 0)
                PlayerPrefs.SetInt("planeID", 0);
            else if (pageTag == 1)
                PlayerPrefs.SetInt("planeID", 6);
            else if (pageTag == 2)
                PlayerPrefs.SetInt("planeID", 12);
            else if (pageTag == 3)
                PlayerPrefs.SetInt("planeID", 18);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane2"))
		{
			audio.PlayOneShot(clickSound);
            if (pageTag == 0)
                PlayerPrefs.SetInt("planeID", 1);
            else if (pageTag == 1)
                PlayerPrefs.SetInt("planeID", 7);
            else if (pageTag == 2)
                PlayerPrefs.SetInt("planeID", 13);
            else if (pageTag == 3)
                PlayerPrefs.SetInt("planeID", 19);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_THREE_MARGIN_LEFT, RACEBTN_ONE_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane3"))
		{
			audio.PlayOneShot(clickSound);
            if (pageTag == 0)
                PlayerPrefs.SetInt("planeID", 2);
            else if (pageTag == 1)
                PlayerPrefs.SetInt("planeID", 8);
            else if (pageTag == 2)
                PlayerPrefs.SetInt("planeID", 14);
            else if (pageTag == 3)
                PlayerPrefs.SetInt("planeID", 20);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_ONE_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane4"))
		{
			audio.PlayOneShot(clickSound);
            if (pageTag == 0)
                PlayerPrefs.SetInt("planeID", 3);
            else if (pageTag == 1)
                PlayerPrefs.SetInt("planeID", 9);
            else if (pageTag == 2)
                PlayerPrefs.SetInt("planeID", 15);
            else if (pageTag == 3)
                PlayerPrefs.SetInt("planeID", 21);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_TWO_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane5"))
		{
			audio.PlayOneShot(clickSound);
            if (pageTag == 0)
                PlayerPrefs.SetInt("planeID", 4);
            else if (pageTag == 1)
                PlayerPrefs.SetInt("planeID", 10);
            else if (pageTag == 2)
                PlayerPrefs.SetInt("planeID", 16);
            else if (pageTag == 3)
                PlayerPrefs.SetInt("planeID", 22);
			Application.LoadLevel("Scene1");
		}
		if(GUI.Button(new Rect(RACEBTN_THREE_MARGIN_LEFT, RACEBTN_TWO_MARGIN_UP,RACEBTN_TWO_WIDTH,RACEBTN_TWO_HEIGHT), "Plane6"))
		{
			audio.PlayOneShot(clickSound);
            if (pageTag == 0)
                PlayerPrefs.SetInt("planeID", 5);
            else if (pageTag == 1)
                PlayerPrefs.SetInt("planeID", 11);
            else if (pageTag == 2)
                PlayerPrefs.SetInt("planeID", 17);
            else if (pageTag == 3)
                PlayerPrefs.SetInt("planeID", 23);
			Application.LoadLevel("Scene1");
		}
        //switch button
        if (GUI.Button(new Rect(SWITCH_BUTTON_LEFT_MARGIN, SWITCH_BUTTON_MARGIN_HEIGHT, SWITCH_BUTTON_WIDTH, SWITCH_BUTTON_HEIGHT), "<"))
        {
            audio.PlayOneShot(clickSound);
            //prev
            if (pageTag > 0 && pageTag <= 3)
            {
                pageTag--;
            }
        }
        if (GUI.Button(new Rect(SWITCH_BUTTON_RIGHT_MARGIN, SWITCH_BUTTON_MARGIN_HEIGHT, SWITCH_BUTTON_WIDTH, SWITCH_BUTTON_HEIGHT), ">"))
        {
            audio.PlayOneShot(clickSound);
            //next
            if (pageTag >= 0 && pageTag < 3)
            {
                pageTag++;
            }
        }
		//exitButton
		if(GUI.Button(new Rect(10,screenHeight-60,50,50),"ESC"))
		{
			Application.Quit();
		}
	}

    Texture changeUIBG(int pageNum)
    {
        switch (pageNum)
        {
            case 0:
                return pageOneTexture;
                break;
            case 1:
                return pageTwoTexture;
                break;
            case 2:
                return pageThreeTexture;
                break;
            case 3:
                return pageFourTexture;
                break;
            default:
                return pageOneTexture;
                break;
        }
    }
	
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

        SWITCH_BUTTON_WIDTH = 160;
        SWITCH_BUTTON_HEIGHT = 160;
        SWITCH_BUTTON_LEFT_MARGIN = 70;
        SWITCH_BUTTON_RIGHT_MARGIN = screenWidth - SWITCH_BUTTON_WIDTH - 70;
        SWITCH_BUTTON_MARGIN_HEIGHT = screenHeight / 2 - SWITCH_BUTTON_HEIGHT / 2 + 40;
	}
}
