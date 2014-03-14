using UnityEngine;
using System.Collections;

public class sendMailButton : MonoBehaviour {

    public GUISkin mySkin;
    public GUISkin mySkin2;
    private SendEmailFunction smfScript;

    private int screenWidth;
    private int screenHeight;

    private bool sendButtonIsActive;
	// Use this for initialization
	void Start () 
    {
        smfScript = (SendEmailFunction)Camera.main.GetComponent(typeof(SendEmailFunction));
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        sendButtonIsActive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = mySkin;

        if (GUI.Button(new Rect(screenWidth - 260, screenHeight - 150, 210, 110), " "))
        {
            if (sendButtonIsActive)
            {
                smfScript.SendEmail();
                sendButtonIsActive = false;
            }
        }

        GUI.skin = mySkin2;
        if (GUI.Button(new Rect(screenWidth - 260, screenHeight - 290, 210, 110), " "))
        {
            Application.LoadLevel("FlyScene");
        }
    }

    //public interface
    public void activateSendButton()
    {
        sendButtonIsActive = true;
    }
}
