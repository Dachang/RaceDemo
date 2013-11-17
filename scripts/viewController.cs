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
	//UI texture bitmaps
	/*private Texture2D compTextureOne;*/
	//whether the car should rotate or not
	private bool shouldCarRotate = true;
	
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		currentCar = GameObject.Find("currentCar");
		//compTextureOne = (Texture2D)(Resources.Load("texture1",typeof(Texture2D)));
		loadChangePart();
		setUIPosition();
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

		}
		//confirm button
		if(GUI.Button(new Rect(CONFIRMBTN_MARGIN_LEFT, BACKBTN_MARGIN_UP,BACKBTN_WIDTH,BACKBTN_HEIGHT), "Done"))
		{

		}
		//Material buttons
		if(GUI.Button(new Rect(MATBTNONE_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Red"))
		{
			changeMaterial(COLOR_RED);
		}
		if(GUI.Button(new Rect(MATBTNTWO_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Black"))
		{
			changeMaterial(COLOR_BLACK);
		}
		if(GUI.Button(new Rect(MATBTNTHREE_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Yellow"))
		{
			changeMaterial(COLOR_YELLOW);
		}
		if(GUI.Button(new Rect(MATBTNFOUR_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Grey"))
		{
			changeMaterial(COLOR_GREY);
		}
		if(GUI.Button(new Rect(MATBTNFIVE_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"Magenta"))
		{
			changeMaterial(COLOR_MAGENTA);
		}
		if(GUI.Button(new Rect(MATBTNSIX_MARGIN_LEFT,MATBTN_MARGIN_UP, BUTTON_WIDTH, BUTTON_WIDTH),"White"))
		{
			changeMaterial(COLOR_WHITE);
		}
		//draw component Textures
		//GUI.DrawTexture(new Rect(70,70,225,160), compTextureOne, ScaleMode.StretchToFill, true, 0);
	}
	
	void loadChangePart()
	{
		changeMaterials[0] = GameObject.Find("Audi_Q7-PART1").renderer.material;
		changeMaterials[1] = GameObject.Find("Audi_Q7-PART2").renderer.material;
		changeMaterials[2] = GameObject.Find("Audi_Q7-PART3").renderer.material;
		changeMaterials[3] = GameObject.Find("Audi_Q7-PART4").renderer.material;
		changeMaterials[4] = GameObject.Find("Audi_Q7-PART5").renderer.material;
		changeMaterials[5] = GameObject.Find("Audi_Q7-PART6").renderer.material;
	}
	
	void setUIPosition()
	{
		MATBTNONE_MARGIN_LEFT = screenWidth/2 - 240;
		MATBTNTWO_MARGIN_LEFT = screenWidth/2 - 160;
		MATBTNTHREE_MARGIN_LEFT = screenWidth/2 - 80;
		MATBTNFOUR_MARGIN_LEFT = screenWidth/2 + 10;
		MATBTNFIVE_MARGIN_LEFT = screenWidth/2 + 90;
		MATBTNSIX_MARGIN_LEFT = screenWidth/2 + 170;
		MATBTN_MARGIN_UP = screenHeight - 90;
		BUTTON_WIDTH = 70;
		BACKBTN_MARGIN_LEFT = 25;
		CONFIRMBTN_MARGIN_LEFT = screenWidth - 115;
		BACKBTN_MARGIN_UP = screenHeight - 70;
		BACKBTN_WIDTH = 94;
		BACKBTN_HEIGHT = 36;
	}
	
	void changeMaterial(int materialID)
	{
		switch(materialID)
		{
		case 0:
			GameObject.Find("Audi_Q7-PART1").renderer.material.SetColor("_Color",Color.red);
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.red);
			}
			break;
		case 1:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.black);
			}
			break;
		case 2:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.yellow);
			}
			break;
		case 3:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.grey);
			}
			break;
		case 4:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.magenta);
			}
			break;
		case 5:
			for(int i = 0; i<=5; i++)
			{
				changeMaterials[i].SetColor("_Color",Color.white);
			}
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
	
	//public interfaces
	public void pauseRotate()
	{
		shouldCarRotate = false;
	}
	
	public void resumeRotate()
	{
		shouldCarRotate = true;
	}
}
