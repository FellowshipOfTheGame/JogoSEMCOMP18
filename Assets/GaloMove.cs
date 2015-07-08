using UnityEngine;
using System.Collections;

public class GaloMove : MonoBehaviour {

	public float V;
	// Use this for initialization
	void Start () {
	
		V = Random.Range(-1.5f, 1.5f);

	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = transform.position + new Vector3 (1.0f, 0.0f, 0.0f)*V*Time.deltaTime;
		if (transform.position.x > 10.0f){

			transform.position = new Vector3 (-9.99f, 0.0f, 0.0f);

		}
		if (transform.position.x < -10.0f){
			
			transform.position = new Vector3 (9.99f, 0.0f, 0.0f);
			
		}

	}
}
