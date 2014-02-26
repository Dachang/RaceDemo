using UnityEngine;
using System.Collections;

public class failViewController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.color = new Color(1.0f, 1.0f, 1.0f, .0f);
        if (GUI.Button(new Rect(650,680,230,120), " "))
        {
            //send mail again
            Application.LoadLevel("Mail");
        }
        if (GUI.Button(new Rect(1055, 680, 230, 120), ""))
        {
            //goto fly scene
            Application.LoadLevel("FlyScene");
        }
    }
}
