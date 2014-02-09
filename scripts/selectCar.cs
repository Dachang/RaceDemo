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
	//those components who need change textures
	private Material[] car3changeBodyMats = new Material[10];
	private Material[] car3changeWheelMats = new Material[10];
	private Material[] car4changeBodyMats = new Material[10];
	private Material[] car4changeBottomMats = new Material[10];
	private Material[] car5changeWheelMats = new Material[10];
	private Material[] car5changeBottomMats = new Material[10];
	private Material[] car5changeBodyMats = new Material[10];
	private Material[] car6changeWheelMats = new Material[10];
	private Material[] car6changeBottomMats = new Material[10];
	private Material[] car7changeBodyMats = new Material[10];
	private Material[] car7changeBottomMats = new Material[10];
	private Material[] car8changeBodyMats = new Material[10];
	private Material[] car8changeBottomMats = new Material[10];
	
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
			car3changeWheelMats = GameObject.FindGameObjectWithTag("car3changeWheel").renderer.materials;
			car3changeBodyMats = GameObject.FindGameObjectWithTag("car3changeBody").renderer.materials;
			changeMaterials[5] = car3changeWheelMats[1];
			changeMaterials[6] = car3changeBodyMats[1];
			break;
		case 3:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car4changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car4changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car4changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car4changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car4main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[1];
			car4changeBodyMats = GameObject.FindGameObjectWithTag("car4changeBody").renderer.materials;
			car4changeBottomMats = GameObject.FindGameObjectWithTag("car4changeBottom").renderer.materials;
			changeMaterials[5] = car4changeBodyMats[1];
			changeMaterials[6] = car4changeBottomMats[2];
			break;
		case 4:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car5changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car5changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car5changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car5changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car5main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[4];
			car5changeWheelMats = GameObject.FindGameObjectWithTag("car5changeWheel").renderer.materials;
			car5changeBodyMats = GameObject.FindGameObjectWithTag("car5changeBody").renderer.materials;
			car5changeBottomMats = GameObject.FindGameObjectWithTag("car5changeBottom").renderer.materials;
			changeMaterials[5] = car5changeWheelMats[4];
			changeMaterials[6] = car5changeBodyMats[1];
			changeMaterials[7] = car5changeBottomMats[0];
			break;
		case 5:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car6changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car6changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car6changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car6changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car6main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[8];
			car6changeWheelMats = GameObject.FindGameObjectWithTag("car6changeWheel").renderer.materials;
			car6changeBottomMats = GameObject.FindGameObjectWithTag("car6changeBottom").renderer.materials;
			changeMaterials[5] = car6changeWheelMats[2];
			changeMaterials[6] = car6changeBottomMats[3];
			break;
		case 6:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car7changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car7changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car7changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car7changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car7main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[5];
			car7changeBodyMats = GameObject.FindGameObjectWithTag("car7changeBody").renderer.materials;
			car7changeBottomMats = GameObject.FindGameObjectWithTag("car7changeBottom").renderer.materials;
			changeMaterials[5] = car7changeBodyMats[1];
			changeMaterials[6] = car7changeBottomMats[2];
			break;
		case 7:
			changeMaterials[0] = GameObject.FindGameObjectWithTag("car8changeWheel").renderer.material;
			changeMaterials[1] = GameObject.FindGameObjectWithTag("car8changeBody").renderer.material;
			changeMaterials[2] = GameObject.FindGameObjectWithTag("car8changeFrontWheel").renderer.material;
			changeMaterials[3] = GameObject.FindGameObjectWithTag("car8changeBottom").renderer.material;
			mainPartMaterials = GameObject.FindGameObjectWithTag("car8main").renderer.materials;
			changeMaterials[4] = mainPartMaterials[3];
			car8changeBodyMats = GameObject.FindGameObjectWithTag("car8changeBody").renderer.materials;
			car8changeBottomMats = GameObject.FindGameObjectWithTag("car8changeBottom").renderer.materials;
			changeMaterials[5] = car8changeBodyMats[5];
			changeMaterials[6] = car8changeBottomMats[4];
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
				changeMaterials[4].mainTexture = textureColor1;
				break;
			case 1:
				changeMaterials[4].mainTexture = textureColor2;
				break;
			case 2:
				changeMaterials[4].mainTexture = textureColor3;
				break;
			case 3:
				changeMaterials[4].mainTexture = textureColor4;
				break;
			case 4:
				changeMaterials[4].mainTexture = textureColor5;
				break;
			case 5:
				changeMaterials[4].mainTexture = textureColor6;
				break;
			default:
				break;
		}
		changeComponentMaterial(materialID);	
	}
	
	void changeComponentMaterial(int materialID)
	{
		switch(currentCarID)
		{
			case 2:
				changeMaterials[5].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[6].mainTexture = convertMaterialIDtoTexture(materialID);
				break;
			case 3:
				changeMaterials[5].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[6].mainTexture = convertMaterialIDtoTexture(materialID);
				break;
			case 4:
				changeMaterials[5].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[6].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[7].mainTexture = convertMaterialIDtoTexture(materialID);
				break;
			case 5:
				changeMaterials[1].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[5].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[6].mainTexture = convertMaterialIDtoTexture(materialID);
				break;
			case 6:
				changeMaterials[5].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[6].mainTexture = convertMaterialIDtoTexture(materialID);
				break;
			case 7:
				changeMaterials[5].mainTexture = convertMaterialIDtoTexture(materialID);
				changeMaterials[6].mainTexture = convertMaterialIDtoTexture(materialID);
				break;
			default:
				break;
		}
	}
	
	private Texture convertMaterialIDtoTexture(int materialID)
	{
		switch(materialID)
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
