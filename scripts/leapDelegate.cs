using UnityEngine;
using System.Collections;

public class leapDelegate : MonoBehaviour {

    private bool leapIsEnabled;
    private Car carScript;
    private Timer tScript;
	// Use this for initialization
	void Start () 
    {
        leapIsEnabled = false;
        carScript = (Car)this.GetComponent("Car");
        tScript = (Timer)Camera.main.GetComponent(typeof(Timer));
	}
	
	// Update is called once per frame
	void Update () 
    {
        checkInput();
        if (leapIsEnabled)
        {
            if (carScript.getRaceBegin())
            {
                tScript.endFakeCountDown();
                carScript.setThrottleValue(5f);
                carScript.setSteerValue(pxsLeapInput.GetHandAxis("Rotation"));
                if (pxsLeapInput.GetFingerCount() > 1)
                {
                    carScript.setAbleToMove(true);
                }
                else
                {
                    carScript.setThrottleValue(-50f);
                    //carScript.setAbleToMove(false);
                }
            }
        }
        else
        {
            //keyboard input
        }
	}

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            leapIsEnabled = !leapIsEnabled;
            carScript.setLeapIsTriggered();
        }
    }
}
