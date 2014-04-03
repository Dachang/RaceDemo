using UnityEngine;
using System.Collections;
//default resolution is 1920x1080
public class viewController : MonoBehaviour 
{
    //GUI Textures
    public Texture2D tabbarBG, screenShot1, screenShot2, screenShot3, screenShot4,
                     screenShot5, screenShot6, screenShot7, cursorImage;
    private float defaultScreenWidth = 1920f;
    private float defaultScreenHeight = 1080f;
    private int SCREEN_SHOTS_NUM = 7;
    private Texture2D[] screenShots;
    private float SCREEN_SHOTS_GAP = 20f;

    private Rect cursorCorrdinate;
	
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
	}
	
	void Update () 
    {
        float resizeFactor = Screen.width / defaultScreenWidth;
        cursorCorrdinate = new Rect(pxsLeapInput.GetHandAxis("Horizontal") * 1920 * resizeFactor,
            (1080 - pxsLeapInput.GetHandAxis("Vertical") * 1080) * resizeFactor, 50, 50);
	}

    void OnGUI()
    {
        float resizeFactor = Screen.width / defaultScreenWidth;
        GUI.DrawTexture(new Rect(0, 900f * resizeFactor, Screen.width,
            300f * resizeFactor), tabbarBG, ScaleMode.StretchToFill, true, 0);

        for (int i = 0; i < SCREEN_SHOTS_NUM; i++)
        {
            GUI.DrawTexture(new Rect((110 + SCREEN_SHOTS_GAP * i + screenShots[i].width * i) * resizeFactor, 920f * resizeFactor,
                screenShots[i].width * resizeFactor, screenShots[i].height * resizeFactor), screenShots[i],
                ScaleMode.StretchToFill, true, 0);
        }

        GUI.DrawTexture(cursorCorrdinate, cursorImage, ScaleMode.StretchToFill, true, 0);
    }
}
