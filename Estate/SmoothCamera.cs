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

    private GameObject uiBG;

    private float currentHandPosX, currentHandPosY, lastHandPosX, lastHandPosY;
    private float MOUSE_SMOOTH_FACTOR_X = 0.5f;
    private float MOUSE_SMOOTH_FACTOR_Y = 0.5f;
    private float HAND_POS_SCALE = 20f;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (rigidbody)
            rigidbody.freezeRotation = true;

        buildingBG = GameObject.Find("moxing_0402");
        currentBuilding = GameObject.Find("buildingB");
        bScript = (BuildingBehaviour)currentBuilding.GetComponent(typeof(BuildingBehaviour));
        //uiBG = GameObject.Find("UIBG");
    }

    void Update()
    {
        currentHandPosX = pxsLeapInput.m_Frame.Hands[0].StabilizedPalmPosition.x;
        currentHandPosY = pxsLeapInput.m_Frame.Hands[0].StabilizedPalmPosition.y * HAND_POS_SCALE;
        int fingerCount = pxsLeapInput.m_Frame.Fingers.Count;
        Debug.Log(fingerCount);
        if (target && cameraTriggered && fingerCount > 2)
        {
            x += (currentHandPosX - lastHandPosX) * xSpeed * Time.deltaTime * MOUSE_SMOOTH_FACTOR_X;
            y -= (currentHandPosY - lastHandPosY) * -0.05f * ySpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
        }
        lastHandPosX = currentHandPosX;
        lastHandPosY = currentHandPosY;
        //checkScale();
    }

    void LateUpdate()
    {
        float tempBufferX = 0; 
        float tempBufferY = 0;
        if (target && cameraTriggered)
        {
            tempBufferX += pxsLeapInput.GetHandAxisRaw("Horizontal") * xSpeed * 0.02f;
            tempBufferY -= pxsLeapInput.GetHandAxisRaw("Vertical") * ySpeed * 0.02f;

            tempBufferY = ClampAngle(tempBufferY, yMinLimit, yMaxLimit);

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
                uiBG.SetActive(true);
                Camera.main.transform.position = new Vector3(0.0f, 2.0f, -3.3f);
                Camera.main.transform.rotation = new Quaternion(0.2f, 0, 0, 1.0f);
                cameraAdjusted = true;
            }
            //target = targetBuilding.transform;
            Camera.main.fieldOfView = 60.0f;
            //distance = 1.0f;
        }
        else
        {
            uiBG.SetActive(false);
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

