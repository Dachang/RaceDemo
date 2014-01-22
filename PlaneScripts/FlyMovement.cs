using UnityEngine;
using System.Collections;

public class FlyMovement : MonoBehaviour {

    private float planeAngleY=0;
    private bool isGameOver=false;

    private float rotationX=0;
    private float rotationY=0;
    private float rotationZ=0;

    private float positionX=0;
    private float positionY=0;
    private float positionZ=0;

    public float flySpeed=750;

    private float upLiftForce=0;
    private float pseudogravitation=-0.3f;

    private float RightLeftSoft=0;
    private float RightLeftSoftAbs=0;

    private float divesalto=0;
    private float diveblocker=0;

    private int scoreRate = 100;
    private int myScore = 0;
	//sound effect
	public AudioClip coinSound;
	//leap enabled
	public bool leapIsEnabled;
	//movement factors
	private float factorX = 0;
	private float factorY1 = 0;
	private float factorY2 = 0;
	//leap control adjust factor
	private int tempFactorX = 100;
	private int tempFactorY = 200;
	private float factorLeapX = 1.0f;
	private float factorLeapY = 2.0f;
	
    void Start()
    {
        //this.gameObject.AddComponent("BoxCollider");
        //BoxCollider boxCollider = new BoxCollider();
        //////boxCollider.size =
        //BoxCollider temp = (BoxCollider)this.gameObject.transform.collider;
        //boxCollider.size = temp.size * 100;


        //this.gameObject.transform.collider.ClosestPointOnBounds(boxCollider.size);
        //this.gameObject.AddComponent<BoxCollider>();
        

    }
	
	// Update is called once per frame
	void Update () {
        Movement();
		switchControl();
		adjustLeapControl();
	}

    private void Movement()
    {
        if (!isGameOver)
        {
            rotationX = transform.eulerAngles.x;
            rotationY = transform.eulerAngles.y;
            rotationZ = transform.eulerAngles.z;
            positionX = transform.position.x;
            positionY = transform.position.y;
            positionZ = transform.position.z;
            
            //forward speed
            transform.Translate(0, 0, flySpeed / 10 * Time.deltaTime);

            //
            UpAndDonw();
            LeftAndRight();
        }

        
    }

    private void UpAndDonw()
    {
        //if (Input.GetMouseButton(0))
        //{
        //use keyboard wsad 
        //float factorY = Input.GetAxis("Mouse Y")*2;
		if(leapIsEnabled == true)
		{
			factorY1 = -pxsLeapInput.GetHandAxis("Tilt") * factorLeapY;
		}
		else
		{
        	factorY1 = Input.GetAxis("Vertical");
		}
        if (factorY1 <= 0)
        {
            transform.Rotate((factorY1 * Time.deltaTime * 80), 0, 0);
        }
        else if (factorY1 > 0)
        {
            transform.Rotate((float)(0.8 - divesalto) * (factorY1 * Time.deltaTime * 80), 0, 0);
        }
		Debug.Log(factorY1);
        // }
    }

    private void LeftAndRight()
    {
        //if (Input.GetMouseButton(0))
        //{
        //use keyboard wsad 
        //float factorX = Input.GetAxis("Mouse X") * 2;
		if(leapIsEnabled == true)
		{
			factorX = pxsLeapInput.GetHandAxis("Rotation") * factorLeapX;
			factorY2 = -pxsLeapInput.GetHandAxis("Tilt") * factorLeapY;
		}
		else
		{
        	factorX = Input.GetAxis("Horizontal");

        //float factorY = Input.GetAxis("Mouse Y") * 2;
       		factorY2 = Input.GetAxis("Vertical");
		}

        //plane turn left or right
        transform.Rotate(0, Time.deltaTime * 100 * RightLeftSoft, 0, Space.World);
        //plane self rotation
        transform.Rotate(0, 0, (float)(Time.deltaTime * 100 * (1.0 - RightLeftSoftAbs - diveblocker) * factorX * -1.0));

        if ((factorX <= 0)
            && (rotationZ > 0)
            && (rotationZ < 90))
        {
            RightLeftSoft = rotationZ * 2.2f / 100 * -1;//to the left
        }
        else if ((factorX >= 0)
            && (rotationZ > 270))
        {
            RightLeftSoft = (7.92f - rotationZ * 2.2f / 100);//to the right
        }

        if (RightLeftSoft > 1)
        {
            RightLeftSoft = 1;
        }
        else if (RightLeftSoft < -1)
        {
            RightLeftSoft = -1;
        }
        if ((RightLeftSoft > -0.01) && (RightLeftSoft < 0.01))
        {
            RightLeftSoft = 0;
        }
        //get the abs value
        RightLeftSoftAbs = Mathf.Abs(RightLeftSoft);

        if (rotationX < 90)
        {
            divesalto = rotationX / 100.0f;//Updown
        }
        else if (rotationX > 90)
        {
            divesalto = -0.2f;//Updown
        }

        if (rotationX < 90)
        {
            diveblocker = rotationX / 200.0f;
        }
        else
        {
            diveblocker = 0;
        }

        if ((rotationZ < 180) && (factorX > 0))
        {
            transform.Rotate(0, 0, RightLeftSoft * Time.deltaTime * 80);
        }
        else if ((rotationZ > 180) && (factorX < 0))
        {
            transform.Rotate(0, 0, RightLeftSoft * Time.deltaTime * 80);
        }
        //restore
        if (!Input.GetButton("Horizontal"))
        {
            if ((rotationZ < 135))
            {
                transform.Rotate(0, 0, RightLeftSoftAbs * Time.deltaTime * -100);
            }
            if ((rotationZ > 225))
            {
                transform.Rotate(0, 0, RightLeftSoftAbs * Time.deltaTime * 100);
            }
        }

        if ((!Input.GetButton("Vertical")))// && (groundtrigger.triggered == 0))
        {
            if ((rotationX > 5) && (rotationX < 180))
            {
                transform.Rotate(Time.deltaTime * -50, 0, 0);
            }
            if ((rotationX > 180) && (rotationX < 355))
            {
                transform.Rotate(Time.deltaTime * 50, 0, 0);
            }
        }
		//Debug.Log(factorX);
        //}
        //else
        //{

        //    //restore
        //    //if (!Input.GetButton("Horizontal"))
        //    //{
        //    if ((rotationZ < 135))
        //    {
        //        transform.Rotate(0, 0, RightLeftSoftAbs * Time.deltaTime * -100);
        //    }
        //    if ((rotationZ > 225))
        //    {
        //        transform.Rotate(0, 0, RightLeftSoftAbs * Time.deltaTime * 100);
        //    }
        //    //}

        //    //if ((!Input.GetButton("Vertical")))// && (groundtrigger.triggered == 0))
        //    //{
        //    if ((rotationX > 0) && (rotationX < 180))
        //    {
        //        transform.Rotate(Time.deltaTime * -50, 0, 0);
        //    }
        //    if ((rotationX > 0) && (rotationX > 180))
        //    {
        //        transform.Rotate(Time.deltaTime * 50, 0, 0);
        //    }
        //    // }
        //}
    }
	
	void switchControl()
	{
		if(Input.GetButton("Fire1"))
		{
			leapIsEnabled = !leapIsEnabled;
		}
	}
	
	void adjustLeapControl()
	{
		if(leapIsEnabled)
		{
			if(Input.GetButton("Fire2"))
			{
				tempFactorX--;
				tempFactorY--;
			}
			if(Input.GetButton("Fire3"))
			{
				tempFactorX++;
				tempFactorY++;
			}
			factorLeapX = (float)tempFactorX/100;
			factorLeapY = (float)tempFactorY/100;
			
			if(factorLeapX > 1.0f) factorLeapX = 1.0f;
			else if(factorLeapX < 0.1f) factorLeapX = 0.1f;
			
			if(factorLeapY > 2.0f) factorLeapY = 2.0f;
			else if(factorLeapY < 0.5f) factorLeapY = 0.5f;
		}
	}

    public void AddScore()
    {
		audio.PlayOneShot(coinSound);
        myScore += scoreRate;
        Debug.Log("得分: " + myScore);
    }


    public int getIntScore()
    {
        return myScore;
    }

}
