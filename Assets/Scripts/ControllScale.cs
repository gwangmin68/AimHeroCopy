using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllScale : MonoBehaviour {

	public float scaleRange;
	public float scaleTime = 2.0f;
	public float scaleMax = 1.5f;
	public float scaleMin = 0;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0, 1, 0);
		scaleRange = scaleMax / scaleTime * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		Scaling ();
	}

	void Scaling(){
		transform.localScale += new Vector3 (scaleRange, 0, scaleRange);
		Vector3 currentScale = transform.localScale;
		if (currentScale.x >= scaleMax) {
			scaleRange = -scaleRange;
		}
		if (currentScale.x <= scaleMin)
			Destroy (this.gameObject);
	}
}
