using UnityEngine;
using System.Collections;

public class DisplayGameData : MonoBehaviour {

    public GUISkin myskin;

    private float height = Screen.height;
    private float width = Screen.width;

    private float buttonListHeight = 150;
    private float buttonListWidth = 450;

    private int myscore;
	
	//UI texture
	public Texture scoreTexture;
	public Texture timeTexture;
	public Texture resultTexture;
	//UI Label
	private string timeString;
	private string scoreString;
	private string resultScoreLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {	
        GameObject plane = GameObject.Find("Player");
        FlyMovement fly = plane.GetComponent<FlyMovement>();
        CountTime countTimeScript = this.gameObject.GetComponent<CountTime>();
		
		if(!countTimeScript.isTimeOutFunction())
			Time.timeScale = 1.0f;
        
        GUI.skin = myskin;
		GUI.skin.label.fontSize = 70;
        GUILayout.BeginArea(new Rect (0, 0, Screen.width + 500, Screen.height));
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(100);
        //GUILayout.Label(countTimeScript.getTimeString(),GUILayout.Width(500));
        GUILayout.FlexibleSpace();
//      GUILayout.Label("分数 :  " + countScoreScript.getStringScore());
        GUILayout.Space(100);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        //Time & Score Label
		myscore = fly.getIntScore();
		timeString = countTimeScript.getTimeString();
		scoreString = myscore.ToString();

        if (countTimeScript.isTimeOutFunction())
        {
			timeTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			scoreTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
			resultTexture = (Texture)Resources.Load("LabelResult",typeof(Texture));
			resultScoreLabel = scoreString;
			timeString = string.Format(" ");
			scoreString = string.Format(" ");
			
			GUILayout.BeginArea(new Rect(width/2 - 64,height/2,
				buttonListWidth,buttonListHeight));
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(" ",GUILayout.Height(64),GUILayout.Width(128)))
			{
				Application.LoadLevel(0);
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
        }
		
		//static UI
		GUI.DrawTexture(new Rect(width - scoreTexture.width - 180,20,scoreTexture.width,scoreTexture.height), scoreTexture, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(new Rect(20,20,timeTexture.width,timeTexture.height), timeTexture, ScaleMode.StretchToFill, true, 0);
		GUI.Label (new Rect (280,30,500,100), timeString);
		GUI.Label (new Rect (width - 170, 30, 200, 100), scoreString);
		//dynamic UI
		GUI.DrawTexture(new Rect(width/2 - resultTexture.width/2 - 50,height/2 - resultTexture.height/2 - 150,
			resultTexture.width*4/5,resultTexture.height*4/5), resultTexture, ScaleMode.StretchToFill, true, 0);
		GUI.Label(new Rect (width/2 - resultTexture.width/2 + 380, height/2 - 200, 200, 100), resultScoreLabel);
		
		if (countTimeScript.isTimeOutFunction())
			Time.timeScale = 0;
		
		//exitButton
		if(GUI.Button(new Rect(10,height-60,50,50)," "))
		{
			Application.Quit();
		}
        
    }
}
