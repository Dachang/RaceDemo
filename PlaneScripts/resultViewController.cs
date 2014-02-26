using UnityEngine;
using System.Collections;

public class resultViewController : MonoBehaviour {

    public GUISkin mySkin;
    private int score;
	// Use this for initialization
	void Start () 
    {
        score = PlayerPrefs.GetInt("flyResult");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = mySkin;
        GUI.skin.label.fontSize = 60;
        GUI.Label(new Rect(745, 390, 500, 140), "你的分数是:" + score.ToString());
        if (GUI.Button(new Rect(750,640,450,140), " "))
        {
            //back to menu
            Application.LoadLevel(0);
        }
    }
}
