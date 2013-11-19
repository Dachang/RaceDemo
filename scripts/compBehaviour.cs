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
	private int TYPE_BODY = 1;
	
	//original Position
	private Vector3 originalPosition = new Vector3();
	//original material
	private Color originalColor;
	//change part
	private GameObject[] changeParts = new GameObject[10];
	private GameObject[] changePartsBody = new GameObject[10];
	//component drag distance
	private float dragDistance = 3.5f;
	//component has been set up?
	private bool isCompSetUp = false;
	
	void Start()
	{
		changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
		changePartsBody = GameObject.FindGameObjectsWithTag("changeBody");
		//Init with opacity
		setTransparent();
		initWithTransparent();
		vc = (viewController)Camera.main.GetComponent(typeof(viewController));
		originalPosition = this.transform.position;
		originalColor = new Color(transform.renderer.material.color.r,transform.renderer.material.color.g,
					transform.renderer.material.color.b, 1.0f);
	}
	
	void Update()
	{
		if(this.tag == "wheel")
		{
			COMP_ID = TYPE_WHEEL;
		}
		else if(this.tag == "body")
		{
			COMP_ID = TYPE_BODY;
		}
	}
	
	//Mouse Behaviour
	void OnMouseDrag()
	{
		Debug.Log("dragging");
		vc.pauseRotate();
		judgePart(COMP_ID);
		transform.position += Vector3.right * Time.deltaTime*Input.GetAxis("Mouse X") * dragSpeedX;
		transform.position += Vector3.up * Time.deltaTime*Input.GetAxis("Mouse Y") * dragSpeedY;
		//Debug.Log(transform.position);
		if(Mathf.Abs(transform.position.x - originalPosition.x) > dragDistance)
		{
			//hide component
			renderer.enabled = false;
			setSolid();
			isCompSetUp = true;
		}
		else
		{
			//resume component
			renderer.enabled = true;
			setTransparent();
			isCompSetUp = false;
		}
	}
	
	void OnMouseUp()
	{
		Debug.Log("Mouse up");
		vc.resumeRotate();
		if(isCompSetUp)
			renderer.material.color = Color.green;
		else
			renderer.material.color = originalColor;
		//resume component position
		this.transform.position = originalPosition;
		renderer.enabled = true;
	}
	//mouse leave
	void OnMouseExit()
	{
		if(!isCompSetUp)
		{
			renderer.material.color = originalColor;
			Debug.Log("reset color");
		}
	}
	//mouse stay
	void OnMouseOver()
	{
		if(!isCompSetUp)
		{
			renderer.material.color = Color.red;			
		}
	}
	
	//judge which part of the car response the component
	void judgePart(int comp_id)
	{
		switch(comp_id)
		{
		case 0:
			changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
			break;
		case 1:
			changeParts = GameObject.FindGameObjectsWithTag("changeBody");;
			break;
		default:
			break;
		}
	}
	
	void setSolid()
	{
		foreach(GameObject changePart in changeParts)
		{
			changePart.transform.renderer.material.SetColor("_Color", 
					new Color(changePart.transform.renderer.material.color.r,changePart.transform.renderer.material.color.g,
					changePart.transform.renderer.material.color.b, 1.0f));
			changePart.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}
	}
	
	void setTransparent()
	{
		foreach(GameObject changePart in changeParts)
		{
			changePart.transform.renderer.material.SetColor("_Color", 
					new Color(changePart.transform.renderer.material.color.r,changePart.transform.renderer.material.color.g,
					changePart.transform.renderer.material.color.b, 0.1f));
			changePart.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}
	}
	
	void initWithTransparent()
	{
		foreach(GameObject changePartBody in changePartsBody)
		{
			changePartBody.transform.renderer.material.SetColor("_Color", 
					new Color(changePartBody.transform.renderer.material.color.r,changePartBody.transform.renderer.material.color.g,
					changePartBody.transform.renderer.material.color.b, 0.1f));
			changePartBody.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}		
	}
}
