using UnityEngine;
using System.Collections;

public class viewController : MonoBehaviour {
	
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
	private int MATBTNTHREE_MARGIN_LEFT;
	private int MATBTNFOUR_MARGIN_LEFT;
	private int MATBTNFIVE_MARGIN_LEFT;
	private int MATBTNSIX_MARGIN_LEFT;
	private int MATBTN_MARGIN_UP;
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
	//materials
	private Material[] wheelMaterials = new Material[10];
	private Material[] frontWheelMaterials = new Material[10];
	private Material[] bodyMaterials = new Material[10];
	private Material[] bottomMaterials = new Material[10];
	private Material[] mainPartMaterials = new Material[10];
	//current components&scripts
	private GameObject wheel;
	private GameObject body;
	private GameObject frontWheel;
	private GameObject bottom;
	private compBehaviour[] scriptList = new compBehaviour[10];
	private compBehaviour cbWheel;
	private compBehaviour cbBody;
	private compBehaviour cbFrontWheel;
	private compBehaviour cbBottom;
	//parent Transform
	private Transform pTransform;
	private Transform pWheelTransform;
	private Transform pBodyTransform;
	private Transform pFrontWheelTransform;
	private Transform pBottomTransform;
	//current car ID & color ID
	private int currentCarID;
	private int currentColorID;
	//car list
	private GameObject[] carList = new GameObject[10];
	//component list
	private GameObject[] wheelList = new GameObject[10];
	private GameObject[] bodyList = new GameObject[10];
	private GameObject[] frontWheelList = new GameObject[10];
	private GameObject[] bottomList = new GameObject[10];
	//Textures
	private Texture textureColor1,textureColor2,textureColor3,textureColor4,textureColor5,textureColor6;
	public Texture mainTexture;
	public Texture confirmOnClicked;
	public Texture resetOnClicked;
	//on clicked
	private GameObject uiBG;
	private bool onClicked = false;
	private float clickTime;
	//Sound effects
	public AudioClip clickSound;
	public AudioClip paintSound;
	
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		currentCar = GameObject.Find("che001");
		pTransform = currentCar.transform;
		currentCarID = 0;
		currentColorID = 0;
		otherParts = GameObject.FindGameObjectsWithTag("otherPart");
		uiBG = GameObject.Find("Plane");
		
		initCarList();
		setUIPosition();
		loadTexture(currentCarID);
		setTransparent(currentCarID);
		loadChangePart(currentCarID);
		findComponents(currentCarID);
	}
	
	void Update () 
	{
		rotateCar();
		buttonCountDown();
	}
	
	void OnGUI()
	{
		GUI.color = new Color(1.0f,1.0f,1.0f,.0f);
		//reset button
		if(GUI.Button(new Rect(BACKBTN_MARGIN_LEFT,BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT),"Reset"))
		{
			setResume();
			setTransparent(currentCarID);
			//change Done Button Texture
			onClicked = true;
			clickTime = Time.time;
			uiBG.renderer.material.mainTexture = resetOnClicked;
			//play sound
			audio.PlayOneShot(clickSound);
		}
		//confirm button
		if(GUI.Button(new Rect(CONFIRMBTN_MARGIN_LEFT, BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT), "Done"))
		{
			if(cbWheel.isCompSetUp && cbBody.isCompSetUp && cbFrontWheel.isCompSetUp && cbBottom.isCompSetUp)
			{
				//Data interface
				PlayerPrefs.SetInt("CarID",currentCarID);
				PlayerPrefs.SetInt("ColorID",currentColorID);
				//load next scene
				Application.LoadLevel("RoadSelectScene");
			}
			//change Done Button Texture
			onClicked = true;
			clickTime = Time.time;
			uiBG.renderer.material.mainTexture = confirmOnClicked;
			//play sound
			audio.PlayOneShot(clickSound);
		}
		//Material buttons
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Red"))
		{
			changeMaterial(COLOR_RED);
			currentColorID = COLOR_RED;
			//play sound
			audio.PlayOneShot(paintSound);
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Black"))
		{
			changeMaterial(COLOR_BLACK);
			currentColorID = COLOR_BLACK;
			//play sound
			audio.PlayOneShot(paintSound);
		}
		if(GUI.Button(new Rect(MATBTNTHREE_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Yellow"))
		{
			changeMaterial(COLOR_YELLOW);
			currentColorID = COLOR_YELLOW;
			//play sound
			audio.PlayOneShot(paintSound);
		}
		if(GUI.Button(new Rect(MATBTNFOUR_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Grey"))
		{
			changeMaterial(COLOR_GREY);
			currentColorID = COLOR_GREY;
			//play sound
			audio.PlayOneShot(paintSound);
		}
		if(GUI.Button(new Rect(MATBTNFIVE_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Magenta"))
		{
			changeMaterial(COLOR_MAGENTA);
			currentColorID = COLOR_MAGENTA;
			//play sound
			audio.PlayOneShot(paintSound);
		}
		if(GUI.Button(new Rect(MATBTNSIX_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"White"))
		{
			changeMaterial(COLOR_WHITE);
			currentColorID = COLOR_WHITE;
			//play sound
			audio.PlayOneShot(paintSound);
		}
		if(GUI.Button(new Rect(PREVBTN_MARGIN_LEFT,PREVBTN_MARGIN_UP,PREVBTN_WIDTH,PREVBTN_HEIGHT),"Prev"))
		{
			//prev model
			if(currentCarID > 0 && currentCarID <= 4)
			{
				switchCar(currentCarID-1);
				resumeRotate();
			}
			//play sound
			audio.PlayOneShot(clickSound);
		}
		if(GUI.Button(new Rect(NEXTBTN_MARGIN_LEFT,PREVBTN_MARGIN_UP,PREVBTN_WIDTH,PREVBTN_HEIGHT),"Next"))
		{
			//next model
			if(currentCarID >= 0 && currentCarID < 4)
			{
				switchCar(currentCarID+1);
				resumeRotate();
			}
			//play sound
			audio.PlayOneShot(clickSound);
		}
	}
	
	void buttonCountDown()
	{
		if(onClicked)
		{
			float assumeTime = Time.time - clickTime;
			int fraction = (int)((assumeTime * 100) % 100);
			if(fraction > 20)
			{
				uiBG.renderer.material.mainTexture = mainTexture;
				onClicked = false;
			}
		}
	}
	
	void initCarList()
	{
		//car
		carList[0] = GameObject.Find("che001");
		carList[1] = GameObject.Find("che002");
		carList[2] = GameObject.Find("che003");
		carList[3] = GameObject.Find("che004");
		carList[4] = GameObject.Find("che005");
		//wheel
		wheelList[0] = GameObject.FindGameObjectWithTag("wheel");
		wheelList[1] = GameObject.FindGameObjectWithTag("car2wheel");
		wheelList[2] = GameObject.FindGameObjectWithTag("car3wheel");
		wheelList[3] = GameObject.FindGameObjectWithTag("car4wheel");
		wheelList[4] = GameObject.FindGameObjectWithTag("car5wheel");
		//body
		bodyList[0] = GameObject.FindGameObjectWithTag("body");
		bodyList[1] = GameObject.FindGameObjectWithTag("car2body");
		bodyList[2] = GameObject.FindGameObjectWithTag("car3body");
		bodyList[3] = GameObject.FindGameObjectWithTag("car4body");
		bodyList[4] = GameObject.FindGameObjectWithTag("car5body");
		//frontWheel
		frontWheelList[0] = GameObject.FindGameObjectWithTag("frontWheel");
		frontWheelList[1] = GameObject.FindGameObjectWithTag("car2frontWheel");
		frontWheelList[2] = GameObject.FindGameObjectWithTag("car3frontWheel");
		frontWheelList[3] = GameObject.FindGameObjectWithTag("car4frontWheel");
		frontWheelList[4] = GameObject.FindGameObjectWithTag("car5frontWheel");
		//bottom
		bottomList[0] = GameObject.FindGameObjectWithTag("bottom");
		bottomList[1] = GameObject.FindGameObjectWithTag("car2bottom");
		bottomList[2] = GameObject.FindGameObjectWithTag("car3bottom");
		bottomList[3] = GameObject.FindGameObjectWithTag("car4bottom");
		bottomList[4] = GameObject.FindGameObjectWithTag("car5bottom");
	}
	
	void loadTexture(int carID)
	{
		switch(carID)
		{
		case 0:
			textureColor1 = (Texture)Resources.Load("che001.fbm/che001_clr",typeof(Texture));
			textureColor2 = (Texture)Resources.Load("che001.fbm/che001_clr_cheng",typeof(Texture));
			textureColor3 = (Texture)Resources.Load("che001.fbm/che001_clr_huang",typeof(Texture));
			textureColor4 = (Texture)Resources.Load("che001.fbm/che001_clr_lv",typeof(Texture));
			textureColor5 = (Texture)Resources.Load("che001.fbm/che001_clr_lan",typeof(Texture));
			textureColor6 = (Texture)Resources.Load("che001.fbm/che001_clr_zi",typeof(Texture));
			break;
		case 1:
			textureColor1 = (Texture)Resources.Load("che002.fbm/che002_hong_clr2",typeof(Texture));
			textureColor2 = (Texture)Resources.Load("che002.fbm/che002_cheng_clr",typeof(Texture));
			textureColor3 = (Texture)Resources.Load("che002.fbm/che002_huang_clr",typeof(Texture));
			textureColor4 = (Texture)Resources.Load("che002.fbm/che002_lv_clr",typeof(Texture));
			textureColor5 = (Texture)Resources.Load("che002.fbm/che002_lan_clr",typeof(Texture));
			textureColor6 = (Texture)Resources.Load("che002.fbm/che002_zi_clr",typeof(Texture));
			break;
		case 2:
			textureColor1 = (Texture)Resources.Load("che003.fbm/che003_hong_clr",typeof(Texture));
			textureColor2 = (Texture)Resources.Load("che003.fbm/che003_cheng_clr",typeof(Texture));
			textureColor3 = (Texture)Resources.Load("che003.fbm/che003_huang_clr",typeof(Texture));
			textureColor4 = (Texture)Resources.Load("che003.fbm/che003_lv_clr",typeof(Texture));
			textureColor5 = (Texture)Resources.Load("che003.fbm/che003_lan_clr",typeof(Texture));
			textureColor6 = (Texture)Resources.Load("che003.fbm/che003_zi_clr",typeof(Texture));
			break;
		case 3:
			textureColor1 = (Texture)Resources.Load("che004.fbm/che004_hong_clr",typeof(Texture));
			textureColor2 = (Texture)Resources.Load("che004.fbm/che004_cheng_clr",typeof(Texture));
			textureColor3 = (Texture)Resources.Load("che004.fbm/che004_huang_clr",typeof(Texture));
			textureColor4 = (Texture)Resources.Load("che004.fbm/che004_lv_clr",typeof(Texture));
			textureColor5 = (Texture)Resources.Load("che004.fbm/che004_lan_clr",typeof(Texture));
			textureColor6 = (Texture)Resources.Load("che004.fbm/che004_zi_clr",typeof(Texture));
			break;
		case 4:
			textureColor1 = (Texture)Resources.Load("che005.fbm/che005_hong_clr",typeof(Texture));
			textureColor2 = (Texture)Resources.Load("che005.fbm/che005_cheng_clr",typeof(Texture));
			textureColor3 = (Texture)Resources.Load("che005.fbm/che005_huang_clr",typeof(Texture));
			textureColor4 = (Texture)Resources.Load("che005.fbm/che005_lv_clr",typeof(Texture));
			textureColor5 = (Texture)Resources.Load("che005.fbm/che005_lan_clr",typeof(Texture));
			textureColor6 = (Texture)Resources.Load("che005.fbm/che005_zi_clr",typeof(Texture));
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
		//fix position
		positionFix(carID);
		//load
		loadTexture(currentCarID);
		setTransparent(currentCarID);
		loadChangePart(currentCarID);
		findComponents(currentCarID);
		setResume();
	}
	
	void positionFix(int carID)
	{
		switch(carID)
		{
		case 0:
			break;
		case 1:
			currentCar.transform.position = new Vector3(pTransform.position.x,pTransform.position.y + 0.2f,pTransform.position.z);
			break;
		case 2:
			currentCar.transform.position = new Vector3(pTransform.position.x,pTransform.position.y + 0.9f,pTransform.position.z);
			wheel.transform.position = new Vector3(pWheelTransform.position.x+0.05f,pWheelTransform.position.y+0.1f,pWheelTransform.position.z);
			break;
		case 3:
			currentCar.transform.position = new Vector3(pTransform.position.x,pTransform.position.y + 0.7f,pTransform.position.z);
			break;
		case 4:
			break;
		default:
			break;
		}
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
			mainPartMaterials = GameObject.FindGameObjectWithTag("otherPart").renderer.materials;
			changeMaterials[4] = mainPartMaterials[1];
			break;
		case 1:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car2changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car2changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car2changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car2changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car2main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[0];
			break;
		case 2:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car3changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car3changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car3changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car3changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car3main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[0];
			break;
		case 3:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car4changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car4changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car4changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car4changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car4main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[1];
			break;
		case 4:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car5changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car5changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car5changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car5changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car5main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[4];
			break;
		default:
			break;
		}
	}
	
	void setUIPosition()
	{
		MATBTNONE_MARGIN_LEFT = screenWidth/2 - 455;
		MATBTNTWO_MARGIN_LEFT = screenWidth/2 - 295;
		MATBTNTHREE_MARGIN_LEFT = screenWidth/2 - 135;
		MATBTNFOUR_MARGIN_LEFT = screenWidth/2 + 25;
		MATBTNFIVE_MARGIN_LEFT = screenWidth/2 + 185;
		MATBTNSIX_MARGIN_LEFT = screenWidth/2 + 345;
		MATBTN_MARGIN_UP = screenHeight - 170;
		BUTTON_WIDTH = 125;
		BACKBTN_MARGIN_LEFT = 40;
		CONFIRMBTN_MARGIN_LEFT = screenWidth - 195;
		BACKBTN_MARGIN_UP = screenHeight - 145;
		BACKBTN_WIDTH = 175;
		BACKBTN_HEIGHT = 90;
		PREVBTN_MARGIN_LEFT = screenWidth/2 - 420;
		PREVBTN_MARGIN_UP = screenHeight/2 - 85;
		PREVBTN_WIDTH = 100;
		PREVBTN_HEIGHT = 100;
		NEXTBTN_MARGIN_LEFT = screenWidth/2 + 335;
	}
	
	void changeMaterial(int materialID)
	{
		switch(materialID)
		{
		case 0:
			for(int i = 0; i<=3; i++)
			{
				if(scriptList[i].isCompSetUp)
					changeMaterials[i].SetColor("_Color",Color.red);
			}
			changeMaterials[4].mainTexture = textureColor1;
			break;
		case 1:
			for(int i = 0; i<=3; i++)
			{
				if(scriptList[i].isCompSetUp)
					changeMaterials[i].SetColor("_Color",Color.white);
			}
			changeMaterials[4].mainTexture = textureColor2;
			break;
		case 2:
			for(int i = 0; i<=3; i++)
			{
				if(scriptList[i].isCompSetUp)
					changeMaterials[i].SetColor("_Color",Color.yellow);
			}
			changeMaterials[4].mainTexture = textureColor3;
			break;
		case 3:
			for(int i = 0; i<=3; i++)
			{
				if(scriptList[i].isCompSetUp)
					changeMaterials[i].SetColor("_Color",Color.green);
			}
			changeMaterials[4].mainTexture = textureColor4;
			break;
		case 4:
			for(int i = 0; i<=3; i++)
			{
				if(scriptList[i].isCompSetUp)
					changeMaterials[i].SetColor("_Color",Color.blue);
			}
			changeMaterials[4].mainTexture = textureColor5;
			break;
		case 5:
			for(int i = 0; i<=3; i++)
			{
				if(scriptList[i].isCompSetUp)
					changeMaterials[i].SetColor("_Color",Color.magenta);
			}
			changeMaterials[4].mainTexture = textureColor6;
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
			//transforms
			pWheelTransform = wheel.transform;
			pBodyTransform = body.transform;
			pFrontWheelTransform = frontWheel.transform;
			pBottomTransform = bottom.transform;
			break;
		case 1:
			wheel = GameObject.FindGameObjectWithTag("car2wheel");
			body = GameObject.FindGameObjectWithTag("car2body");
			frontWheel = GameObject.FindGameObjectWithTag("car2frontWheel");
			bottom = GameObject.FindGameObjectWithTag("car2bottom");
			break;
		case 2:
			wheel = GameObject.FindGameObjectWithTag("car3wheel");
			body = GameObject.FindGameObjectWithTag("car3body");
			frontWheel = GameObject.FindGameObjectWithTag("car3frontWheel");
			bottom = GameObject.FindGameObjectWithTag("car3bottom");
			break;
		case 3:
			wheel = GameObject.FindGameObjectWithTag("car4wheel");
			body = GameObject.FindGameObjectWithTag("car4body");
			frontWheel = GameObject.FindGameObjectWithTag("car4frontWheel");
			bottom = GameObject.FindGameObjectWithTag("car4bottom");
			break;
		case 4:
			wheel = GameObject.FindGameObjectWithTag("car5wheel");
			body = GameObject.FindGameObjectWithTag("car5body");
			frontWheel = GameObject.FindGameObjectWithTag("car5frontWheel");
			bottom = GameObject.FindGameObjectWithTag("car5bottom");
			break;
		default:
			break;
		}
		//scripts
		cbWheel = (compBehaviour)wheel.GetComponent(typeof(compBehaviour));
		cbBody = (compBehaviour)body.GetComponent(typeof(compBehaviour));
		cbFrontWheel = (compBehaviour)frontWheel.GetComponent(typeof(compBehaviour));
		cbBottom = (compBehaviour)bottom.GetComponent(typeof(compBehaviour));
		cbWheel.originalPosition = pWheelTransform.position;
		cbBody.originalPosition = pBodyTransform.position;
		cbFrontWheel.originalPosition = pFrontWheelTransform.position;
		cbBottom.originalPosition = pBottomTransform.position;
		scriptList[0] = cbWheel;
		scriptList[1] = cbBody;
		scriptList[2] = cbFrontWheel;
		scriptList[3] = cbBottom;
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
			break;
		case 1:
			changeParts = GameObject.FindGameObjectsWithTag("car2changeWheel");
			changePartsBody = GameObject.FindGameObjectsWithTag("car2changeBody");
			changePartsFrontWheel = GameObject.FindGameObjectsWithTag("car2changeFrontWheel");
			changePartsBottom = GameObject.FindGameObjectsWithTag("car2changeBottom");
			break;
		case 2:
			changeParts = GameObject.FindGameObjectsWithTag("car3changeWheel");
			changePartsBody = GameObject.FindGameObjectsWithTag("car3changeBody");
			changePartsFrontWheel = GameObject.FindGameObjectsWithTag("car3changeFrontWheel");
			changePartsBottom = GameObject.FindGameObjectsWithTag("car3changeBottom");
			break;
		case 3:
			changeParts = GameObject.FindGameObjectsWithTag("car4changeWheel");
			changePartsBody = GameObject.FindGameObjectsWithTag("car4changeBody");
			changePartsFrontWheel = GameObject.FindGameObjectsWithTag("car4changeFrontWheel");
			changePartsBottom = GameObject.FindGameObjectsWithTag("car4changeBottom");
			break;
		case 4:
			changeParts = GameObject.FindGameObjectsWithTag("car5changeWheel");
			changePartsBody = GameObject.FindGameObjectsWithTag("car5changeBody");
			changePartsFrontWheel = GameObject.FindGameObjectsWithTag("car5changeFrontWheel");
			changePartsBottom = GameObject.FindGameObjectsWithTag("car5changeBottom");
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
					wheelMaterial.color.b, 0.1f));
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
					bodyMaterial.color.b, 0.1f));
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
					frontWheelMaterial.color.b, 0.1f));
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
					bottomMaterial.color.b, 0.1f));
				bottomMaterial.shader = Shader.Find("Transparent/Diffuse");
			}
		}
	}
	
	void resumeMainBodyMaterial(int carID)
	{
		switch(carID)
		{
		case 0:
			changeMaterials[4].mainTexture = textureColor1;
			break;
		case 1:
			changeMaterials[4].mainTexture = textureColor4;
			break;
		case 2:
			changeMaterials[4].mainTexture = textureColor5;
			break;
		case 3:
			changeMaterials[4].mainTexture = textureColor2;
			break;
		case 4:
			changeMaterials[4].mainTexture = textureColor6;
			break;
		default:
			break;
		}
	}
	
	void setResume()
	{
		cbWheel.isCompSetUp = false;
		cbBody.isCompSetUp = false;
		cbFrontWheel.isCompSetUp = false;
		cbBottom.isCompSetUp = false;
		cbWheel.isAbleToDrag = true;
		cbBody.isAbleToDrag = true;
		cbFrontWheel.isAbleToDrag = true;
		cbBottom.isAbleToDrag = true;
		cbWheel.resumeColor();
		cbBody.resumeColor();
		cbFrontWheel.resumeColor();
		cbBottom.resumeColor();
		resumeMainBodyMaterial(currentCarID);
		resumeRotate();
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
