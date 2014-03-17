using UnityEngine;
using System.Collections;

public class BuildingBehaviour : MonoBehaviour {

    private GameObject building_B;
    
    private GameObject buildingTrigger_B;
    private TriggerBehaviour mbScript;

    private GameObject buildingBG;
    private MapBehaviour mScript;

    private GameObject replaceBuilding;

    private SmoothCamera camScript;

    private Transform originalBuilingTransform;
    private Transform targetTransform;

    private float smooth = 5.0f;
    private bool animateStart = false;

    private bool hasAlreadyReplaced = false;
    private bool showBuildingUIView = false;

    private bool buildingHasAppeared = false;
	// Use this for initialization
	void Start () 
    {
        building_B = GameObject.Find("fake_building_B");
        buildingTrigger_B = GameObject.Find("BuildingBTrigger");
        buildingBG = GameObject.Find("factory_area");
        replaceBuilding = GameObject.Find("building_B_2");

        mScript = (MapBehaviour)buildingBG.GetComponent(typeof(MapBehaviour));
        mbScript = (TriggerBehaviour)buildingTrigger_B.GetComponent(typeof(TriggerBehaviour));
        camScript = (SmoothCamera)Camera.main.GetComponent(typeof(SmoothCamera));

        originalBuilingTransform = this.transform;
        targetTransform = building_B.transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (animateStart)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetTransform.position, Time.deltaTime * smooth);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetTransform.rotation, Time.deltaTime * smooth);
            if (Mathf.Abs(this.transform.position.x - targetTransform.position.x) < 0.01f)
            { 
                building_B.SetActive(true);
                foreach (Transform child in transform)
                    child.gameObject.renderer.enabled = false;
                camScript.setCameraTriggered(false);
            }
        }
        else
        {
            building_B.SetActive(false);
        }
	}

    void OnGUI()
    {
        if (showBuildingUIView)
        {
            if (GUI.Button(new Rect(Screen.width - 60, 20, 40, 40), "esc"))
            {
                animateStart = false;
                showBuildingUIView = false;
                mScript.setMapIsAbleToDrag(true);
                camScript.setCameraTriggered(true);
                foreach (Transform child in transform)
                    child.gameObject.renderer.enabled = true;
                this.transform.position = replaceBuilding.transform.position;
                this.transform.rotation = replaceBuilding.transform.rotation;
            }
        }
    }

    void OnMouseDown()
    {
        if (mbScript.getBuildingIsTriggered())
        {
            if (GameObject.Find("building_B_2"))
            {
                //disable map dragging
                mScript.setMapIsAbleToDrag(false);
                //animation
                animateStart = true;
                showBuildingUIView = true;
            }
        }
    }

    public bool isAnimationStart()
    {
        return animateStart;
    }

    public bool hasShowBuildingUIView()
    {
        return showBuildingUIView;
    }
}
