using UnityEngine;
using System.Collections;
//default resolution is 1920x1080
public class viewController : MonoBehaviour 
{
    //GUI Textures
    public Texture2D tabbarBG, screenShot1, screenShot2, screenShot3, screenShot4,
                     screenShot5, screenShot6, screenShot7, cursorImage, renderImage1, closeBtnImage;
    private float defaultScreenWidth = 1920f;
    private float defaultScreenHeight = 1080f;
    private int SCREEN_SHOTS_NUM = 7;
    private Texture2D[] screenShots;
    private Rect[] screenshotRect;
    private Rect closeBtnRect;
    private float SCREEN_SHOTS_GAP = 20f;

    private Rect cursorCorrdinate;
    private float fingerPosX, fingerPosY, lastFingerPosX, lastFingerPosY;
    private float MOUSE_POS_SCALE_FACTOR_X = 10f;
    private float MOUSE_POS_SCALE_FACTOR_Y = 10f;

    private int fingerCount = 0;
    private bool renderImageTrigger = false;

    private float inc = 1.1f;
    private float inc_offset = 0.5f;
    public Texture[] spinMouseTexture;
    private int spinIndex = 0;
    private int SPIN_COUNT = 19;
    private bool isMouseSpinning = false;
    private int mouseState = 0;
	
	void Start () 
    {
        screenShots = new Texture2D[SCREEN_SHOTS_NUM];
        screenShots[0] = screenShot1;
        screenShots[1] = screenShot2;
        screenShots[2] = screenShot3;
        screenShots[3] = screenShot4;
        screenShots[4] = screenShot5;
        screenShots[5] = screenShot6;
        screenShots[6] = screenShot7;
        cursorCorrdinate = new Rect(0, 0, 50, 50);
        initScreenShotPosition();
	}
	
	void Update () 
    {
        fingerPosX = pxsLeapInput.m_Frame.Fingers[0].StabilizedTipPosition.x * MOUSE_POS_SCALE_FACTOR_X;
        fingerPosY = pxsLeapInput.m_Frame.Fingers[0].StabilizedTipPosition.y * MOUSE_POS_SCALE_FACTOR_Y;
        float resizeFactor = Screen.width / defaultScreenWidth;
        cursorCorrdinate = new Rect(cursorCorrdinate.x + fingerPosX - lastFingerPosX,
            cursorCorrdinate.y + lastFingerPosY - fingerPosY, 50, 50);
        lastFingerPosX = fingerPosX;
        lastFingerPosY = fingerPosY;
        calibrateMouse();
	}

    void OnGUI()
    {
        fingerCount = pxsLeapInput.m_Frame.Fingers.Count;
        float resizeFactor = Screen.width / defaultScreenWidth;
        checkMousePosition();
        //draw tabbar
        GUI.DrawTexture(new Rect(0, 900f * resizeFactor, Screen.width,
            300f * resizeFactor), tabbarBG, ScaleMode.StretchToFill, true, 0);
        //draw screen shots
        for (int i = 0; i < SCREEN_SHOTS_NUM; i++)
        {
            GUI.DrawTexture(new Rect((110 + SCREEN_SHOTS_GAP * i + screenShots[i].width * i) * resizeFactor, 920f * resizeFactor,
                screenShots[i].width * resizeFactor, screenShots[i].height * resizeFactor), screenShots[i],
                ScaleMode.StretchToFill, true, 0);
        }
        //draw render image
        if (renderImageTrigger)
        {
            GUI.DrawTexture(new Rect((1920f / 2 - renderImage1.width / 2) * resizeFactor,
                (1080f / 2 - renderImage1.height / 2 - 100f) * resizeFactor, renderImage1.width * resizeFactor,
                renderImage1.height * resizeFactor), renderImage1, ScaleMode.StretchToFill, true, 0);
            GUI.DrawTexture(new Rect((1920f / 2 + renderImage1.width / 2) * resizeFactor,
                (1080f / 2 - renderImage1.height / 2 - 160f) * resizeFactor, closeBtnImage.width * resizeFactor,
                closeBtnImage.height * resizeFactor), closeBtnImage, ScaleMode.StretchToFill, true, 0);
        }
        //draw mouse
        if(fingerCount == 1 && isMouseSpinning == false) 
            GUI.DrawTexture(cursorCorrdinate, cursorImage, ScaleMode.StretchToFill, true, 0);
        //draw spinning mouse
        if (isMouseSpinning)
        {
            if (spinIndex < SPIN_COUNT - 1)
            {
                GUI.DrawTexture(cursorCorrdinate, spinMouseTexture[spinIndex], ScaleMode.StretchToFill, true);
                spinIndex = IncreaseOne(spinIndex);
            }
        }
        if (spinIndex == SPIN_COUNT - 1)
        {
            isMouseSpinning = false;
            if (mouseState == 0) renderImageTrigger = true;
            else if (mouseState == 1) renderImageTrigger = false;
            spinIndex = 0;
        }
    }

    void initScreenShotPosition()
    {
        float resizeFactor = Screen.width / defaultScreenWidth;
        screenshotRect = new Rect[SCREEN_SHOTS_NUM];
        for (int i = 0; i < SCREEN_SHOTS_NUM; i++)
        {
            screenshotRect[i] = new Rect((110 + SCREEN_SHOTS_GAP * i + screenShots[i].width * i) * resizeFactor, 920f * resizeFactor,
                screenShots[i].width * resizeFactor, screenShots[i].height * resizeFactor);
        }
        closeBtnRect = new Rect((1920f / 2 + renderImage1.width / 2) * resizeFactor,
                (1080f / 2 - renderImage1.height / 2 - 160f) * resizeFactor, closeBtnImage.width * resizeFactor,
                closeBtnImage.height * resizeFactor);
    }

    void checkMousePosition()
    {
        if (fingerCount == 1)
        {
            if (screenshotRect[0].Contains(new Vector3(cursorCorrdinate.x, cursorCorrdinate.y)) && renderImageTrigger == false)
            {
                isMouseSpinning = true;
                mouseState = 0;
            }
            if (closeBtnRect.Contains(new Vector3(cursorCorrdinate.x, cursorCorrdinate.y)) && renderImageTrigger == true)
            { 
                isMouseSpinning = true;
                mouseState = 1;
            }
        }
    }

    void calibrateMouse()
    {
        if (cursorCorrdinate.x < 0) cursorCorrdinate.x = 0;
        else if (cursorCorrdinate.x > Screen.width - 50f) cursorCorrdinate.x = Screen.width - 50f;
        if (cursorCorrdinate.y < 0) cursorCorrdinate.y = 0;
        else if (cursorCorrdinate.y > Screen.height - 50f) cursorCorrdinate.y = Screen.height - 50f;
    }

    // A function that helps the increase slower
    private int IncreaseOne(int cur)
    {
        if (inc > 1) { cur += 1; inc = 0; }
        inc += inc_offset;
        return cur;
    }
}
