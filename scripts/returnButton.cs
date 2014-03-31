using UnityEngine;
using System.Collections;

public class returnButton : MonoBehaviour {

    private int screenWidth;
    private int screenHeight;

    public GUISkin mySkin;
	// Use this for initialization
	void Start () 
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = mySkin;
        if (GUI.Button(new Rect(screenWidth - 90, screenHeight - 90, 80, 80), " "))
        {
            Application.LoadLevel(0);
            //Application.Quit();
        }
    }
}
