using UnityEngine;
using System.Collections;

public class selectCar : MonoBehaviour {
	
	//current car
	public GameObject currentCar;
	//parent transform
	private Transform pTransform;
	private int currentCarID = 0;
	private int currentColorID = 0;
	//color ID
	private int COLOR_RED = 0;
	private int COLOR_BLACK = 1;
	private int COLOR_YELLOW = 2;
	private int COLOR_GREY = 3;
	private int COLOR_MAGENTA = 4;
	private int COLOR_WHITE = 5;
	//change materials
	Material[] changeMaterials = new Material[10];
	//materials
	private Material[] wheelMaterials = new Material[10];
	private Material[] frontWheelMaterials = new Material[10];
	private Material[] bodyMaterials = new Material[10];
	private Material[] bottomMaterials = new Material[10];
	private Material[] mainPartMaterials = new Material[10];
	//car List
	private GameObject[] carList = new GameObject[10];
	//Textures
	private Texture textureColor1,textureColor2,textureColor3,textureColor4,textureColor5,textureColor6;
	//car has chosen
	private bool isCarSelected = false;
	//color has selected
	private bool isColorSelected = false;
	
	// Use this for initialization
	void Start () 
	{
		currentCar = GameObject.Find("che001");
		pTransform = currentCar.transform;
		
		initCarList();
		loadChangePart(currentCarID);
		loadTexture(currentCarID);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isCarSelected)
		{
			currentCarID = PlayerPrefs.GetInt("CarID",0);
			switchCar(currentCarID);
			isCarSelected = true;
		}
		if(!isColorSelected)
		{
			currentColorID = PlayerPrefs.GetInt("ColorID",0);
			changeMaterial(currentColorID);
			isColorSelected = true;
		}
	}
	
	//init carList
	void initCarList()
	{
		//car
		carList[0] = GameObject.Find("che001");
		carList[1] = GameObject.Find("che002");
		carList[2] = GameObject.Find("che003");
		carList[3] = GameObject.Find("che004");
		carList[4] = GameObject.Find("che005");
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
			textureColor1 = (Texture)Resources.Load("che002.fbm/che002_hong_clr3",typeof(Texture));
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
		if(currentCarID != 0)
		{
			currentCar.SetActive(false);
			currentCar = carList[carID];
			currentCar.transform.position = pTransform.position;
			currentCar.transform.rotation = pTransform.rotation;
			currentCar.SetActive(true);
			currentCarID = carID;
	
			//fix position
			//positionFix(carID);
			//load
			loadTexture(currentCarID);
			loadChangePart(currentCarID);
			//findComponents(currentCarID);
			//setResume();
		}
	}
	
	void positionFix(int carID)
	{
		switch(carID)
		{
		case 0:
			break;
		case 1:
			float tempY1 = pTransform.position.y + 0.2f;
			currentCar.transform.position = new Vector3(pTransform.position.x,tempY1,pTransform.position.z);
			break;
		case 2:
			float tempY2 = pTransform.position.y + 0.9f;
			currentCar.transform.position = new Vector3(pTransform.position.x,tempY2,pTransform.position.z);
			break;
		case 3:
			float tempY3 = pTransform.position.y + 0.7f;
			currentCar.transform.position = new Vector3(pTransform.position.x,tempY3,pTransform.position.z);
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
	
	void changeMaterial(int materialID)
	{
		switch(materialID)
		{
		case 0:
			for(int i = 0; i<=3; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.red);
			}
			changeMaterials[4].mainTexture = textureColor1;
			break;
		case 1:
			for(int i = 0; i<=3; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.white);
			}
			changeMaterials[4].mainTexture = textureColor2;
			break;
		case 2:
			for(int i = 0; i<=3; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.yellow);
			}
			changeMaterials[4].mainTexture = textureColor3;
			break;
		case 3:
			for(int i = 0; i<=3; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.green);
			}
			changeMaterials[4].mainTexture = textureColor4;
			break;
		case 4:
			for(int i = 0; i<=3; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.blue);
			}
			changeMaterials[4].mainTexture = textureColor5;
			break;
		case 5:
			for(int i = 0; i<=3; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.magenta);
			}
			changeMaterials[4].mainTexture = textureColor6;
			break;
		default:
			break;
		}	
	}
	
	//public interface
	public int getCurrentCarID()
	{
		return currentCarID;
	}
	
	public void setIsCarSelected(bool isSelected)
	{
		isCarSelected = isSelected;
	}
	
	public void setIsColorSelected(bool isSelected)
	{
		isColorSelected = isSelected;
	}
}
