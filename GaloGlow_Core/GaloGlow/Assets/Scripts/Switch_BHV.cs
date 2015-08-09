using UnityEngine;
using System.Collections;

public class Switch_BHV : MonoBehaviour {

	public GameObject God;
	public GameObject Node;
	public GameObject SwitchCenter;
	public Mesh ConnectedMesh;
	public Mesh DisconnectedMesh;
	public Material TurnOffMaterial;
	
	public GameObject [] SwitchPattern;
	private int PatternIterator = 0;
	public AnimationCurve CenterBrightness;
	
	private void Switch (){
		
		PatternIterator++;
		
		if (PatternIterator >= SwitchPattern.Length){
			
			PatternIterator = 0;
			
		}

		Node.GetComponent <Node_BHV> ().DisconnectFromAll ();
		if (SwitchPattern [PatternIterator] != null){

			Node.GetComponent <Node_BHV> ().ConnectTo (SwitchPattern [PatternIterator]);
			if (SwitchPattern [PatternIterator].GetComponent <Node_BHV> ().IsConnectedTo (Node)){

				SwitchPattern [PatternIterator].GetComponent <Node_BHV> ().DisconnectFrom (Node);
				//God.GetComponent <God_BHV> ().ResetCompletionScore ();

			}

		}
		else {

			//Node.GetComponent <Node_BHV> ().DisconnectFromAll ();

		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(1) && SwitchPattern.Length > 0){
			
			Switch ();


			if (SwitchPattern [PatternIterator] != null){

				SwitchCenter.GetComponent <MeshFilter> ().mesh = ConnectedMesh;
				SwitchCenter.transform.rotation = Quaternion.LookRotation (SwitchPattern [PatternIterator].transform.position - transform.position, Vector3.up);

			}
			else {

				SwitchCenter.GetComponent <MeshFilter> ().mesh = DisconnectedMesh;

			}


		}
		
		//if (God.GetComponent <God_BHV> ().GetSemipathTrigger(0) && SwitchPattern.Length > 0){
			
		//	SwitchCenter.GetComponent <MeshRenderer> ().material = God.GetComponent <God_BHV> ().GetColorMaterial (SwitchPattern [(PatternIterator+1)%SwitchPattern.Length]);
		//}
		
		//SwitchCenter.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", CenterBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
		
	}

}
