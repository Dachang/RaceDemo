using UnityEngine;
using System.Collections;

public class bonusBehaviour : MonoBehaviour {
	
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
	
	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		sMgr.addScore();
	}
}
