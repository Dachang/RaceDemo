using UnityEngine;
using System.Collections;

public class AirscrewBehaviour : MonoBehaviour
{
    public GameObject airscrew;
    private GameObject airscrew_two;
    private GameObject airscrew_three;
    private GameObject airscrew_four;
    private float speed = 700;

    private CreatePlane cpScript;
    private int planeID;
	// Use this for initialization
	void Start () 
    {
        cpScript = (CreatePlane)Camera.main.GetComponent(typeof(CreatePlane));
        planeID = cpScript.getPlaneID();
        switchAirscrew(planeID);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(planeID);
        if (planeID == 3 || planeID == 4)
        {
            airscrew.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else if (planeID == 5 || planeID == 6 || planeID == 7)
        {
            airscrew.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            airscrew_two.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (planeID == 8 || planeID == 10 || planeID == 11 || planeID == 18 || planeID == 19 || planeID == 20 || planeID == 21)
        {
            airscrew.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (planeID == 13 || planeID == 23)
        {
            airscrew.transform.Rotate(Vector3.up * speed * Time.deltaTime);
            airscrew_two.transform.Rotate(Vector3.left * speed * Time.deltaTime);
        }
        else if (planeID == 15)
        {
            airscrew.transform.Rotate(Vector3.up * speed * Time.deltaTime);
            airscrew_two.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else if (planeID == 16)
        {
            airscrew.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            airscrew_two.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            airscrew_three.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            airscrew_four.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
	}

    void switchAirscrew(int id)
    {
        switch (id)
        {
            case 5:
                speed = 1000;
                airscrew_two = GameObject.Find("feiJi005_LXJ02");
                break;
            case 6:
                speed = 1000;
                airscrew_two = GameObject.Find("feiJi006_LXJ02");
                break;
            case 7:
                speed = 1000;
                airscrew_two = GameObject.Find("feiJi007_LXJ02");
                break;
            case 8:
                speed = 1000;
                break;
            case 10:
                speed = 1000;
                break;
            case 11:
                speed = 1000;
                break;
            case 13:
                speed = 700;
                airscrew_two = GameObject.Find("feiJi013_LXJ02");
                break;
            case 15:
                airscrew_two = GameObject.Find("feiJi015_LXJ02");
                break;
            case 16:
                speed = 1000;
                airscrew_two = GameObject.Find("feiJi016_LXJ02");
                airscrew_three = GameObject.Find("feiJi016_LXJ03");
                airscrew_four = GameObject.Find("feiJi016_LXJ04");
                break; 
            case 18:
                speed = 1000;
                break;
            case 19:
                speed = 1000;
                break;
            case 20:
                speed = 2000;
                break;
            case 21:
                speed = 1000;
                break;
            case 23:
                airscrew_two = GameObject.Find("feiJi023_LXJ02");
                break;
            default:
                break;
        }
    }
}
