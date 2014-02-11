using UnityEngine;
using System.Collections;

public class startTriggerBehaviour : MonoBehaviour {
	
	private scoreManager sMgr;
	
	// Use this for initialization
	void Start () 
	{
		sMgr = (scoreManager)Camera.main.GetComponent(typeof(scoreManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerExit(Collider other)
	{
		sMgr.addRoundNum();
	}
}
