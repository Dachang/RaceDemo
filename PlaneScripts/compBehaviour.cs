using UnityEngine;
using System.Collections;

public class compBehaviour : MonoBehaviour {
	
	private viewController vc;
	//drag speed
	private int dragSpeedX = 150;
	private int dragSpeedY = 150;
	//component types
	private int COMP_ID;
	private int TYPE_WHEEL = 0;
	private int TYPE_BODY = 1;
	private int TYPE_FRONT_WHEEL = 2;
	private int TYPE_BOTTOM = 3;
	private int TYPE_PART5 = 4;
	private int TYPE_PART6 = 5;
	
	//original Position
	public Vector3 originalPosition = new Vector3();
	//original material
	private Material[] originalMaterials = new Material[10];
	private Color originalColor;
	//change part
	private GameObject[] changeParts = new GameObject[10];
	private GameObject[] changePartsBody = new GameObject[10];
	private GameObject[] changePartsFrontWheel = new GameObject[10];
	//component drag distance
	private float dragDistance = 80f;
	//component has been set up?
	public bool isCompSetUp = false;
	//component can be dragged?
	private bool isAbleToDrag = true;
	//change part materials
	private Material[] changePartMaterials = new Material[10];
	//current car ID
	private int currentCarID;
	
	void Start()
	{
		changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
		changePartsBody = GameObject.FindGameObjectsWithTag("changeBody");
		changePartsFrontWheel = GameObject.FindGameObjectsWithTag("changeFrontWheel");
		//Init with opacity
		setTransparent();
		vc = (viewController)Camera.main.GetComponent(typeof(viewController));
		originalPosition = this.transform.position;
		originalColor = new Color(transform.renderer.material.color.r,transform.renderer.material.color.g,
					transform.renderer.material.color.b, 1.0f);
		originalMaterials = transform.renderer.materials;
	}
	
	void Update()
	{
		if(this.tag == "wheel" || this.tag == "car2wheel")
		{
			COMP_ID = TYPE_WHEEL;
		}
		else if(this.tag == "body" || this.tag == "car2body")
		{
			COMP_ID = TYPE_BODY;
		}
		else if(this.tag == "frontWheel" || this.tag == "car2frontWheel")
		{
			COMP_ID = TYPE_FRONT_WHEEL;
		}
		else if(this.tag == "bottom" || this.tag == "car2bottom")
		{
			COMP_ID = TYPE_BOTTOM;
		}
		else if(this.tag == "part5")
		{
			COMP_ID = TYPE_PART5;
		}
		else if(this.tag == "part6")
		{
			COMP_ID = TYPE_PART6;
		}
		currentCarID = vc.getCurrentCarID();
	}
	
	//Mouse Behaviour
	void OnMouseDrag()
	{
		if(isAbleToDrag)
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
	}
	
	void OnMouseUp()
	{
		Debug.Log("Mouse up");
		vc.resumeRotate();
		if(isCompSetUp)
		{
			renderer.material.color = Color.green;
			isAbleToDrag = false;
		}
		else
		{
			renderer.material.color = originalColor;
		}		
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
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeWheel");
			break;
		case 1:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeBody");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeBody");
			break;
		case 2:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeFrontWheel");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeFrontWheel");
			break;
		case 3:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeBottom");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeBottom");
			break;
		case 4:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changePart5");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changePart5");
			break;
		case 5:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changePart6");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changePart6");
			break;
		default:
			break;
		}
	}
	
	void setSolid()
	{
		foreach(GameObject changePart in changeParts)
		{
			changePartMaterials = changePart.renderer.materials;
			foreach(Material changePartMaterial in changePartMaterials)
			{
				changePartMaterial.SetColor("_Color", 
					new Color(changePartMaterial.color.r,changePartMaterial.color.g,
					changePartMaterial.color.b, 1.0f));
				changePartMaterial.shader = Shader.Find("Diffuse");
			}
		}
	}
	
	void setTransparent()
	{
		foreach(GameObject changePart in changeParts)
		{
			changePartMaterials = changePart.renderer.materials;
			foreach(Material changePartMaterial in changePartMaterials)
			{
				changePartMaterial.SetColor("_Color", 
					new Color(changePartMaterial.color.r,changePartMaterial.color.g,
					changePartMaterial.color.b, 0.05f));
				changePartMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
	}
	
	//public interface
	public void resumeColor()
	{
		renderer.material.color = originalColor;
	}
}