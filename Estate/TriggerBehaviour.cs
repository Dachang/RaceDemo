using UnityEngine;
using System.Collections;

public class TriggerBehaviour : MonoBehaviour {

    private GameObject building;
    private GameObject triggerTarget;
    private GameObject replaceBuilding;
    private bool buildingIsTriggered = false;

    private BuildingBehaviour bScript;

    private float appearSpeed = 0.3f;
    // Use this for initialization
    void Start()
    {
        building = GameObject.Find("building_B");
        bScript = (BuildingBehaviour)building.GetComponent(typeof(BuildingBehaviour));
        triggerTarget = GameObject.Find("BuildingBTriggerTarget");
        replaceBuilding = GameObject.Find("building_B_2");
    }

    // Update is called once per frame
    void Update()
    {
        if (buildingIsTriggered && building.transform.position.y <= 0.03)
        {
            building.transform.Translate(Vector3.up * Time.deltaTime * appearSpeed);
        }
        this.transform.position = triggerTarget.transform.position;
        if (Mathf.Abs(building.transform.position.y - replaceBuilding.transform.position.y) < 0.03f)
        {
            replaceBuilding.SetActive(true);
        }
        else
        {
            if (!bScript.isAnimationStart())  replaceBuilding.SetActive(false);
            else replaceBuilding.SetActive(true);
        }
    }

    void OnMouseDown()
    {
        buildingIsTriggered = true;
        this.renderer.enabled = false;
    }

    public bool getBuildingIsTriggered()
    {
        return buildingIsTriggered;
    }
}
