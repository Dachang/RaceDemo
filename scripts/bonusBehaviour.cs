using UnityEngine;
using System.Collections;

public class bonusBehaviour : MonoBehaviour {
	
	private scoreManager sMgr;
	private bool hasTriggered = false;
	// Use this for initialization
	void Start () 
	{
		sMgr = (scoreManager)Camera.main.GetComponent(typeof(scoreManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(new Vector3(1.5f, 0.0f, 0.0f));
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(!hasTriggered)
		{
			Destroy(gameObject);
			sMgr.addScore();
			hasTriggered = true;
		}
	}
}
