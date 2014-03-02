using UnityEngine;
using System.Collections;

public class raceHud : MonoBehaviour {

    private int screenHeight;
    private int screenWidth;
	// Use this for initialization
	void Start () 
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin.label.fontSize = 24;
        GUI.Label(new Rect(20, screenHeight - 70, 470, 50), "提示：小键盘5前进，2后退，1左转，3右转");
    }
}
