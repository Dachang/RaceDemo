using UnityEngine;
using System.Collections;

public class MapBehaviour : MonoBehaviour {

    private int dragSpeedX = -50;
    private bool mapIsAbleToDrag = true;

	void Start () 
    {

	}
	
	void Update () 
    {
        
	}

    void OnMouseDrag()
    {
        if (mapIsAbleToDrag)
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse X") * dragSpeedX);
        }
    }

    public void setMapIsAbleToDrag(bool isAbleOrNot)
    {
        mapIsAbleToDrag = isAbleOrNot;
    }
}