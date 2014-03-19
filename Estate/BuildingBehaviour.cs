using UnityEngine;
using System.Collections;

public class BuildingBehaviour : MonoBehaviour {

    public GUISkin mySkin;

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

    private int pageTag = 1;
    public Texture2D renderImageOne;
    public Texture2D renderImageTwo;
    public Texture2D renderImageThree;
    public Texture2D renderImageFour;
    public Texture2D renderImageFive;
    public Texture2D renderImageSix;
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
            camScript.setCameraTriggered(false);
            this.transform.position = Vector3.Lerp(this.transform.position, targetTransform.position, Time.deltaTime * smooth);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetTransform.rotation, Time.deltaTime * smooth);
            if (Mathf.Abs(this.transform.position.x - targetTransform.position.x) < 0.01f)
            { 
                building_B.SetActive(true);
                foreach (Transform child in transform)
                    child.gameObject.renderer.enabled = false;
            }
        }
        else
        {
            building_B.SetActive(false);
        }
	}

    void OnGUI()
    {
        GUI.skin = mySkin;
        float resizeFactor = Screen.width / 1920.0f;
        Debug.Log(resizeFactor);
        if (showBuildingUIView)
        {
            if (pageTag == 1)
            {
                GUI.DrawTexture(new Rect((float)(1920 / 2 - renderImageOne.width - 20) * resizeFactor,
                    (float)(1080 / 2 + 160) * resizeFactor, (float)renderImageOne.width * resizeFactor,
                    (float)renderImageOne.height * resizeFactor), renderImageOne, ScaleMode.StretchToFill, true, 0);
                GUI.DrawTexture(new Rect((float)(1920 / 2 + 20) * resizeFactor, (float)(1080 / 2 + 160) * resizeFactor,
                    (float)renderImageTwo.width * resizeFactor, (float)renderImageTwo.height * resizeFactor), 
                    renderImageTwo, ScaleMode.StretchToFill, true, 0);
            }
            else if (pageTag == 2)
            {
                GUI.DrawTexture(new Rect((float)(1920 / 2 - renderImageThree.width - 20) * resizeFactor,
                    (float)(1080 / 2 + 160) * resizeFactor, (float)renderImageThree.width * resizeFactor,
                    (float)renderImageThree.height * resizeFactor), renderImageThree, ScaleMode.StretchToFill, true, 0);
                GUI.DrawTexture(new Rect((float)(1920 / 2 + 20) * resizeFactor, (float)(1080 / 2 + 160) * resizeFactor,
                    (float)renderImageFour.width * resizeFactor, (float)renderImageFour.height * resizeFactor), 
                    renderImageFour, ScaleMode.StretchToFill, true, 0);
            }
            else if (pageTag == 3)
            {
                GUI.DrawTexture(new Rect((float)(1920 / 2 - renderImageFive.width - 20) * resizeFactor,
                    (float)(1080 / 2 + 160) * resizeFactor, (float)renderImageFive.width * resizeFactor,
                    (float)renderImageFive.height * resizeFactor), renderImageFive, ScaleMode.StretchToFill, true, 0);
                GUI.DrawTexture(new Rect((float)(1920 / 2 + 20) * resizeFactor, (float)(1080 / 2 + 160) * resizeFactor,
                    (float)renderImageSix.width * resizeFactor, (float)renderImageSix.height * resizeFactor), 
                    renderImageSix, ScaleMode.StretchToFill, true, 0);
            }

            if (GUI.Button(new Rect((float)(1920 / 2 + 400) * resizeFactor, 40.0f * resizeFactor, 
                80.0f * resizeFactor, 80.0f * resizeFactor), ""))
            {
                animateStart = false;
                showBuildingUIView = false;
                mScript.setMapIsAbleToDrag(true);
                camScript.setCameraTriggered(true);
                camScript.setCameraAdjusted(false);
                foreach (Transform child in transform)
                    child.gameObject.renderer.enabled = true;
                this.transform.position = replaceBuilding.transform.position;
                this.transform.rotation = replaceBuilding.transform.rotation;
                pageTag = 1;
            }
            if (GUI.Button(new Rect((float)(1920 / 2 - 470) * resizeFactor, (float)(1080 / 2 + 270) * resizeFactor,
                80.0f * resizeFactor, 80.0f * resizeFactor), ""))
            {
                if (pageTag > 1) pageTag--;
            }
            if (GUI.Button(new Rect((float)(1920 / 2 + 390) * resizeFactor, (float)(1080 / 2 + 270) * resizeFactor,
                80.0f * resizeFactor, 80.0f * resizeFactor), ""))
            {
                if (pageTag < 3) pageTag++;
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
