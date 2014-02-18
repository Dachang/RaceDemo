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
	private int RETURNBTN_MARGIN_LEFT;
	//UI texture bitmaps
	/*private Texture2D compTextureOne;*/
	//whether the car should rotate or not
	private bool shouldCarRotate = true;
	//transparent part
	private GameObject[] otherParts = new GameObject[30];
	//current change part
	private GameObject[] changeParts = new GameObject[30];
	private GameObject[] changePartsBody = new GameObject[30];
	private GameObject[] changePartsFrontWheel = new GameObject[30];
	private GameObject[] changePartsBottom = new GameObject[30];
	private GameObject[] changePartsPart5 = new GameObject[30];
	private GameObject[] changePartsPart6 = new GameObject[30];
	//materials
	private Material[] wheelMaterials = new Material[30];
	private Material[] frontWheelMaterials = new Material[30];
	private Material[] bodyMaterials = new Material[30];
	private Material[] bottomMaterials = new Material[30];
	private Material[] part5Materials = new Material[30];
	private Material[] part6Materials = new Material[30];
	private Material[] mainPartMaterials = new Material[30];
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
	//current car ID & color ID
	private int currentCarID;
	private int currentColorID;
	//car list
	private GameObject[] carList = new GameObject[30];
	//component list
	private GameObject[] wheelList = new GameObject[30];
	private GameObject[] bodyList = new GameObject[30];
	private GameObject[] frontWheelList = new GameObject[30];
	private GameObject[] bottomList = new GameObject[30];
	private GameObject[] part5List = new GameObject[30];
	private GameObject[] part6List = new GameObject[30];
	//original colors
	private Color[] originalColorList = new Color[30];
	//Textures
	private Texture textureColor1,textureColor2,textureColor3,textureColor4,textureColor5,textureColor6;
	//plane has chosen
	private bool planeHasChosen = false;
	private int planeID;
	//string deliver to the next scene
	private string carIDString;
	private string colorIDString;
	//sound effect
	public AudioClip clickSound;
	public AudioClip successSound;
    //original color array
    private int[] originalColorArray = new int[30];
    //original plane ID
    private int[] realIDArray = new int[30];
	
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		currentCar = GameObject.Find("feiJi003");
		pTransform = currentCar.transform;
		currentCarID = 0;
		otherParts = GameObject.FindGameObjectsWithTag("otherPart");
		
		planeID = PlayerPrefs.GetInt("planeID",0);
		
		initCarList();
        initOriginalColorArray();
        initRealIDList();
        planePositionFix(planeID);
		setUIPosition();
		//setTransparent(currentCarID);
		loadChangePart(currentCarID);
		loadTexture(currentCarID);
		//findComponents(currentCarID);
	}
	
	void Update () 
	{
		Time.timeScale = 1.0f;
		if(!planeHasChosen)
		{
			switchCar(planeID);
			currentCarID = planeID;
			planeHasChosen = true;
		}
		rotateCar();
	}
	
	void OnGUI()
	{
		GUI.color = new Color(1.0f,1.0f,1.0f,.0f);
		//reset button
		if(GUI.Button(new Rect(BACKBTN_MARGIN_LEFT,BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT),"Reset"))
		{
			setResume(currentCarID);
			//setTransparent(currentCarID);
			audio.PlayOneShot(clickSound);
		}
		//confirm button
		if(GUI.Button(new Rect(CONFIRMBTN_MARGIN_LEFT, BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT), "Done"))
		{
			//load next scene & pass data
			audio.PlayOneShot(clickSound);
			if(currentCarID == 0 || currentCarID == 1 || currentCarID == 2 || currentCarID == 3 || currentCarID == 4
                || currentCarID == 5 || currentCarID == 6 || currentCarID == 7 || currentCarID == 8 || currentCarID == 9
                || currentCarID == 10 || currentCarID == 11 || currentCarID == 12 || currentCarID == 13 || currentCarID == 14
                || currentCarID == 15 || currentCarID == 16 || currentCarID == 17 || currentCarID == 18 || currentCarID == 19
                || currentCarID == 20 || currentCarID == 21 || currentCarID == 22)
			{
				generateID();
				PlayerPrefs.SetString("planeID",carIDString);
				PlayerPrefs.SetString("colorID",colorIDString);
				Application.LoadLevel("Mail");
			}
		}
		//Material buttons
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTNONE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Red"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(successSound);
			changeMaterial(COLOR_RED);
			currentColorID = COLOR_RED;
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTNONE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Black"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(successSound);
			changeMaterial(COLOR_GREY);
			currentColorID = COLOR_GREY;
		}
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTNTWO_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Yellow"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(successSound);
			changeMaterial(COLOR_BLACK);
			currentColorID = COLOR_BLACK;
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTNTWO_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Grey"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(successSound);
			changeMaterial(COLOR_MAGENTA);
			currentColorID = COLOR_MAGENTA;
		}
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTNTHREE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Magenta"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(successSound);
			changeMaterial(COLOR_YELLOW);
			currentColorID = COLOR_YELLOW;
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTNTHREE_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"White"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(successSound);
			changeMaterial(COLOR_WHITE);
			currentColorID = COLOR_WHITE;
		}
		//return button
		if(GUI.Button(new Rect(RETURNBTN_MARGIN_LEFT, BACKBTN_MARGIN_UP,PREVBTN_WIDTH,PREVBTN_HEIGHT),"Back"))
		{
			audio.PlayOneShot(clickSound);
			audio.PlayOneShot(clickSound);
			Application.LoadLevel("Scene1");
		}
		//exitButton
		if(GUI.Button(new Rect(10,screenHeight-60,50,50),"ESC"))
		{
			Application.Quit();
		}
	}
	
	void initCarList()
	{
		//car
		carList[0] = GameObject.Find("feiJi003");
		carList[1] = GameObject.Find("feiJi004");
		carList[2] = GameObject.Find("feiJi005");
		carList[3] = GameObject.Find("feiJi008");
		carList[4] = GameObject.Find("feiJi010");
		carList[5] = GameObject.Find("feiJi011");
        carList[6] = GameObject.Find("feiJi001");
        carList[7] = GameObject.Find("feiJi002");
        carList[8] = GameObject.Find("feiJi006");
        carList[9] = GameObject.Find("feiJi007");
        carList[10] = GameObject.Find("feiJi009");
        for (int i = 11; i <= 23; i++)
        {
            carList[i] = GameObject.Find("feiJi0" + (i + 1).ToString());
        }
		//wheel
		wheelList[0] = GameObject.FindGameObjectWithTag("wheel");
		//body
		bodyList[0] = GameObject.FindGameObjectWithTag("body");
		//frontWheel
		frontWheelList[0] = GameObject.FindGameObjectWithTag("frontWheel");
		//bottom
		bottomList[0] = GameObject.FindGameObjectWithTag("bottom");
		//part5
		part5List[0] = GameObject.FindGameObjectWithTag("part5");
		//part6
		part6List[0] = GameObject.FindGameObjectWithTag("part6");
	}
	
	void loadTexture(int carID)
	{
        if (realIDArray[carID] < 10)
        {
            textureColor1 = (Texture)Resources.Load("feiJi00" + realIDArray[carID].ToString() + ".fbm/feiJi00" +
                realIDArray[carID].ToString() + "_clr_hong", typeof(Texture));
            textureColor2 = (Texture)Resources.Load("feiJi00" + realIDArray[carID].ToString() + ".fbm/feiJi00" +
                realIDArray[carID].ToString() + "_clr_cheng", typeof(Texture));
            textureColor3 = (Texture)Resources.Load("feiJi00" + realIDArray[carID].ToString() + ".fbm/feiJi00" +
                realIDArray[carID].ToString() + "_clr_huang", typeof(Texture));
            textureColor4 = (Texture)Resources.Load("feiJi00" + realIDArray[carID].ToString() + ".fbm/feiJi00" +
                realIDArray[carID].ToString() + "_clr_lan", typeof(Texture));
            textureColor5 = (Texture)Resources.Load("feiJi00" + realIDArray[carID].ToString() + ".fbm/feiJi00" +
                realIDArray[carID].ToString() + "_clr_lv", typeof(Texture));
            textureColor6 = (Texture)Resources.Load("feiJi00" + realIDArray[carID].ToString() + ".fbm/feiJi00" +
                realIDArray[carID].ToString() + "_clr_zi", typeof(Texture));
        }
        else
        {
            textureColor1 = (Texture)Resources.Load("feiJi0" + realIDArray[carID].ToString() + ".fbm/feiJi0" +
               realIDArray[carID].ToString() + "_clr_hong", typeof(Texture));
            textureColor2 = (Texture)Resources.Load("feiJi0" + realIDArray[carID].ToString() + ".fbm/feiJi0" +
                realIDArray[carID].ToString() + "_clr_cheng", typeof(Texture));
            textureColor3 = (Texture)Resources.Load("feiJi0" + realIDArray[carID].ToString() + ".fbm/feiJi0" +
                realIDArray[carID].ToString() + "_clr_huang", typeof(Texture));
            textureColor4 = (Texture)Resources.Load("feiJi0" + realIDArray[carID].ToString() + ".fbm/feiJi0" +
                realIDArray[carID].ToString() + "_clr_lan", typeof(Texture));
            textureColor5 = (Texture)Resources.Load("feiJi0" + realIDArray[carID].ToString() + ".fbm/feiJi0" +
                realIDArray[carID].ToString() + "_clr_lv", typeof(Texture));
            textureColor6 = (Texture)Resources.Load("feiJi0" + realIDArray[carID].ToString() + ".fbm/feiJi0" +
                realIDArray[carID].ToString() + "_clr_zi", typeof(Texture));
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
		
		loadTexture(currentCarID);
		loadChangePart(currentCarID);
		setResume(currentCarID);
		adjustColor(currentCarID);
	}

    void planePositionFix(int carID)
    {
        switch (carID)
        {
            case 8:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 10, pTransform.position.z);
                break;
            case 9:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 20, pTransform.position.z);
                break;
            case 12:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 8, pTransform.position.z);
                break;
            case 13:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 15, pTransform.position.z);
                break;
            case 14:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 20, pTransform.position.z);
                break;
            case 15:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 10, pTransform.position.z);
                break;
            case 16:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 10, pTransform.position.z);
                break;
            case 17:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 15, pTransform.position.z);
                break;
            case 18:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 15, pTransform.position.z);
                break;
            case 19:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 15, pTransform.position.z);
                break;
            case 20:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 15, pTransform.position.z);
                break;
            case 22:
                pTransform.position = new Vector3(pTransform.position.x, pTransform.position.y - 15, pTransform.position.z);
                break;
            default:
                break;
        }
    }
	
	void adjustColor(int carID)
	{
		switch(carID)
		{
		case 2:
			changeMaterials[6].mainTexture = textureColor1;
			break;
		case 3:
			changeMaterials[6].mainTexture = textureColor3;
			break;
		case 4:
			changeMaterials[6].mainTexture = textureColor1;
			break;
		default:
			break;
		}
	}
	
	void loadChangePart(int carID)
	{
        if (carID == 0)
        {
            changeMaterials[0] = GameObject.FindGameObjectWithTag("changeWheel").renderer.material;
            changeMaterials[1] = GameObject.FindGameObjectWithTag("changeBody").renderer.material;
            changeMaterials[2] = GameObject.FindGameObjectWithTag("changeFrontWheel").renderer.material;
            changeMaterials[3] = GameObject.FindGameObjectWithTag("changeBottom").renderer.material;
            changeMaterials[4] = GameObject.FindGameObjectWithTag("changePart5").renderer.material;
            changeMaterials[5] = GameObject.FindGameObjectWithTag("changePart6").renderer.material;
            mainPartMaterials = GameObject.FindGameObjectWithTag("otherPart").renderer.materials;
            changeMaterials[6] = mainPartMaterials[0];
        }
        else
        {
            changeMaterials[0] = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "changeWheel").renderer.material;
            changeMaterials[1] = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "changeBody").renderer.material;
            changeMaterials[2] = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "changeFrontWheel").renderer.material;
            changeMaterials[3] = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "changeBottom").renderer.material;
            changeMaterials[4] = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "changePart5").renderer.material;
            changeMaterials[5] = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "changePart6").renderer.material;
            mainPartMaterials = GameObject.FindGameObjectWithTag("f" + (carID + 1).ToString() + "main").renderer.materials;
            changeMaterials[6] = mainPartMaterials[0];
        }
        if (carID == 3) changeMaterials[6] = mainPartMaterials[1];
        else if (carID == 15) changeMaterials[6] = mainPartMaterials[1];
        else if (carID == 19) changeMaterials[6] = mainPartMaterials[1];
        else if (carID == 22) changeMaterials[6] = mainPartMaterials[1];
        //mark original color
        for (int i = 0; i <= 5; i++)
        {
            originalColorList[i] = new Color(changeMaterials[i].color.r, changeMaterials[i].color.g, changeMaterials[i].color.b, 1.0f);
        }
	}

    void initOriginalColorArray()
    {
        originalColorArray[0] = 3;
        originalColorArray[1] = 3;
        originalColorArray[2] = 0;
        originalColorArray[3] = 2;
        originalColorArray[4] = 0;
        originalColorArray[5] = 4;
        originalColorArray[6] = 3;
        originalColorArray[7] = 1;
        originalColorArray[8] = 4;
        originalColorArray[9] = 3;
        originalColorArray[10] = 5;
        originalColorArray[11] = 0;
        originalColorArray[12] = 2;
        originalColorArray[13] = 1;
        originalColorArray[14] = 4;
        originalColorArray[15] = 3;
        originalColorArray[16] = 3;
        originalColorArray[17] = 0;
        originalColorArray[18] = 1;
        originalColorArray[19] = 5;
        originalColorArray[20] = 4;
        originalColorArray[21] = 2;
        originalColorArray[22] = 3;
    }

    void initRealIDList()
    {
        realIDArray[0] = 3;
        realIDArray[1] = 4;
        realIDArray[2] = 5;
        realIDArray[3] = 8;
        realIDArray[4] = 10;
        realIDArray[5] = 11;
        realIDArray[6] = 1;
        realIDArray[7] = 2;
        realIDArray[8] = 6;
        realIDArray[9] = 7;
        realIDArray[10] = 9;
        //realIDArray[11] = 12;
        for (int i = 11; i <= 23; i++)
        {
            realIDArray[i] = i + 1;
        }
    }
	
	void setUIPosition()
	{
		MATBTNONE_MARGIN_LEFT = screenWidth - 510;
		MATBTNTWO_MARGIN_LEFT = screenWidth - 255;
		MATBTNONE_MARGIN_UP = screenHeight/2 - 260;
		MATBTNTWO_MARGIN_UP = screenHeight/2 - 50;
		MATBTNTHREE_MARGIN_UP = screenHeight/2 + 170;
		BUTTON_WIDTH = 140;
		BACKBTN_MARGIN_LEFT = screenWidth/2 + 368;
		CONFIRMBTN_MARGIN_LEFT = screenWidth - 247;
		BACKBTN_MARGIN_UP = screenHeight - 150;
		BACKBTN_WIDTH = 210;
		BACKBTN_HEIGHT = 110;
		PREVBTN_MARGIN_LEFT = screenWidth/2 - 400;
		PREVBTN_MARGIN_UP = screenHeight/2 - 135;
		PREVBTN_WIDTH = 250;
		PREVBTN_HEIGHT = 105;
		NEXTBTN_MARGIN_LEFT = screenWidth/2 + 360;
		RETURNBTN_MARGIN_LEFT = screenWidth/2 - 400;
	}
	
	void changeMaterial(int materialID)
	{
		switch(materialID)
		{
		case 0:
			//GameObject.Find("che001_part01").renderer.material.SetColor("_Color",Color.red);
			for(int i = 0; i<=5; i++)
			{
				//changeMaterials[i].SetColor("_Color",Color.red);
                changeMaterials[i].mainTexture = textureColor1;
			}
			changeMaterials[6].mainTexture = textureColor1;
			break;
		case 1:
			for(int i = 0; i<=5; i++)
			{
				//changeMaterials[i].SetColor("_Color",Color.white);
                changeMaterials[i].mainTexture = textureColor2;
			}
			changeMaterials[6].mainTexture = textureColor2;
			break;
		case 2:
			for(int i = 0; i<=5; i++)
			{
				//changeMaterials[i].SetColor("_Color",Color.yellow);
                changeMaterials[i].mainTexture = textureColor3;
			}
			changeMaterials[6].mainTexture = textureColor3;
			break;
		case 3:
			for(int i = 0; i<=5; i++)
			{
				//changeMaterials[i].SetColor("_Color",Color.blue);
                changeMaterials[i].mainTexture = textureColor4;
			}
			changeMaterials[6].mainTexture = textureColor4;
			break;
		case 4:
			for(int i = 0; i<=5; i++)
			{
				//changeMaterials[i].SetColor("_Color",Color.green);
                changeMaterials[i].mainTexture = textureColor5;
			}
			changeMaterials[6].mainTexture = textureColor5;
			break;
		case 5:
			for(int i = 0; i<=5; i++)
			{
				//changeMaterials[i].SetColor("_Color",Color.magenta);
                changeMaterials[i].mainTexture = textureColor6;
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
	
	void setResume(int carID)
	{
		for(int i=0; i<=5; i++)
		{
			changeMaterials[i].color = originalColorList[i];
		}
        for (int i = 0; i <= 6; i++)
        {
            changeMaterials[i].mainTexture = convertCarIDToTexture(carID);
        }
        currentColorID = convertCarIDToColorID(carID);
		shouldCarRotate = true;
	}

    Texture convertCarIDToTexture(int carID)
    {
        switch (originalColorArray[carID])
        {
            case 0:
                return textureColor1;
                break;
            case 1:
                return textureColor2;
                break;
            case 2:
                return textureColor3;
                break;
            case 3:
                return textureColor4;
                break;
            case 4:
                return textureColor5;
                break;
            case 5:
                return textureColor6;
                break;
            default:
                return textureColor1;
                break;
        }
    }

    int convertCarIDToColorID(int carID)
    {
        switch (originalColorArray[carID])
        {
            case 0:
                return COLOR_RED;
                break;
            case 1:
                return COLOR_BLACK;
                break;
            case 2:
                return COLOR_YELLOW;
                break;
            case 3:
                return COLOR_GREY;
                break;
            case 4:
                return COLOR_MAGENTA;
                break;
            case 5:
                return COLOR_WHITE;
                break;
            default:
                return COLOR_RED;
                break;
        }
    }
	
	void generateID()
	{
		switch(currentCarID)
		{
		case 0:
			carIDString = string.Format("3");
			break;
		case 1:
			carIDString = string.Format("4");
			break;
		case 2:
			carIDString = string.Format("5");
			break;
		case 3:
			carIDString = string.Format("8");
			break;
		case 4:
			carIDString = string.Format("10");
			break;
		case 5:
			carIDString = string.Format("11");
			break;
        case 6:
            carIDString = string.Format("1");
            break;
       case 7:
            carIDString = string.Format("2");
            break;
            case 8:
            carIDString = string.Format("6");
            break;
            case 9:
            carIDString = string.Format("7");
            break;
            case 10:
            carIDString = string.Format("9");
            break;
		default:
            carIDString = string.Format((currentCarID+1).ToString());
			break;
		}
		switch(currentColorID)
		{
		case 0:
			colorIDString = string.Format("hong");
			break;
		case 1:
			colorIDString = string.Format("cheng");
			break;
		case 2:
			colorIDString = string.Format("huang");
			break;
		case 3:
			colorIDString = string.Format("lan");
			break;
		case 4:
			colorIDString = string.Format("lv");
			break;
		case 5:
			colorIDString = string.Format("zi");
			break;
		default:
			break;
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
