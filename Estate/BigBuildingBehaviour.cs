using UnityEngine;
using System.Collections;

public class BigBuildingBehaviour : MonoBehaviour {

    private int autoSpeed = 10;
    private int dragSpeedX = -50;
    private int dragSpeedY = 50;

    private bool isAbleToRotate = true;
    private bool isDraggingHorizontal = false;
    private bool isDraggingVertical = false;

    private float resizeFactor = 0.01f;
    private float cameraMoveFactor = 0.01f;

    private GameObject buildingBG;
    private MapBehaviour mScript;

    private Transform originalTransform;
    private Transform originalCameraTransform;

    private float smooth = 0.5f;

	void Start () 
    {
        buildingBG = GameObject.Find("factory_area");
        mScript = (MapBehaviour)buildingBG.GetComponent(typeof(MapBehaviour));
        originalTransform = this.transform;
        originalCameraTransform = Camera.main.transform;
	}
	
	void Update () 
    {
        //if (isAbleToRotate)
        //{
        //    this.transform.Rotate(Vector3.up * Time.deltaTime * autoSpeed);
        //}
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.transform.localScale.x >= 0.015f)
        {
            this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * resizeFactor;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.transform.localScale.x <= 0.018f)
        {
            this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * resizeFactor;
        }
	}

    void OnMouseDrag()
    {
        isAbleToRotate = false;
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y")) && !isDraggingVertical)
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse X") * dragSpeedX);
            isDraggingHorizontal = true;
        }
        else if (Mathf.Abs(Input.GetAxis("Mouse X")) < Mathf.Abs(Input.GetAxis("Mouse Y")) && !isDraggingHorizontal)
        {
            if (Mathf.Abs(Camera.main.transform.localPosition.y) > 1.3f &&
                Mathf.Abs(Camera.main.transform.localPosition.y) < 1.8f)
            {
                Camera.main.transform.Rotate(Vector3.left * Time.deltaTime * Input.GetAxis("Mouse Y") * dragSpeedY);
                Camera.main.transform.localPosition -= new Vector3(0, Input.GetAxis("Mouse Y") * cameraMoveFactor, 0);
            }
            else
            {
                fixCameraPosition();
            }
            isDraggingVertical = true;
        }
    }

    void OnMouseUp()
    {
        isDraggingHorizontal = false;
        isDraggingVertical = false;
    }

    void fixCameraPosition()
    {
        Vector3 targetPos = new Vector3(0, 1.7f, -2.4f);
        Quaternion targetRot = new Quaternion(0.4f, 0, 0, 0.9f);
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPos,
            Time.deltaTime * smooth);
        Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, targetRot,
            Time.deltaTime * smooth);
    }
}
