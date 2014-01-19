using UnityEngine;
using System.Collections;

public class carBehaviour : MonoBehaviour {
	
	private int dragSpeedX = -100;
	private viewController vc;
	// Use this for initialization
	void Start () 
	{
		//vc = (viewController)Camera.main.GetComponent(typeof(viewController));
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
	}
	
	void OnMouseDrag()
	{
		Debug.Log("Is dragging car");
		//vc.pauseRotate();
		this.transform.Rotate(Vector3.up*Time.deltaTime*Input.GetAxis("Mouse X")*dragSpeedX);
		//transform.position += Vector3.up * Time.deltaTime*Input.GetAxis("Mouse Y") * dragSpeedY;
	}
	
	void OnMouseUp()
	{
		//vc.resumeRotate();
	}
}
