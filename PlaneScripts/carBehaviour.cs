using UnityEngine;
using System.Collections;

public class carBehaviour : MonoBehaviour {
	
	private int dragSpeedX = -100;
	private int dragSpeedY = -100;
	private paintViewController pvc;
	private viewController vc;
	
	private bool isDraggingVertical = false;
	private bool isDraggingHorizontal = false;
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
		if((Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y"))) && !isDraggingVertical)
		{
			this.transform.Rotate(Vector3.up*Time.deltaTime*Input.GetAxis("Mouse X")*dragSpeedX);
			isDraggingHorizontal = true;
		}
		else if((Mathf.Abs(Input.GetAxis("Mouse X")) < Mathf.Abs(Input.GetAxis("Mouse Y"))) && !isDraggingHorizontal)
		{	
			this.transform.Rotate(Vector3.back*Time.deltaTime*Input.GetAxis("Mouse Y")*dragSpeedY);
			isDraggingVertical = true;
		}
	}
	
	void OnMouseUp()
	{
		if(this.tag == "paintScene")
			pvc.resumeRotate();
		else
			vc.resumeRotate();
		isDraggingVertical = false;
		isDraggingHorizontal = false;
	}
}
