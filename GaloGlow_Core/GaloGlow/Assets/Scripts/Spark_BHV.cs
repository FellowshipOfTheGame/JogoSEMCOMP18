using UnityEngine;
using System.Collections;

public class Spark_BHV : MonoBehaviour {

	public GameObject God;
	public int SparkColor = 0;
	private GameObject FirstPosition;
	private GameObject LastPosition;
	public AnimationCurve BrightnessOverTime;

	private bool jarodei = false;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(1)){
			
			transform.position = Vector3.Lerp (FirstPosition.transform.position, LastPosition.transform.position, 1.0f);
			Destroy(gameObject);
			
		}
		else{

			if (!jarodei) {

				LastPosition.GetComponent <Node_BHV> ().AddColor (SparkColor);
				jarodei = true;

			}

			GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", BrightnessOverTime.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
			transform.position = Vector3.Lerp (FirstPosition.transform.position, LastPosition.transform.position, God.GetComponent <God_BHV> ().PathFactor)+new Vector3 (0.0f, 0.5f, 0.0f);


			if (!FirstPosition.GetComponent <Node_BHV> ().IsConnectedTo (LastPosition)){

				if (jarodei){

					LastPosition.GetComponent <Node_BHV> ().RemoveColor (SparkColor);
				
				}
				Destroy(gameObject);
			
			}
			
		}

	}

	public void SetPath (GameObject Start, GameObject End){

		FirstPosition = Start;
		LastPosition = End;
	
	}


}
