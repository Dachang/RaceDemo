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
    public bool isSoundPlayed = false;
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
			|| this.tag == "f4wheel" || this.tag == "f5wheel" || this.tag == "f6wheel" || this.tag == "f7wheel" || this.tag == "f8wheel"
            || this.tag == "f9wheel" || this.tag == "f10wheel" || this.tag == "f11wheel" || this.tag == "f12wheel" || this.tag == "f13wheel"
             || this.tag == "f14wheel" || this.tag == "f15wheel" || this.tag == "f16wheel" || this.tag == "f17wheel" || this.tag == "f18wheel"
             || this.tag == "f19wheel" || this.tag == "f20wheel" || this.tag == "f21wheel" || this.tag == "f22wheel" || this.tag == "f23wheel"
             || this.tag == "f24wheel")
		{
			COMP_ID = TYPE_WHEEL;
		}
		else if(this.tag == "body" || this.tag == "f2body" || this.tag == "f3body"
            || this.tag == "f4body" || this.tag == "f5body" || this.tag == "f6body" || this.tag == "f7body" || this.tag == "f8body"
             || this.tag == "f9body" || this.tag == "f10body" || this.tag == "f11body" || this.tag == "f12body" || this.tag == "f13body"
             || this.tag == "f14body" || this.tag == "f15body" || this.tag == "f16body" || this.tag == "f17body" || this.tag == "f18body"
             || this.tag == "f19body" || this.tag == "f20body" || this.tag == "f21body" || this.tag == "f22body" || this.tag == "f23body"
             || this.tag == "f24body")
		{
			COMP_ID = TYPE_BODY;
		}
		else if(this.tag == "frontWheel" || this.tag == "f2frontWheel" || this.tag == "f3frontWheel" 
			|| this.tag == "f4frontWheel" || this.tag == "f5frontWheel" || this.tag == "f6frontWheel" || this.tag == "f7frontWheel"
             || this.tag == "f8frontWheel" || this.tag == "f9frontWheel" || this.tag == "f10frontWheel" || this.tag == "f11frontWheel"
             || this.tag == "f12frontWheel" || this.tag == "f13frontWheel" || this.tag == "f14frontWheel" || this.tag == "f15frontWheel"
             || this.tag == "f16frontWheel" || this.tag == "f17frontWheel" || this.tag == "f18frontWheel" || this.tag == "f19frontWheel"
            || this.tag == "f20frontWheel" || this.tag == "f21frontWheel" || this.tag == "f22frontWheel" || this.tag == "f23frontWheel"
             || this.tag == "f24frontWheel")
		{
			COMP_ID = TYPE_FRONT_WHEEL;
		}
		else if(this.tag == "bottom" || this.tag == "f2bottom" || this.tag == "f3bottom"
            || this.tag == "f4bottom" || this.tag == "f5bottom" || this.tag == "f6bottom" || this.tag == "f7bottom" || this.tag == "f8bottom"
             || this.tag == "f9bottom" || this.tag == "f10bottom" || this.tag == "f11bottom" || this.tag == "f12bottom" || this.tag == "f13bottom"
             || this.tag == "f14bottom" || this.tag == "f15bottom" || this.tag == "f16bottom" || this.tag == "f17bottom" || this.tag == "f18bottom"
             || this.tag == "f19bottom" || this.tag == "f20bottom" || this.tag == "f21bottom" || this.tag == "f22bottom" || this.tag == "f23bottom"
             || this.tag == "f24bottom")
		{
			COMP_ID = TYPE_BOTTOM;
		}
		else if(this.tag == "part5" || this.tag == "f2part5" || this.tag == "f3part5"
            || this.tag == "f4part5" || this.tag == "f5part5" || this.tag == "f6part5" || this.tag == "f7part5" || this.tag == "f8part5"
             || this.tag == "f9part5" || this.tag == "f10part5" || this.tag == "f11part5" || this.tag == "f12part5" || this.tag == "f13part5"
             || this.tag == "f14part5" || this.tag == "f15part5" || this.tag == "f16part5" || this.tag == "f17part5" || this.tag == "f18part5"
             || this.tag == "f19part5" || this.tag == "f20part5" || this.tag == "f21part5" || this.tag == "f22part5" || this.tag == "f23part5"
             || this.tag == "f24part5")
		{
			COMP_ID = TYPE_PART5;
		}
		else if(this.tag == "part6" || this.tag == "f2part6" || this.tag == "f3part6"
            || this.tag == "f4part6" || this.tag == "f5part6" || this.tag == "f6part6" || this.tag == "f7part6" || this.tag == "f8part6"
             || this.tag == "f9part6" || this.tag == "f10part6" || this.tag == "f11part6" || this.tag == "f12part6" || this.tag == "f13part6"
             || this.tag == "f14part6" || this.tag == "f15part6" || this.tag == "f16part6" || this.tag == "f17part6" || this.tag == "f18part6"
             || this.tag == "f19part6" || this.tag == "f20part6" || this.tag == "f21part6" || this.tag == "f22part6" || this.tag == "f23part6"
             || this.tag == "f24part6")
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
            if(!isSoundPlayed) audio.PlayOneShot(successSound);
            isSoundPlayed = true;
            renderer.material.mainTexture = (Texture)Resources.Load("LabelTP",typeof(Texture));
            renderer.material.color = Color.grey;
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
            if (currentCarID == 0)
                changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
            else
                changeParts = GameObject.FindGameObjectsWithTag("f" + (currentCarID+1).ToString() + "changeWheel");
			break;
		case 1:
            if (currentCarID == 0)
                changeParts = GameObject.FindGameObjectsWithTag("changeBody");
            else
                changeParts = GameObject.FindGameObjectsWithTag("f" + (currentCarID+1).ToString() + "changeBody");
			break;
		case 2:
            if (currentCarID == 0)
                changeParts = GameObject.FindGameObjectsWithTag("changeFrontWheel");
            else
                changeParts = GameObject.FindGameObjectsWithTag("f" + (currentCarID + 1).ToString() + "changeFrontWheel");
			break;
		case 3:
            if (currentCarID == 0)
                changeParts = GameObject.FindGameObjectsWithTag("changeBottom");
            else
                changeParts = GameObject.FindGameObjectsWithTag("f" + (currentCarID + 1).ToString() + "changeBottom");
			break;
		case 4:
            if (currentCarID == 0)
                changeParts = GameObject.FindGameObjectsWithTag("changePart5");
            else
                changeParts = GameObject.FindGameObjectsWithTag("f" + (currentCarID + 1).ToString() + "changePart5");
			break;
		case 5:
            if (currentCarID == 0)
                changeParts = GameObject.FindGameObjectsWithTag("changePart6");
            else
                changeParts = GameObject.FindGameObjectsWithTag("f" + (currentCarID + 1).ToString() + "changePart6");
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
        isSoundPlayed = false;
        renderer.material.color = originalColor;
	}
}
