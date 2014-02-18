using UnityEngine;
using System.Collections;

public class CreatePlane : MonoBehaviour {

    private string dataForPlane = "4szi";
	private string sPlaneID;
	private string sColorID;

    private int planeId=3;
    private string materialName;
    public GameObject startPositon;
    public GameObject planePrefab1;
    public GameObject planePrefab2;
    public GameObject planePrefab3;
    public GameObject planePrefab4;
    public GameObject planePrefab5;
    public GameObject planePrefab6;
    public GameObject planePrefab7;
    public GameObject planePrefab8;
    public GameObject planePrefab9;
    public GameObject planePrefab10;
    public GameObject planePrefab11;
    public GameObject planePrefab12;
    public GameObject planePrefab13;
    public GameObject planePrefab14;
    public GameObject planePrefab15;
    public GameObject planePrefab16;
    public GameObject planePrefab17;
    public GameObject planePrefab18;
    public GameObject planePrefab19;
    public GameObject planePrefab20;
    public GameObject planePrefab21;
    public GameObject planePrefab22;
    public GameObject planePrefab23;
    public GameObject planePrefab24;

    private ArrayList planePrefabArray;

    //handle various materials
    private Material[] changePartArray = new Material[10];
    private Material changePartMat;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	    //get id&material
        GetPrefabArray();

        GetData();

        ChoosePlane();
	}
    private void GetPrefabArray()
    {
        planePrefabArray = new ArrayList(24);

        planePrefabArray.Add(planePrefab1);
        planePrefabArray.Add(planePrefab2);
        planePrefabArray.Add(planePrefab3);
        planePrefabArray.Add(planePrefab4);
        planePrefabArray.Add(planePrefab5);
        planePrefabArray.Add(planePrefab6);
        planePrefabArray.Add(planePrefab7);
        planePrefabArray.Add(planePrefab8); 
        planePrefabArray.Add(planePrefab9); 
        planePrefabArray.Add(planePrefab10); 
        planePrefabArray.Add(planePrefab11);
        planePrefabArray.Add(planePrefab12); 
        planePrefabArray.Add(planePrefab13);
        planePrefabArray.Add(planePrefab14);
        planePrefabArray.Add(planePrefab15);
        planePrefabArray.Add(planePrefab16);
        planePrefabArray.Add(planePrefab17);
        planePrefabArray.Add(planePrefab18);
        planePrefabArray.Add(planePrefab19);
        planePrefabArray.Add(planePrefab20);
        planePrefabArray.Add(planePrefab21);
        planePrefabArray.Add(planePrefab22);
        planePrefabArray.Add(planePrefab23);
        planePrefabArray.Add(planePrefab24); 
    }
    private void GetData()
    {

        dataForPlane = PlayerPrefs.GetString("planeProperty");
		sPlaneID = PlayerPrefs.GetString("planeID","3");
		sColorID = PlayerPrefs.GetString("colorID","zi");
        //分割
        string[] data = dataForPlane.Split('s');
        planeId = System.Int32.Parse(sPlaneID);
        materialName = sColorID;
        //Debug.Log(data[0]+"+"+data[1]);
    }

    void ChoosePlane()
    {
        GameObject temp;
        Texture tempTexture;

        //get prefab and material by planeId
        temp = (GameObject)Instantiate((GameObject)planePrefabArray[planeId-1], startPositon.transform.position, startPositon.transform.rotation);
        temp.name = "Player";
        if (planeId <= 9)
        {
            tempTexture = (Texture)Resources.Load("Planes/" + "Plane00" + planeId + "/" + "feiJi00" + planeId + "_clr_" + materialName, typeof(Texture));
            findChangeParts(planeId);
            ChangeMeaterial(temp, tempTexture);
        }
        else if (planeId >=10)
        {
            tempTexture = (Texture)Resources.Load("Planes/" + "Plane0" + planeId + "/" + "feiJi0" + planeId + "_clr_" + materialName, typeof(Texture));
            findChangeParts(planeId);
            ChangeMeaterial(temp, tempTexture);
        }
        
    }
    void ChangeMeaterial(GameObject temp,Texture tempTexture)
    {

        //change material
        Renderer[] rendererComponents = temp.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer tempRenderer in rendererComponents)
        {
            tempRenderer.material.mainTexture = tempTexture;
        }
        changePartMat.mainTexture = tempTexture;
    }
    void findChangeParts(int id)
    {
        if(id<10)
            changePartArray = GameObject.Find("feiJi00" + planeId.ToString() + "_zhuTi").renderer.materials;
        else
            changePartArray = GameObject.Find("feiJi0" + planeId.ToString() + "_zhuTi").renderer.materials;
        switch (id)
        {
            case 5:
                changePartMat = changePartArray[2];
                break;
            case 8:
                changePartMat = changePartArray[1];
                break;
            case 16:
                changePartMat = changePartArray[1];
                break;
            case 20:
                changePartMat = changePartArray[1];
                break;
            case 23:
                changePartMat = changePartArray[1];
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
	
	}

    //public interface
    public int getPlaneID()
    {
        return planeId;
    }
}
