using UnityEngine;
using System.Collections;

public class God_BHV : MonoBehaviour {
	
	public float Frequency = 1.0f;

	public float PathFactor = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (PathFactor >= 1.0f){
			
			PathFactor = 0.0f;

		}

		PathFactor = PathFactor + Time.deltaTime*Frequency;



	}
}
