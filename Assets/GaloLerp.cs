using UnityEngine;
using System.Collections;

public class GaloLerp : MonoBehaviour {

	public GameObject God;
	public int SparkColor = 0;
	private GameObject FirstPosition;
	private GameObject LastPosition;
	public AnimationCurve BrightnessOverTime;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
		if (God.GetComponent <God_BHV> ().PathFactor >= 1.0f){
			
			transform.position = Vector3.Lerp (FirstPosition.transform.position, LastPosition.transform.position, 1.0f);

			if (LastPosition.tag == "Node"){

				LastPosition.GetComponent <Node_BHV> ().ArrivingSparks.Add (SparkColor);
			}
			if (LastPosition.tag == "GaloBrilha"){
				
				LastPosition.GetComponent <GaloBrilha_BHV> ().ArrivingSparks.Add (SparkColor);
			}

			Destroy(gameObject);
			
		}
		else{

			GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", BrightnessOverTime.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
			transform.position = Vector3.Lerp (FirstPosition.transform.position, LastPosition.transform.position, God.GetComponent <God_BHV> ().PathFactor);

			if (FirstPosition.tag == "Source"){
				if (!FirstPosition.GetComponent <Source_BHV> ().NextNodes.Contains (LastPosition)) { Destroy(gameObject);}
			}
			if (FirstPosition.tag == "Node"){
				if (!FirstPosition.GetComponent <Node_BHV> ().NextNodes.Contains (LastPosition)) { Destroy(gameObject);}
			}
			
		}


	}

	public void SetPath (GameObject Start, GameObject End){

		FirstPosition = Start;
		LastPosition = End;
	
	}


}
