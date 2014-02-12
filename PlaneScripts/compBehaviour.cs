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
	public bool isAbleToDrag = true;
	//change part materials
	private Material[] changePartMaterials = new Material[10];
	//current car ID
	private int currentCarID;
	//Timer
	private float startTime;
	private bool countDownShouldStart = false;
	public int resumeRotationTimeDuration = 30;
	//smooth follow buffer
	private Vector3 screenPoint;
	private Vector3 offset;
	//sound effect
	public AudioClip successSound;
	
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
		if(this.tag == "wheel" || this.tag == "f2wheel" || this.tag == "f3wheel" 
			|| this.tag == "f4wheel" || this.tag == "f5wheel" || this.tag == "f6wheel" || this.tag == "f7wheel")
		{
			COMP_ID = TYPE_WHEEL;
		}
		else if(this.tag == "body" || this.tag == "f2body" || this.tag == "f3body" 
			|| this.tag == "f4body" || this.tag == "f5body" || this.tag == "f6body" || this.tag == "f7body")
		{
			COMP_ID = TYPE_BODY;
		}
		else if(this.tag == "frontWheel" || this.tag == "f2frontWheel" || this.tag == "f3frontWheel" 
			|| this.tag == "f4frontWheel" || this.tag == "f5frontWheel" || this.tag == "f6frontWheel" || this.tag == "f7frontWheel")
		{
			COMP_ID = TYPE_FRONT_WHEEL;
		}
		else if(this.tag == "bottom" || this.tag == "f2bottom" || this.tag == "f3bottom" 
			|| this.tag == "f4bottom" || this.tag == "f5bottom" || this.tag == "f6bottom" || this.tag == "f7bottom")
		{
			COMP_ID = TYPE_BOTTOM;
		}
		else if(this.tag == "part5" || this.tag == "f2part5" || this.tag == "f3part5" 
			|| this.tag == "f4part5" || this.tag == "f5part5" || this.tag == "f6part5" || this.tag == "f7part5")
		{
			COMP_ID = TYPE_PART5;
		}
		else if(this.tag == "part6" || this.tag == "f2part6" || this.tag == "f3part6" 
			|| this.tag == "f4part6" || this.tag == "f5part6" || this.tag == "f6part6" || this.tag == "f7part6")
		{
			COMP_ID = TYPE_PART6;
		}
		currentCarID = vc.getCurrentCarID();
		judgeManipulation();
		judgeDragDistance(COMP_ID);
	}
	
	//Mouse Behaviour
	void OnMouseDrag()
	{
		if(isAbleToDrag)
		{
			Debug.Log("dragging");
			//vc.pauseRotate();
			judgePart(COMP_ID);
			
		    //smooth follow
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
			transform.position = curPosition;
			
			if(Mathf.Abs(transform.position.x - originalPosition.x) > dragDistance)
			{
				//hide component
				renderer.enabled = false;
				setSolid();
				isCompSetUp = true;
				renderer.material.color = originalColor;
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
		//vc.resumeRotate();
		if(isCompSetUp)
		{
			//renderer.material.color = Color.green;
			isAbleToDrag = false;
			audio.PlayOneShot(successSound);
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
	//mouse down
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
     	offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3
			(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
				changeParts = GameObject.FindGameObjectsWithTag("f2changeWheel");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("f3changeWheel");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("f4changeWheel");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("f5changeWheel");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("f6changeWheel");
            else if (currentCarID == 6)
                changeParts = GameObject.FindGameObjectsWithTag("f7changeWheel");
			break;
		case 1:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeBody");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("f2changeBody");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("f3changeBody");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("f4changeBody");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("f5changeBody");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("f6changeBody");
            else if (currentCarID == 6)
                changeParts = GameObject.FindGameObjectsWithTag("f7changeBody");
			break;
		case 2:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeFrontWheel");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("f2changeFrontWheel");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("f3changeFrontWheel");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("f4changeFrontWheel");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("f5changeFrontWheel");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("f6changeFrontWheel");
            else if (currentCarID == 6)
                changeParts = GameObject.FindGameObjectsWithTag("f7changeFrontWheel");
			break;
		case 3:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeBottom");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("f2changeBottom");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("f3changeBottom");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("f4changeBottom");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("f5changeBottom");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("f6changeBottom");
            else if (currentCarID == 6)
                changeParts = GameObject.FindGameObjectsWithTag("f7changeBottom");
			break;
		case 4:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changePart5");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("f2changePart5");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("f3changePart5");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("f4changePart5");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("f5changePart5");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("f6changePart5");
            else if (currentCarID == 6)
                changeParts = GameObject.FindGameObjectsWithTag("f7changePart5");
			break;
		case 5:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changePart6");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("f2changePart6");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("f3changePart6");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("f4changePart6");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("f5changePart6");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("f6changePart6");
            else if (currentCarID == 6)
                changeParts = GameObject.FindGameObjectsWithTag("f7changePart6");
			break;
		default:
			break;
		}
	}

    void judgeChangePart()
    {

    }

	void judgeManipulation()
	{
		if(Input.GetMouseButtonDown(0))
		{
			vc.pauseRotate();
			countDownShouldStart = true;
			startTime = Time.time;
		}
		if(countDownShouldStart)
		{
			float currentTime = Time.time - startTime;
			int seconds = (int)(currentTime % 60);
			if(seconds > resumeRotationTimeDuration)
			{
				vc.resumeRotate();
				countDownShouldStart = false;
			}
		}	
	}
	
	void judgeDragDistance(int comp_id)
	{
		if(currentCarID == 0)
		{
			switch(comp_id)
			{
			case 0:
				dragDistance = 80f;
				break;
			case 1:
				dragDistance = 150f;
				break;
			case 2:
				dragDistance = 80f;
				break;
			case 3:
				dragDistance = 180f;
				break;
			case 4:
				dragDistance = 70f;
				break;
			case 5:
				dragDistance = 180f;
				break;
			default:
				break;
			}
		}
		else if(currentCarID == 1)
		{
			switch(comp_id)
			{
			case 0:
				dragDistance = 110f;
				break;
			case 1:
				dragDistance = 140f;
				break;
			case 2:
				dragDistance = 150f;
				break;
			case 3:
				dragDistance = 180f;
				break;
			case 4:
				dragDistance = 120f;
				break;
			case 5:
				dragDistance = 190f;
				break;
			default:
				break;
			}
		}
		else if(currentCarID == 2)
		{
			//add
		}
		else if(currentCarID == 3)
		{
			//add 
		}
		else if(currentCarID == 4)
		{
			//add
		}
		else if(currentCarID == 5)
		{
			//add
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
