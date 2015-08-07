using UnityEngine;
using System.Collections;

public class Source_BHV: MonoBehaviour {

	public GameObject God;
	public GameObject Node;
	public GameObject LedCenter;
	public Material TurnOffMaterial;

	public int [] EmissionPattern;
	//public Material [] MaterialPattern;
	private int PatternIterator = 0;
	public AnimationCurve CenterBrightness;

	private void Emit (){

		PatternIterator++;
		
		if (PatternIterator >= EmissionPattern.Length){
			
			PatternIterator = 0;
			
		}
		
		if (EmissionPattern [PatternIterator] > -1 && EmissionPattern [PatternIterator] <= 6){

			Node.GetComponent <Node_BHV> ().AddColor (EmissionPattern [PatternIterator]);
			
		}

	}

	// Update is called once per frame
	void Update () {
	
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(1) && EmissionPattern.Length > 0){

			Emit ();
			
		}

		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(0) && EmissionPattern.Length > 0){
			
			LedCenter.GetComponent <MeshRenderer> ().material = God.GetComponent <God_BHV> ().GetColorMaterial (EmissionPattern [(PatternIterator+1)%EmissionPattern.Length]);
		}
		
		LedCenter.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", CenterBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));

	}
}
