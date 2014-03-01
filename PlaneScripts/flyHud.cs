using UnityEngine;
using System.Collections;

public class flyHud : MonoBehaviour {

    private int screenWidth;
    private int screenHeight;
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
        GUI.skin.label.fontSize = 18;
        GUI.Label(new Rect(screenWidth - 370, screenHeight - 50, 350, 30), "提示：小键盘5下降，2爬升，1左转，3右转");
    }
}
