using UnityEngine;
using System.Collections;

public class compBehaviour : MonoBehaviour {
	
	private viewController vc;
	//drag speed
	private int dragSpeedX = 10;
	private int dragSpeedY = 10;
	//component types
	private int COMP_ID;
	private int TYPE_WHEEL = 0;
	
	//original Position
	private Vector3 originalPosition = new Vector3();
	//change part
	private GameObject[] changeParts = new GameObject[10];
	//component drag distance
	private float dragDistance = 3.5f;
	
	void Start()
	{
		changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
		//Init with opacity
		foreach(GameObject changePart in changeParts)
		{
			changePart.transform.renderer.material.SetColor("_Color", 
						new Color(changePart.transform.renderer.material.color.r,changePart.transform.renderer.material.color.g,
						changePart.transform.renderer.material.color.b, 0.1f));
			changePart.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}
		vc = (viewController)Camera.main.GetComponent(typeof(viewController));
		originalPosition = this.transform.position;
	}
	
	void Update()
	{
		if(this.tag == "wheel")
		{
			Debug.Log("this is wheel");
			COMP_ID = TYPE_WHEEL;
		}
	}
	
	//Mouse Behaviour
	void OnMouseDrag()
	{
		Debug.Log("dragging");
		vc.pauseRotate();
		transform.position += Vector3.right * Time.deltaTime*Input.GetAxis("Mouse X") * dragSpeedX;
		transform.position += Vector3.up * Time.deltaTime*Input.GetAxis("Mouse Y") * dragSpeedY;
		//Debug.Log(transform.position);
		if(Mathf.Abs(transform.position.x - originalPosition.x) > dragDistance)
		{
			//hide component
			renderer.enabled = false;
			judgePart(COMP_ID);
		}
	}
	
	void OnMouseDown()
	{
		//Debug.Log("on Clicking");
	}
	
	void OnMouseUp()
	{
		Debug.Log("Mouse up");
		vc.resumeRotate();
		switch(COMP_ID)
		{
		case 0:
			break;
		default:
			break;
		}
		//resume component position
		this.transform.position = originalPosition;
		renderer.enabled = true;
	}
	
	void OnMouseEnter()
	{
		//Debug.Log("Mouse Entering");
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
		Debug.Log("Mouse leave");
	}
	
	void OnMouseOver()
	{
		renderer.material.color = Color.red;
		Debug.Log("Mouse Stay");
	}
	
	//judge which part of the car response the component
	void judgePart(int comp_id)
	{
		switch(comp_id)
		{
		case 0:
			foreach(GameObject changePart in changeParts)
			{
				changePart.transform.renderer.material.SetColor("_Color", 
					new Color(changePart.transform.renderer.material.color.r,changePart.transform.renderer.material.color.g,
					changePart.transform.renderer.material.color.b, 1.0f));
				changePart.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
			}
			break;
		default:
			break;
		}
	}
}
