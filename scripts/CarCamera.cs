using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour
{
	public Transform target = null;
	public float height = 1f;
	public float positionDamping = 3f;
	public float velocityDamping = 3f;
	public float distance = 4f;
	public LayerMask ignoreLayers = -1;

	private RaycastHit hit = new RaycastHit();

	private Vector3 prevVelocity = Vector3.zero;
	private LayerMask raycastLayers = -1;
	
	private Vector3 currentVelocity = Vector3.zero;
	private Timer timerScript;
	
	private selectCar scScript;
	private int currentCarID;
	
	void Start()
	{
		raycastLayers = ~ignoreLayers;
		timerScript = (Timer)Camera.main.GetComponent(typeof(Timer));
		scScript = (selectCar)Camera.main.GetComponent(typeof(selectCar));
	}
	
	void Update()
	{
		currentCarID = scScript.getCurrentCarID();
		switch(currentCarID)
		{
		case 0:
			if(!timerScript.countDownHasEnded)
				transform.LookAt(GameObject.Find("CenterofView").transform.position);
			target = GameObject.Find("CenterofView").transform;
			break;
		case 1:
			if(!timerScript.countDownHasEnded)
				transform.LookAt(GameObject.Find("CenterofView2").transform.position);
			target = GameObject.Find("CenterofView2").transform;
			break;
        case 2:
            if (!timerScript.countDownHasEnded)
                transform.LookAt(GameObject.Find("CenterofView3").transform.position);
            target = GameObject.Find("CenterofView3").transform;
            break;
        case 3:
            if (!timerScript.countDownHasEnded)
                transform.LookAt(GameObject.Find("CenterofView4").transform.position);
            target = GameObject.Find("CenterofView4").transform;
            break;
        case 4:
            if (!timerScript.countDownHasEnded)
                transform.LookAt(GameObject.Find("CenterofView5").transform.position);
            target = GameObject.Find("CenterofView5").transform;
            break;
        case 5:
            if (!timerScript.countDownHasEnded)
                transform.LookAt(GameObject.Find("CenterofView6").transform.position);
            target = GameObject.Find("CenterofView6").transform;
            break;
        case 6:
            if (!timerScript.countDownHasEnded)
                transform.LookAt(GameObject.Find("CenterofView7").transform.position);
            target = GameObject.Find("CenterofView7").transform;
            break;
        case 7:
            if (!timerScript.countDownHasEnded)
                transform.LookAt(GameObject.Find("CenterofView8").transform.position);
            target = GameObject.Find("CenterofView8").transform;
            break;
		default:
			break;
		}
        checkInput();
	}

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (timerScript.threeSecondsEnd())
            {
                timerScript.endFakeCountDown();
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (timerScript.threeSecondsEnd())
            {
                timerScript.endFakeCountDown();
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            if (timerScript.threeSecondsEnd())
            { 
                timerScript.endFakeCountDown(); 
            }
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            if (timerScript.threeSecondsEnd())
            {
                timerScript.endFakeCountDown();
            }
        }
    }

	void FixedUpdate()
	{
        if (timerScript.hasCountDownEnded())
        {
            currentVelocity = Vector3.Lerp(prevVelocity, target.root.rigidbody.velocity, velocityDamping * Time.deltaTime);
            currentVelocity.y = 0;
            prevVelocity = currentVelocity;
        }
	}
	
	void LateUpdate()
	{
        if (timerScript.hasCountDownEnded())
        {
            float speedFactor = Mathf.Clamp01(target.root.rigidbody.velocity.magnitude / 70.0f);
            //camera.fieldOfView = Mathf.Lerp(55, 72, speedFactor);
            float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);

            currentVelocity = currentVelocity.normalized;

            Vector3 newTargetPosition = target.position + Vector3.up * height;
            Vector3 newPosition = newTargetPosition - (currentVelocity * currentDistance);
            newPosition.y = newTargetPosition.y;

            Vector3 targetDirection = newPosition - newTargetPosition;
            if (Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
                newPosition = hit.point;

            transform.position = newPosition;
            transform.LookAt(newTargetPosition);
        }
	}
}
