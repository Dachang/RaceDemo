using UnityEngine;
using System.Collections;

public class carBehaviour : MonoBehaviour {
	
	private int dragSpeedX = -100;
	private int dragSpeedY = -100;
	private paintViewController pvc;
	private viewController vc;
	// Use this for initialization
	void Start () 
	{
		if(this.tag == "paintScene")
			pvc = (paintViewController)Camera.main.GetComponent(typeof(paintViewController));
		else
			vc = (viewController)Camera.main.GetComponent(typeof(viewController));
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnMouseDrag()
	{
		Debug.Log("Is dragging car");
		if(this.tag == "paintScene")
			pvc.pauseRotate();
		else
			vc.pauseRotate();
		this.transform.Rotate(Vector3.up*Time.deltaTime*Input.GetAxis("Mouse X")*dragSpeedX);
		this.transform.Rotate(Vector3.left*Time.deltaTime*Input.GetAxis("Mouse Y")*dragSpeedY);
	}
	
	void OnMouseUp()
	{
		if(this.tag == "paintScene")
			pvc.resumeRotate();
		else
			vc.resumeRotate();
	}
}
