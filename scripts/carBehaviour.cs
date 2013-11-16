using UnityEngine;
using System.Collections;

public class carBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(collider.Raycast(ray,out hit,200.0f))
			{
				Debug.Log("Paint");
				renderer.material.SetColor("_Color",Color.blue);
			}
			else
			{
				Debug.Log("Click miss");
			}
		}
	}
}
