using UnityEngine;
using System.Collections;

public class paintViewController : MonoBehaviour {
	
	private GameObject currentCar;
	private Material currentMaterial;
	
	private int screenWidth;
	private int screenHeight;
	//change materials
	Material[] changeMaterials = new Material[10];
	//material ID
	private int COLOR_RED = 0;
	private int COLOR_BLACK = 1;
	private int COLOR_YELLOW = 2;
	private int COLOR_GREY = 3;
	private int COLOR_MAGENTA = 4;
	private int COLOR_WHITE = 5;
	//UI Positions
	private int MATBTNONE_MARGIN_LEFT;
	private int MATBTNTWO_MARGIN_LEFT;
	private int MATBTNONE_MARGIN_UP;
	private int MATBTNTWO_MARGIN_UP;
	private int MATBTNTHREE_MARGIN_UP;
	private int BUTTON_WIDTH;
	private int BACKBTN_MARGIN_LEFT;
	private int CONFIRMBTN_MARGIN_LEFT;
	private int BACKBTN_MARGIN_UP;
	private int BACKBTN_WIDTH;
	private int BACKBTN_HEIGHT;
	private int PREVBTN_MARGIN_LEFT;
	private int PREVBTN_MARGIN_UP;
	private int PREVBTN_WIDTH;
	private int PREVBTN_HEIGHT;
	private int NEXTBTN_MARGIN_LEFT;
	//UI texture bitmaps
	/*private Texture2D compTextureOne;*/
	//whether the car should rotate or not
	private bool shouldCarRotate = true;
	//transparent part
	private GameObject[] otherParts = new GameObject[30];
	//current change part
	private GameObject[] changeParts = new GameObject[10];
	private GameObject[] changePartsBody = new GameObject[10];
	private GameObject[] changePartsFrontWheel = new GameObject[10];
	private GameObject[] changePartsBottom = new GameObject[10];
	private GameObject[] changePartsPart5 = new GameObject[10];
	private GameObject[] changePartsPart6 = new GameObject[10];
	//materials
	private Material[] wheelMaterials = new Material[10];
	private Material[] frontWheelMaterials = new Material[10];
	private Material[] bodyMaterials = new Material[10];
	private Material[] bottomMaterials = new Material[10];
	private Material[] part5Materials = new Material[10];
	private Material[] part6Materials = new Material[10];
	private Material[] mainPartMaterials = new Material[10];
	//current components&scripts
	private GameObject wheel;
	private GameObject body;
	private GameObject frontWheel;
	private GameObject bottom;
	private GameObject part5;
	private GameObject part6;
	private compBehaviour[] scriptList = new compBehaviour[10];
	private compBehaviour cbWheel;
	private compBehaviour cbBody;
	private compBehaviour cbFrontWheel;
	private compBehaviour cbBottom;
	private compBehaviour cbPart5;
	private compBehaviour cbPart6;
	//parent Transform
	private Transform pTransform;
	private Transform pWheelTransform;
	private Transform pBodyTransform;
	private Transform pFrontWheelTransform;
	private Transform pBottomTransform;
	private Transform pPart5Transfrom;
	private Transform pPart6Transform;
	//current car ID
	private int currentCarID;
	//car list
	private GameObject[] carList = new GameObject[10];
	//component list
	private GameObject[] wheelList = new GameObject[10];
	private GameObject[] bodyList = new GameObject[10];
	private GameObject[] frontWheelList = new GameObject[10];
	private GameObject[] bottomList = new GameObject[10];
	private GameObject[] part5List = new GameObject[10];
	private GameObject[] part6List = new GameObject[10];
	//original colors
	private Color[] originalColorList = new Color[10];
	//Textures
	private Texture textureColor1,textureColor2,textureColor3,textureColor4,textureColor5,textureColor6;
	
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		currentCar = GameObject.Find("feiJi003");
		pTransform = currentCar.transform;
		currentCarID = 0;
		otherParts = GameObject.FindGameObjectsWithTag("otherPart");
		
		initCarList();
		setUIPosition();
		//setTransparent(currentCarID);
		loadChangePart(currentCarID);
		loadTexture(currentCarID);
		//findComponents(currentCarID);
	}
	
	void Update () 
	{
		rotateCar();
	}
	
	void OnGUI()
	{
		//reset button
		if(GUI.Button(new Rect(BACKBTN_MARGIN_LEFT,BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT),"Reset"))
		{
			setResume();
			//setTransparent(currentCarID);
		}
		//confirm button
		if(GUI.Button(new Rect(CONFIRMBTN_MARGIN_LEFT, BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT), "Done"))
		{
			if(cbWheel.isCompSetUp && cbBody.isCompSetUp && cbFrontWheel.isCompSetUp && cbBottom.isCompSetUp)
			{
				//load next scene
				Application.LoadLevel("RoadSelectScene");
			}
		}
		//Material buttons
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTNONE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Red"))
		{
			changeMaterial(COLOR_RED);
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTNONE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Black"))
		{
			changeMaterial(COLOR_BLACK);
		}
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTNTWO_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Yellow"))
		{
			changeMaterial(COLOR_YELLOW);
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTNTWO_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Grey"))
		{
			changeMaterial(COLOR_GREY);
		}
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTNTHREE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Magenta"))
		{
			changeMaterial(COLOR_MAGENTA);
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTNTHREE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"White"))
		{
			changeMaterial(COLOR_WHITE);
		}
		//draw component Textures
		//GUI.DrawTexture(new Rect(70,70,225,160), compTextureOne, ScaleMode.StretchToFill, true, 0);
//		if(GUI.Button(new Rect(PREVBTN_MARGIN_LEFT,PREVBTN_MARGIN_UP,PREVBTN_WIDTH,PREVBTN_HEIGHT),"Prev"))
//		{
//			//prev model
//			if(currentCarID > 0 && currentCarID <= 1)
//			{
//				switchCar(currentCarID-1);
//			}
//		}
//		if(GUI.Button(new Rect(NEXTBTN_MARGIN_LEFT,PREVBTN_MARGIN_UP,PREVBTN_WIDTH,PREVBTN_HEIGHT),"Next"))
//		{
//			//next model
//			if(currentCarID >= 0 && currentCarID < 1)
//			{
//				switchCar(currentCarID+1);
//			}
//		}
	}
	
	void initCarList()
	{
		//car
		carList[0] = GameObject.Find("feiji003");
		//carList[1] = GameObject.Find("che002");
		//wheel
		wheelList[0] = GameObject.FindGameObjectWithTag("wheel");
		//wheelList[1] = GameObject.FindGameObjectWithTag("car2wheel");
		//body
		bodyList[0] = GameObject.FindGameObjectWithTag("body");
		//bodyList[1] = GameObject.FindGameObjectWithTag("car2body");
		//frontWheel
		frontWheelList[0] = GameObject.FindGameObjectWithTag("frontWheel");
		//frontWheelList[1] = GameObject.FindGameObjectWithTag("car2frontWheel");
		//bottom
		bottomList[0] = GameObject.FindGameObjectWithTag("bottom");
		//bottomList[1] = GameObject.FindGameObjectWithTag("car2bottom");
		//part5
		part5List[0] = GameObject.FindGameObjectWithTag("part5");
		//part6
		part6List[0] = GameObject.FindGameObjectWithTag("part6");
	}
	
	void loadTexture(int carID)
	{
		switch(carID)
		{
		case 0:
			textureColor1 = (Texture)Resources.Load("feiJi003.fbm/feiJi003_clr_hong",typeof(Texture));
			textureColor2 = (Texture)Resources.Load("feiJi003.fbm/feiJi003_clr_cheng",typeof(Texture));
			textureColor3 = (Texture)Resources.Load("feiJi003.fbm/feiJi003_clr_huang",typeof(Texture));
			textureColor4 = (Texture)Resources.Load("feiJi003.fbm/feiJi003_clr_lan",typeof(Texture));
			textureColor5 = (Texture)Resources.Load("feiJi003.fbm/feiJi003_clr_lv",typeof(Texture));
			textureColor6 = (Texture)Resources.Load("feiJi003.fbm/feiJi003_clr_zi",typeof(Texture));
			break;
		case 1:
			break;
		default:
			break;
		}
	}
	
	void switchCar(int carID)
	{
		currentCar.SetActive(false);
		currentCar = carList[carID];
		currentCar.transform.position = pTransform.position;
		currentCar.transform.rotation = pTransform.rotation;
		currentCar.SetActive(true);
		currentCarID = carID;
		//change components
		//wheel
		wheel.SetActive(false);
		wheel = wheelList[carID];
		wheel.transform.position = pWheelTransform.position;
		wheel.transform.rotation = pWheelTransform.rotation;
		wheel.SetActive(true);
		//body
		body.SetActive(false);
		body = bodyList[carID];
		body.transform.position = pBodyTransform.position;
		body.transform.rotation = pBodyTransform.rotation;
		body.SetActive(true);
		//frontWheel
		frontWheel.SetActive(false);
		frontWheel = frontWheelList[carID];
		frontWheel.transform.position = pFrontWheelTransform.position;
		frontWheel.transform.rotation = pFrontWheelTransform.rotation;
		frontWheel.SetActive(true);
		//bottom
		bottom.SetActive(false);
		bottom = bottomList[carID];
		bottom.transform.position = pBottomTransform.position;
		bottom.transform.rotation = pBottomTransform.rotation;
		bottom.SetActive(true);
		//part5
		part5.SetActive(false);
		part5 = part5List[carID];
		part5.transform.position = pPart5Transfrom.position;
		part5.transform.rotation = pPart5Transfrom.rotation;
		part5.SetActive(true);
		//part6
		part6.SetActive(false);
		part6 = part6List[carID];
		part6.transform.position = pPart6Transform.position;
		part6.transform.rotation = pPart6Transform.rotation;
		part6.SetActive(true);
		
		loadTexture(currentCarID);
		setTransparent(currentCarID);
		loadChangePart(currentCarID);
		findComponents(currentCarID);
		setResume();
	}
	
	void loadChangePart(int carID)
	{
		switch(carID)
		{
		case 0:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("changeBottom").renderer.material;
			changeMaterials[4] = GameObject.FindGameObjectWithTag("changePart5").renderer.material;
			changeMaterials[5] = GameObject.FindGameObjectWithTag("changePart6").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("otherPart").renderer.materials;
			changeMaterials[6] = mainPartMaterials[0];
			//mark original color
			for(int i=0; i<=5; i++)
			{
				originalColorList[i] = new Color(changeMaterials[i].color.r,changeMaterials[i].color.g,changeMaterials[i].color.b,1.0f);
			}
			break;
		case 1:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car2changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car2changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car2changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car2changeBottom").renderer.material;
			break;
		default:
			break;
		}
	}
	
	void setUIPosition()
	{
		MATBTNONE_MARGIN_LEFT = screenWidth - 360;
		MATBTNTWO_MARGIN_LEFT = screenWidth - 170;
		MATBTNONE_MARGIN_UP = screenHeight/2 - 180;
		MATBTNTWO_MARGIN_UP = screenHeight/2 - 10;
		MATBTNTHREE_MARGIN_UP = screenHeight/2 + 160;
		BUTTON_WIDTH = 70;
		BACKBTN_MARGIN_LEFT = screenWidth/2 + 300;
		CONFIRMBTN_MARGIN_LEFT = screenWidth - 115;
		BACKBTN_MARGIN_UP = screenHeight - 70;
		BACKBTN_WIDTH = 94;
		BACKBTN_HEIGHT = 36;
		PREVBTN_MARGIN_LEFT = screenWidth/2 - 400;
		PREVBTN_MARGIN_UP = screenHeight/2 - 135;
		PREVBTN_WIDTH = 40;
		PREVBTN_HEIGHT = 90;
		NEXTBTN_MARGIN_LEFT = screenWidth/2 + 360;
	}
	
	void changeMaterial(int materialID)
	{
		switch(materialID)
		{
		case 0:
			//GameObject.Find("che001_part01").renderer.material.SetColor("_Color",Color.red);
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.red);
			}
			changeMaterials[6].mainTexture = textureColor1;
			break;
		case 1:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.white);
			}
			changeMaterials[6].mainTexture = textureColor2;
			break;
		case 2:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.yellow);
			}
			changeMaterials[6].mainTexture = textureColor3;
			break;
		case 3:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.grey);
			}
			changeMaterials[6].mainTexture = textureColor4;
			break;
		case 4:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.green);
			}
			changeMaterials[6].mainTexture = textureColor5;
			break;
		case 5:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.magenta);
			}
			changeMaterials[6].mainTexture = textureColor6;
			break;
		default:
			break;
		}	
	}
	
	void rotateCar()
	{
		if(shouldCarRotate)
		{
			currentCar.transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
		}
		else
		{
			//drag components
		}
	}
	
	void dataPersist()
	{
		for(int i=0; i<10; i++)
		{
			DontDestroyOnLoad(changeParts[i]);
			DontDestroyOnLoad(changePartsBody[i]);
			DontDestroyOnLoad(changePartsFrontWheel[i]);
			DontDestroyOnLoad(changePartsBottom[i]);
		}
	}
	
	void findComponents(int carID)
	{
		//components
		switch(carID)
		{
		case 0:
			wheel = GameObject.FindGameObjectWithTag("wheel");
			body = GameObject.FindGameObjectWithTag("body");
			frontWheel = GameObject.FindGameObjectWithTag("frontWheel");
			bottom = GameObject.FindGameObjectWithTag("bottom");
			part5 = GameObject.FindGameObjectWithTag("part5");
			part6 = GameObject.FindGameObjectWithTag("part6");
			//transforms
			pWheelTransform = wheel.transform;
			pBodyTransform = body.transform;
			pFrontWheelTransform = frontWheel.transform;
			pBottomTransform = bottom.transform;
			pPart5Transfrom = part5.transform;
			pPart6Transform = part6.transform;
			break;
		case 1:
			wheel = GameObject.FindGameObjectWithTag("car2wheel");
			body = GameObject.FindGameObjectWithTag("car2body");
			frontWheel = GameObject.FindGameObjectWithTag("car2frontWheel");
			bottom = GameObject.FindGameObjectWithTag("car2bottom");
			break;
		default:
			break;
		}
		//scripts
		cbWheel = (compBehaviour)wheel.GetComponent(typeof(compBehaviour));
		cbBody = (compBehaviour)body.GetComponent(typeof(compBehaviour));
		cbFrontWheel = (compBehaviour)frontWheel.GetComponent(typeof(compBehaviour));
		cbBottom = (compBehaviour)bottom.GetComponent(typeof(compBehaviour));
		cbPart5 = (compBehaviour)part5.GetComponent(typeof(compBehaviour));
		cbPart6 = (compBehaviour)part6.GetComponent(typeof(compBehaviour));
		//original positions
		cbWheel.originalPosition = pWheelTransform.position;
		cbBody.originalPosition = pBodyTransform.position;
		cbFrontWheel.originalPosition = pFrontWheelTransform.position;
		cbBottom.originalPosition = pBottomTransform.position;
		cbPart5.originalPosition = pPart5Transfrom.position;
		cbPart6.originalPosition = pPart6Transform.position;
		scriptList[0] = cbWheel;
		scriptList[1] = cbBody;
		scriptList[2] = cbFrontWheel;
		scriptList[3] = cbBottom;
		scriptList[4] = cbPart5;
		scriptList[5] = cbPart6;
	}

	void setTransparent(int carID)
	{
		switch(carID)
		{
		case 0:
			changeParts = GameObject.FindGameObjectsWithTag("changeWheel");
			changePartsBody = GameObject.FindGameObjectsWithTag("changeBody");
			changePartsFrontWheel = GameObject.FindGameObjectsWithTag("changeFrontWheel");
			changePartsBottom = GameObject.FindGameObjectsWithTag("changeBottom");
			changePartsPart5 = GameObject.FindGameObjectsWithTag("changePart5");
			changePartsPart6 = GameObject.FindGameObjectsWithTag("changePart6");
			break;
		case 1:
			changeParts = GameObject.FindGameObjectsWithTag("car2changeWheel");
			changePartsBody = GameObject.FindGameObjectsWithTag("car2changeBody");
			changePartsFrontWheel = GameObject.FindGameObjectsWithTag("car2changeFrontWheel");
			changePartsBottom = GameObject.FindGameObjectsWithTag("car2changeBottom");
			break;
		default:
			break;
		}
		foreach(GameObject changePart in changeParts)
		{
			wheelMaterials = changePart.renderer.materials;
			foreach(Material wheelMaterial in wheelMaterials)
			{
				wheelMaterial.SetColor("_Color", 
					new Color(wheelMaterial.color.r,wheelMaterial.color.g,
					wheelMaterial.color.b, 0.05f));
				wheelMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
		foreach(GameObject changePartBody in changePartsBody)
		{
			bodyMaterials = changePartBody.renderer.materials;
			foreach(Material bodyMaterial in bodyMaterials)
			{
				bodyMaterial.SetColor("_Color", 
					new Color(bodyMaterial.color.r,bodyMaterial.color.g,
					bodyMaterial.color.b, 0.05f));
				bodyMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
		foreach(GameObject changePartFrontWheel in changePartsFrontWheel)
		{
			frontWheelMaterials = changePartFrontWheel.renderer.materials;
			foreach(Material frontWheelMaterial in frontWheelMaterials)
			{
				frontWheelMaterial.SetColor("_Color", 
					new Color(frontWheelMaterial.color.r,frontWheelMaterial.color.g,
					frontWheelMaterial.color.b, 0.05f));
				frontWheelMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
		foreach(GameObject changePartBottom in changePartsBottom)
		{
			bottomMaterials = changePartBottom.renderer.materials;
			foreach(Material bottomMaterial in bottomMaterials)
			{
				bottomMaterial.SetColor("_Color", 
					new Color(bottomMaterial.color.r,bottomMaterial.color.g,
					bottomMaterial.color.b, 0.05f));
				bottomMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
		foreach(GameObject changePartPart5 in changePartsPart5)
		{
			part5Materials = changePartPart5.renderer.materials;
			foreach(Material part5Material in part5Materials)
			{
				part5Material.SetColor("_Color", 
					new Color(part5Material.color.r,part5Material.color.g,
					part5Material.color.b, 0.05f));
				part5Material.shader = Shader.Find("Transparent/Diffuse");
			}
		}
		foreach(GameObject changePartPart6 in changePartsPart6)
		{
			part6Materials = changePartPart6.renderer.materials;
			foreach(Material part6Material in part6Materials)
			{
				part6Material.SetColor("_Color", 
					new Color(part6Material.color.r,part6Material.color.g,
					part6Material.color.b, 0.05f));
				part6Material.shader = Shader.Find("Transparent/Diffuse");
			}
		}
	}
	
	void setResume()
	{
		for(int i=0; i<=5; i++)
		{
			changeMaterials[i].color = originalColorList[i];
		}
	}
	
	//public interfaces
	public void pauseRotate()
	{
		shouldCarRotate = false;
	}
	
	public void resumeRotate()
	{
		shouldCarRotate = true;
	}
	
	public int getCurrentCarID()
	{
		return currentCarID;
	}
}
