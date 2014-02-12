using UnityEngine;
using System.Collections;

public class keyboard : MonoBehaviour {
	
	public GUISkin mySkin;
	
	private int screenWidth;
	private int screenHeight;
	
	private int FIRST_ROW_START_POS_X = 506;
	private int FIRST_ROW_START_POS_Y = 612;
	private int SECOND_ROW_START_POS_Y = 682;
	private int THIRD_ROW_START_POS_X = 535;
	private int THIRD_ROW_START_POS_Y = 747;
	private int FOURTH_ROW_START_POS_X = 582;
	private int FOURTH_ROW_START_POS_Y = 814;
	private int KEY_BUTTON_WIDTH = 73;
	private int KEY_BUTTON_HEIGHT = 60;
	private int ENTER_BUTTON_WIDTH = 123;
	private int KEY_LEAK = 9;
	private float KEY_LEAK_FR = 7.7f;
	
	private string[] secondRowLetters = new string[20];
	private string[] thirdRowLetters =  new string[20];
	private string[] fourthRowLetters = new string[20];
	
	private string userInputString;
	private bool capsTag = false;
	
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		secondRowLetters[0] = string.Format("Q");
		secondRowLetters[1] = string.Format("W");
		secondRowLetters[2] = string.Format("E");
		secondRowLetters[3] = string.Format("R");
		secondRowLetters[4] = string.Format("T");
		secondRowLetters[5] = string.Format("Y");
		secondRowLetters[6] = string.Format("U");
		secondRowLetters[7] = string.Format("I");
		secondRowLetters[8] = string.Format("O");
		secondRowLetters[9] = string.Format("P");
		secondRowLetters[10] = string.Format("q");
		secondRowLetters[11] = string.Format("w");
		secondRowLetters[12] = string.Format("e");
		secondRowLetters[13] = string.Format("r");
		secondRowLetters[14] = string.Format("t");
		secondRowLetters[15] = string.Format("y");
		secondRowLetters[16] = string.Format("u");
		secondRowLetters[17] = string.Format("i");
		secondRowLetters[18] = string.Format("o");
		secondRowLetters[19] = string.Format("p");
		thirdRowLetters[0] = string.Format("A");
		thirdRowLetters[1] = string.Format("S");
		thirdRowLetters[2] = string.Format("D");
		thirdRowLetters[3] = string.Format("F");
		thirdRowLetters[4] = string.Format("G");
		thirdRowLetters[5] = string.Format("H");
		thirdRowLetters[6] = string.Format("J");
		thirdRowLetters[7] = string.Format("K");
		thirdRowLetters[8] = string.Format("L");
		thirdRowLetters[9] = string.Format("a");
		thirdRowLetters[10] = string.Format("s");
		thirdRowLetters[11] = string.Format("d");
		thirdRowLetters[12] = string.Format("f");
		thirdRowLetters[13] = string.Format("g");
		thirdRowLetters[14] = string.Format("h");
		thirdRowLetters[15] = string.Format("j");
		thirdRowLetters[16] = string.Format("k");
		thirdRowLetters[17] = string.Format("l");
		fourthRowLetters[0] = string.Format("Z");
		fourthRowLetters[1] = string.Format("X");
		fourthRowLetters[2] = string.Format("C");
		fourthRowLetters[3] = string.Format("V");
		fourthRowLetters[4] = string.Format("B");
		fourthRowLetters[5] = string.Format("N");
		fourthRowLetters[6] = string.Format("M"); 
		fourthRowLetters[7] = string.Format("_"); 
		fourthRowLetters[8] = string.Format("-");
		fourthRowLetters[9] = string.Format("z");
		fourthRowLetters[10] = string.Format("x");
		fourthRowLetters[11] = string.Format("c");
		fourthRowLetters[12] = string.Format("v");
		fourthRowLetters[13] = string.Format("b");
		fourthRowLetters[14] = string.Format("n");
		fourthRowLetters[15] = string.Format("m");
		fourthRowLetters[16] = string.Format("_");
		fourthRowLetters[17] = string.Format("-"); 
	}
	
	void Update () 
	{
		Time.timeScale = 1.0f;
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		GUI.skin.label.fontSize = 40;
		GUI.skin.button.fontSize = 30;
		//First Row NUMBER
		for(int i = 1; i <= 9; i++)
		{
			if(GUI.Button(new Rect(FIRST_ROW_START_POS_X + i*KEY_LEAK + KEY_BUTTON_WIDTH*(i-1),FIRST_ROW_START_POS_Y,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),i.ToString()))
			{
				userInputString += i.ToString();
			}
		}
		if(GUI.Button(new Rect(FIRST_ROW_START_POS_X + 10*KEY_LEAK + KEY_BUTTON_WIDTH*9,FIRST_ROW_START_POS_Y,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),"0"))
		{
			userInputString += string.Format("0");
		}
		if(GUI.Button(new Rect(FIRST_ROW_START_POS_X + 11*KEY_LEAK + KEY_BUTTON_WIDTH*10,FIRST_ROW_START_POS_Y,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),"."))
		{
			userInputString += string.Format(".");
		}
		//Second Row QWERTYUIOP
		for(int i = 1; i <= 10; i++)
		{
			if(GUI.Button(new Rect(FIRST_ROW_START_POS_X + i*KEY_LEAK + KEY_BUTTON_WIDTH*(i-1),SECOND_ROW_START_POS_Y-2,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),secondRowLetters[i-1]))
			{
				if(capsTag) userInputString += secondRowLetters[i-1];
				else userInputString += secondRowLetters[i+9];
			}
		}
		//Third Row ASDFGHJKL
		for(int i = 1; i <= 9; i++)
		{
			if(GUI.Button(new Rect(THIRD_ROW_START_POS_X + i*KEY_LEAK + KEY_BUTTON_WIDTH*(i-1),THIRD_ROW_START_POS_Y,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),thirdRowLetters[i-1]))
			{
				if(capsTag)	userInputString += thirdRowLetters[i-1];
				else userInputString += thirdRowLetters[i+8];
			}
		}
		//Fourth Row ZXCVBNM_-
		for(int i = 1; i <= 9; i++)
		{
			if(GUI.Button(new Rect(FOURTH_ROW_START_POS_X + i*KEY_LEAK_FR + KEY_BUTTON_WIDTH*(i-1),FOURTH_ROW_START_POS_Y,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),fourthRowLetters[i-1]))
			{
				if(capsTag)	userInputString += fourthRowLetters[i-1];
				else userInputString += fourthRowLetters[i+8];
			}
		}
		//Backspace
		if(GUI.Button(new Rect(FIRST_ROW_START_POS_X + 11*KEY_LEAK + KEY_BUTTON_WIDTH*10,SECOND_ROW_START_POS_Y-2,
				KEY_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),"退格"))
		{
			userInputString = userInputString.Substring(0,userInputString.Length-1);
		}
		//Enter
		if(GUI.Button(new Rect(THIRD_ROW_START_POS_X + 10*KEY_LEAK + KEY_BUTTON_WIDTH*9,THIRD_ROW_START_POS_Y,
				ENTER_BUTTON_WIDTH,KEY_BUTTON_HEIGHT),"回车"))
		{
			//enter
		}
		//Capslock
		if(GUI.Button(new Rect(FIRST_ROW_START_POS_X+8,FOURTH_ROW_START_POS_Y,
				KEY_BUTTON_WIDTH-2,KEY_BUTTON_HEIGHT),"大写"))
		{
			capsTag = !capsTag;
		}
		//@
		if(GUI.Button(new Rect(FOURTH_ROW_START_POS_X + 10*KEY_LEAK_FR + KEY_BUTTON_WIDTH*9,FOURTH_ROW_START_POS_Y,
			KEY_BUTTON_WIDTH+16,KEY_BUTTON_HEIGHT),"@"))
		{
			userInputString += string.Format("@");
		}
		GUI.Label(new Rect(screenWidth/2-300,500,1000,200),userInputString);
		//Done Button
		//confirm button
//		if(GUI.Button(new Rect(screenWidth - 247, screenHeight - 150,210,110), "Done"))
//		{
//			Application.LoadLevel("FlyScene");
//		}
		//exitButton
		if(GUI.Button(new Rect(10,screenHeight-60,50,50),"ESC"))
		{
			Application.Quit();
		}
	}
	
//	void OnApplicationQuit()
//	{
//		Application.CancelQuit();
//		System.Diagnostics.Process.GetCurrentProcess().Kill();
//	}
	
	//public interface
	public string getInputAddress()
	{
		return userInputString;
	}
}
