using UnityEngine;
using System.Collections;

public class failViewController : MonoBehaviour {

    public GUISkin refillSkin, continueSkin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        //GUI.color = new Color(1.0f, 1.0f, 1.0f, .0f);
        GUI.skin = refillSkin;
        if (GUI.Button(new Rect(630,670,303,158), " "))
        {
            //send mail again
            Application.LoadLevel("Mail");
        }
        GUI.skin = continueSkin;
        if (GUI.Button(new Rect(1035, 670, 303, 158), ""))
        {
            //goto fly scene
            Application.LoadLevel("FlyScene");
        }
    }
}
