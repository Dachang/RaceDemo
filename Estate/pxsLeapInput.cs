/******************************************************************************\
* This is a Singleton class that emulates the axis input that nmost games have *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

/// <summary>

/// </summary>
public static class pxsLeapInput 
{	
	private enum HandID : int
	{
		Primary		= 0,
		Secondary	= 1
	};
	
	//Create a new leap controller object when you create this static class 
	static Leap.Controller 		m_controller;
	public static Leap.Frame			m_Frame;
	static Leap.Hand			m_Hand;
	static string Errors 			= "";

    public static float MOUSE_FACTOR_X = 1.2f;
    public static float MOUSE_FACTOR_Y = 2f;

    public static Vector2 mousePos;
    public static Vector2 handPos;

    public static bool isCircling = false;
	
	// constructor called when the first class or member is referenced.
	static pxsLeapInput()
	{
		try
		{
			//Create a new leap controller object when you create this static class 
			m_controller	= new Leap.Controller();
		}
		catch 
		{
			Errors = Errors + '\r' + '\n'  + "Controller could not be created";
		}
	}

	public static Leap.Frame Frame
	{
		get { return m_Frame; }
	}
	
	public static Leap.Hand Hand
	{
		get { return m_Hand; }
	}

	public static void Update() 
	{	
		if( m_controller != null )
		{
			
			Frame lastFrame = m_Frame == null ? Frame.Invalid : m_Frame;
			m_Frame	= m_controller.Frame();
			if (m_Frame != null)
			{
				if (m_Frame.Hands.Count > 0)
				{
                    //Debug.Log(m_Frame.Hands.Count);
					m_Hand = m_Frame.Hands[0];
				}
			}
		}
	}
	
	// returns the hand axis scaled from -1 to +1
	public static float GetHandAxis(string axisName)
	{
		float ret = GetHandAxisPrivate(axisName, true, false);
		return ret;
	}
	
	public static float GetHandAxisRaw(string axisName)
	{
		float ret = GetHandAxisPrivate(axisName, false, true);
		return ret;
	}

    public static float getLeapGesture()
    {
        float ret = getLeapGesturePrivate();
        return ret;
    }
	
	public static int GetFingerCount()
	{
		Update();
		if(m_Hand!=null)
		{
			return m_Hand.Fingers.Count;
		}
		else return 0;
	}

    public static int getLeftHandFingerCount()
    {
        Update();
        if (m_Frame.Hands.Count == 2)
            return m_Frame.Hands.Leftmost.Fingers.Count;
        else return 0;
    }

	private static float GetHandAxisPrivate(string axisName, bool scaled, bool isForRotate)
	{
		// Call Update so you can get the latest frame and hand
		Update();
		float ret = 0.0F;
		if (m_Hand != null)
		{
			Vector3 PalmPosition = new Vector3(0,0,0);
			Vector3 PalmNormal = new Vector3(0,0,0);
			Vector3 PalmDirection = new Vector3(0,0,0);
			if (scaled == true)
			{
                PalmPosition = m_Hand.StabilizedPalmPosition.ToUnityTranslated();
				PalmNormal = m_Hand.PalmNormal.ToUnity();				
				PalmDirection = m_Hand.Direction.ToUnity();
			}
			else
			{
                PalmPosition = m_Hand.StabilizedPalmPosition.ToUnity();
				PalmNormal = m_Hand.PalmPosition.ToUnity();
				PalmDirection = m_Hand.Direction.ToUnity();
			}
			switch (axisName)
			{
			case "Horizontal":
				ret = PalmPosition.x * 0.5f;
				break;
			case "Vertical":
				ret = PalmPosition.y * 0.2f;
				break;
			case "Depth":
				ret = PalmPosition.z ;
				break;
			case "Rotation":
				ret = -2 * PalmNormal.x ;

				break;
			case "Tilt":
				ret = PalmNormal.z * 50f;
				break;
			case "HorizontalDirection":
				ret = PalmDirection.x ;
				break;
			case "VericalDirection":
				ret = PalmDirection.y ;
				break;
			default:
				break;
			}
		}
		if (scaled == true)
				{
					if (ret > 1) {ret = 1;}
					if (ret < 0) {ret = 0;}
				}
        if (isForRotate)
        {
            if (ret > 1) ret = 1;
            if (ret < -1) ret = -1;
        }
		return ret;
	}

    private static float getLeapGesturePrivate()
    {
        Update();
        float gestureData = 0.0f;
        m_controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
        GestureList gestureList = m_Frame.Gestures();
        for (int i = 0; i < gestureList.Count; i++)
        {
            Gesture gesture = gestureList[i];
            switch (gesture.Type)
            {
                case Gesture.GestureType.TYPECIRCLE:
                    CircleGesture circle = new CircleGesture(gesture);
                    isCircling = true;
                    if (circle.Pointable.Direction.AngleTo(circle.Normal) <= Mathf.PI / 4) gestureData = addRangeTo(circle.Progress);
                    else gestureData = -addRangeTo(circle.Progress);
                    break;
                default:
                    isCircling = false;
                    break;
            }
        }
        if (gestureData == 0) isCircling = false;
        return gestureData;
    }

    private static float addRangeTo(float input)
    {
        if (input > 3f) input = 3f;
        return input;
    }

}
