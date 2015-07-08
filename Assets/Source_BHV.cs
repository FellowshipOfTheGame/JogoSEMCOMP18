using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Source_BHV : MonoBehaviour {

	public GameObject God;
	public GameObject Spark;
	public List <GameObject> NextNodes;
	public bool [] EmissionPattern;
	public int PatternIterator = 0;
	public GameObject [] ConnectionArt;
	public bool DragingWire = false;
	public bool DragingDestroy = false;
	public GameObject CenterArt;
	public AnimationCurve BrightnessOverTime;
	public Material DrawMaterial;
	public Material DestroyMaterial;

	private void RefreshArt (){

		CenterArt.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", BrightnessOverTime.Evaluate (God.GetComponent <God_BHV> ().PathFactor));

		for (int i = 0 ; i < ConnectionArt.Length ; i++){
			
			if (i < NextNodes.Count){
				
				ConnectionArt [i].GetComponent <LineRenderer> ().enabled = true;
				ConnectionArt [i].GetComponent <LineRenderer> ().SetPosition (0,transform.position);
				ConnectionArt [i].GetComponent <LineRenderer> ().SetPosition (1,NextNodes [i].transform.position);
			}
			else {
				
				ConnectionArt [i].GetComponent <LineRenderer> ().enabled = false;
				
			}
			
		}

		if (DragingDestroy){
			
			GetComponent <LineRenderer> ().material = DestroyMaterial;
			
		}
		else{
			
			GetComponent <LineRenderer> ().material = DrawMaterial;
			
		}

		if (DragingWire || DragingDestroy){
			
			Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit Hit;
			
			if (Physics.Raycast(ClickRay, out Hit, 1000.0f)){
				
				GetComponent <LineRenderer> ().SetPosition (0,transform.position);
				GetComponent <LineRenderer> ().SetPosition (1,new Vector3 (Hit.point.x, Hit.point.y, 0.0f));
				GetComponent <LineRenderer> ().enabled = true;
				
			}
			
		}
		else{
			
			GetComponent <LineRenderer> ().enabled = false;
			
		}
		
	}

	// Update is called once per frame
	void Update () {
	
		if (God.GetComponent <God_BHV> ().PathFactor >= 1.0f && EmissionPattern.Length > 0){

			PatternIterator++;

			if (PatternIterator >= EmissionPattern.Length){

				PatternIterator = 0;

			}

			if (EmissionPattern [PatternIterator]){

				for (int i = 0 ; i < NextNodes.Count ; i++){

					GameObject NewSpark = (GameObject)Instantiate (Spark, transform.position, Quaternion.identity);
					NewSpark.GetComponent <GaloLerp> ().SetPath (gameObject, NextNodes [i]);
					NewSpark.GetComponent <GaloLerp> ().God = God;

				}

			}

		}

		RefreshArt ();
		//CreatePath
		if (Input.GetMouseButtonDown(0)){
			
			Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit Hit;
			
			if (Physics.Raycast(ClickRay, out Hit, 1000.0f)){
				
				if (Hit.collider.gameObject == gameObject && !DragingDestroy){
					
					DragingWire = true;
					
				}
				
			}
			else {
				
				DragingWire = false;
				
			}
			
		}
		
		if (Input.GetMouseButtonUp(0)){
			
			Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit Hit;
			
			if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && DragingWire){
				
				if ((Hit.collider.gameObject.tag == "Node" || Hit.collider.gameObject.tag == "GaloBrilha") && Hit.collider.gameObject != gameObject && (Hit.collider.gameObject.transform.position-transform.position).magnitude <= 2.0f){
					
					NextNodes.Add (Hit.collider.gameObject);
					
				}
				
			}
			
			DragingWire = false;
			
		}

		//DestroyPath
		if (Input.GetMouseButtonDown(1)){
			
			Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit Hit;
			
			if (Physics.Raycast(ClickRay, out Hit, 1000.0f)){
				
				if (Hit.collider.gameObject == gameObject && !DragingWire){
					
					DragingDestroy = true;
					
				}
				
			}
			else {
				
				DragingDestroy = false;
				
			}
			
		}
		
		if (Input.GetMouseButtonUp(1)){
			
			Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit Hit;
			
			if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && DragingDestroy){
				
				if ((Hit.collider.gameObject.tag == "Node" || Hit.collider.gameObject.tag == "GaloBrilha") && Hit.collider.gameObject != gameObject && (Hit.collider.gameObject.transform.position-transform.position).magnitude <= 2.0f){
					
					NextNodes.Remove (Hit.collider.gameObject);
					if (Hit.collider.gameObject.tag == "Node"){
						Hit.collider.gameObject.GetComponent <Node_BHV> ().NextNodes.Remove (gameObject);
					}
					if (Hit.collider.gameObject.tag == "Source"){
						Hit.collider.gameObject.GetComponent <Source_BHV> ().NextNodes.Remove (gameObject);
					}

				}
				
			}
			
			DragingDestroy = false;
			
		}

	}
}


