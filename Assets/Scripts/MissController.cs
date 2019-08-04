using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissController : MonoBehaviour {

	public float destroyTime;
	// Use this for initialization
	void Start () {
		destroyTime = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		if(destroyTime >= 0f)
		{
			destroyTime -= Time.deltaTime;
		}else if(destroyTime <= 0f)
		{
			Destroy (this.gameObject);
		}
	}
}
