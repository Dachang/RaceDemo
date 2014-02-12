using UnityEngine;
using System.Collections;

public class returnButton : MonoBehaviour {

    private int screenHeight;
    public GUISkin myskin;
	// Use this for initialization
	void Start () 
    {
        screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = myskin;
        if (GUI.Button(new Rect(10, screenHeight - 90, 80, 80), ""))
        {
            Application.LoadLevel(0);
        }
    }
}
