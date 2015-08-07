using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class HexSnap : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Snap ();
	}

	void Snap(){
		int adj = 1000;
		float radius = (int)(adj *1);
		int x = (int)(adj*transform.localPosition.x);
		int z = (int)(adj*transform.localPosition.z);
		int w = (int)((radius * 3) / 4);
		int h = (int)((radius * 13) / 30);

		float newX = (float)( (Math.Round((double)x / (double)w) *w) / (double)adj );
		float newZ = (float)( (Math.Round((double)z / (double)h) *h) / (double)adj );

		transform.localPosition = new Vector3 (newX, transform.localPosition.y, newZ);
	}

}
