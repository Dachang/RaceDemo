 using UnityEngine;
 using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    private GameObject buildingBG;
    private GameObject targetBuilding;
    private GameObject currentBuilding;
    private BuildingBehaviour bScript;

    private float camScaleFactor = 3f;

    private bool cameraTriggered = true;
    private bool cameraAdjusted = false;
    private Transform originalTransform;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (rigidbody)
            rigidbody.freezeRotation = true;

        buildingBG = GameObject.Find("factory_area");
        currentBuilding = GameObject.Find("building_B");
        bScript = (BuildingBehaviour)currentBuilding.GetComponent(typeof(BuildingBehaviour));
    }

    void Update()
    {
        Debug.Log(Camera.main.transform.position.ToString() + Camera.main.transform.rotation.ToString() +
            Camera.main.fieldOfView.ToString());
        if (GameObject.Find("fake_building_B"))
        {
            targetBuilding = GameObject.Find("fake_building_B");
        }

        if (target && Input.GetMouseButton(0) && cameraTriggered)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
        }

        switchTarget();
        checkScale();
    }

    void LateUpdate()
    {
        if (target && Input.GetMouseButton(0) && cameraTriggered)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    void switchTarget()
    {
        if (bScript.isAnimationStart())
        {
            if (!cameraAdjusted)
            {
                Camera.main.transform.position = new Vector3(0.0f, 1.6f, -2.4f);
                Camera.main.transform.rotation = new Quaternion(0.3f, 0, 0, 1.0f);
                cameraAdjusted = true;
            }
            target = targetBuilding.transform;
            Camera.main.fieldOfView = 55.0f;
            distance = 1.0f;
        }
        else
        {
            target = buildingBG.transform;
            distance = 3.5f;
        }
    }

    void checkScale()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraTriggered && Camera.main.fieldOfView <= 60.0f)
        {
            Camera.main.fieldOfView += camScaleFactor;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraTriggered && Camera.main.fieldOfView >= 30.0f)
        {
            Camera.main.fieldOfView -= camScaleFactor;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    public void setCameraTriggered(bool isTriggered)
    {
        cameraTriggered = isTriggered;
    }

    public void setCameraAdjusted(bool hasAdjusted)
    {
        cameraAdjusted = hasAdjusted;
    }
}

