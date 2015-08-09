using UnityEngine;
using System.Collections;

public class Filter_BHV : MonoBehaviour {

	public GameObject God;
	public GameObject Node;
	public GameObject FilterCenter;
	public Material TurnOffMaterial;
	
	public int [] FilterPattern;
	//public Material [] MaterialPattern;
	private int PatternIterator = 0;
	public AnimationCurve CenterBrightness;
	
	private void Filter (){
		
		PatternIterator++;
		
		if (PatternIterator >= FilterPattern.Length){
			
			PatternIterator = 0;
			
		}
		
		if (FilterPattern [PatternIterator] > -1 && FilterPattern [PatternIterator] <= 6){
			
			Node.GetComponent <Node_BHV> ().RemoveColor (FilterPattern [PatternIterator]);
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(1) && FilterPattern.Length > 0){
			
			Filter ();
			
		}
		
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(0) && FilterPattern.Length > 0){
			
			FilterCenter.GetComponent <MeshRenderer> ().material = God.GetComponent <God_BHV> ().GetColorMaterial (FilterPattern [(PatternIterator+1)%FilterPattern.Length]);
		}
		
		FilterCenter.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", CenterBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
		
	}

}
