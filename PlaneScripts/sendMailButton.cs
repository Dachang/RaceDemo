using UnityEngine;
using System.Collections;

public class sendMailButton : MonoBehaviour {

    public GUISkin mySkin;
    private SendEmailFunction smfScript;

    private int screenWidth;
    private int screenHeight;
	// Use this for initialization
	void Start () 
    {
        smfScript = (SendEmailFunction)Camera.main.GetComponent(typeof(SendEmailFunction));
        screenWidth = Screen.width;
        screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = mySkin;

        if (GUI.Button(new Rect(screenWidth - 260, screenHeight - 150, 210, 110), " "))
        {
            smfScript.SendEmail();
        }
    }
}
