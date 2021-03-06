using UnityEngine;
using System.Collections;

public class compBehaviour : MonoBehaviour {
	
	private viewController vc;
	//drag speed
	private int dragSpeedX = 6;
	private int dragSpeedY = 6;
	//component types
	private int COMP_ID;
	private int TYPE_WHEEL = 0;
	private int TYPE_BODY = 1;
	private int TYPE_FRONT_WHEEL = 2;
	private int TYPE_BOTTOM = 3;
	
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
	private float dragDistance = 4.2f;
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
    private bool isSoundPlayed = false;
	
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
		if(this.tag == "wheel" || this.tag == "car2wheel" || this.tag == "car3wheel" || this.tag == "car4wheel" 
			|| this.tag == "car5wheel" || this.tag == "car6wheel"  || this.tag == "car7wheel" || this.tag == "car8wheel")
		{
			COMP_ID = TYPE_WHEEL;
		}
		else if(this.tag == "body" || this.tag == "car2body" || this.tag == "car3body" || this.tag == "car4body"
			|| this.tag == "car5body" || this.tag == "car6body" || this.tag == "car7body" || this.tag == "car8body")
		{
			COMP_ID = TYPE_BODY;
		}
		else if(this.tag == "frontWheel" || this.tag == "car2frontWheel" || this.tag == "car3frontWheel" || this.tag == "car4frontWheel"
			|| this.tag == "car5frontWheel" || this.tag == "car6frontWheel" || this.tag == "car7frontWheel" || this.tag == "car8frontWheel")
		{
			COMP_ID = TYPE_FRONT_WHEEL;
		}
		else if(this.tag == "bottom" || this.tag == "car2bottom" || this.tag == "car3bottom" || this.tag == "car4bottom"
			|| this.tag == "car5bottom" || this.tag == "car6bottom"  || this.tag == "car7bottom"  || this.tag == "car8bottom")
		{
			COMP_ID = TYPE_BOTTOM;
		}
		currentCarID = vc.getCurrentCarID();
		judgeManipulation();
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
			
			//Debug.Log(transform.position);
			if(Mathf.Abs(transform.position.x - originalPosition.x) > dragDistance)
			{
				//hide component
				renderer.enabled = false;
				setSolid();
				renderer.material.color = originalColor;
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
		//vc.resumeRotate();
		if(isCompSetUp)
		{
			//renderer.material.color = Color.green;
			isAbleToDrag = false;
            if(!isSoundPlayed) audio.PlayOneShot(successSound);
            isSoundPlayed = true;
            setCompTransparent();
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
 
    	offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("car3changeWheel");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("car4changeWheel");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("car5changeWheel");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("car6changeWheel");
			else if(currentCarID == 6)
				changeParts = GameObject.FindGameObjectsWithTag("car7changeWheel");
			else if(currentCarID == 7)
				changeParts = GameObject.FindGameObjectsWithTag("car8changeWheel");
			break;
		case 1:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeBody");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeBody");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("car3changeBody");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("car4changeBody");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("car5changeBody");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("car6changeBody");
			else if(currentCarID == 6)
				changeParts = GameObject.FindGameObjectsWithTag("car7changeBody");
			else if(currentCarID == 7)
				changeParts = GameObject.FindGameObjectsWithTag("car8changeBody");
			break;
		case 2:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeFrontWheel");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeFrontWheel");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("car3changeFrontWheel");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("car4changeFrontWheel");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("car5changeFrontWheel");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("car6changeFrontWheel");
			else if(currentCarID == 6)
				changeParts = GameObject.FindGameObjectsWithTag("car7changeFrontWheel");
			else if(currentCarID == 7)
				changeParts = GameObject.FindGameObjectsWithTag("car8changeFrontWheel");
			break;
		case 3:
			if(currentCarID == 0)
				changeParts = GameObject.FindGameObjectsWithTag("changeBottom");
			else if(currentCarID == 1)
				changeParts = GameObject.FindGameObjectsWithTag("car2changeBottom");
			else if(currentCarID == 2)
				changeParts = GameObject.FindGameObjectsWithTag("car3changeBottom");
			else if(currentCarID == 3)
				changeParts = GameObject.FindGameObjectsWithTag("car4changeBottom");
			else if(currentCarID == 4)
				changeParts = GameObject.FindGameObjectsWithTag("car5changeBottom");
			else if(currentCarID == 5)
				changeParts = GameObject.FindGameObjectsWithTag("car6changeBottom");
			else if(currentCarID == 6)
				changeParts = GameObject.FindGameObjectsWithTag("car7changeBottom");
			else if(currentCarID == 7)
				changeParts = GameObject.FindGameObjectsWithTag("car8changeBottom");
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
					changePartMaterial.color.b, 0.1f));
				changePartMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
	}

    void setCompTransparent()
    {
        Material[] compMat = this.renderer.materials;
        foreach (Material mat in compMat)
        {
            mat.SetColor("_Color", new Color(mat.color.r, mat.color.g,mat.color.b, 0.25f));
            mat.shader = Shader.Find("Transparent/Diffuse");
        }
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
	
	//public interface
	public void resumeColor()
	{
        isSoundPlayed = false;
		renderer.material.color = originalColor;
	}
}
