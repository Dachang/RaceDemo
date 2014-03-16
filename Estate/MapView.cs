using UnityEngine;
using System.Collections;

public class MapView : MonoBehaviour {

    private int screenWidth;
    private int screenHeight;

    private bool showBuildingView = false;

    private GameObject buildingB;
    private BuildingBehaviour bScript;

	// Use this for initialization
	void Start () 
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        buildingB = GameObject.Find("building_B");
        bScript = (BuildingBehaviour)buildingB.GetComponent(typeof(BuildingBehaviour));
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (bScript.isAnimationStart())
        {
            showBuildingView = !showBuildingView;
        }
	}

    void OnGUI()
    {
        if (showBuildingView)
        {
            if (GUI.Button(new Rect(screenWidth - 60, 20, 40, 40), "esc"))
            {
                //add
            }
        }
    }
}
