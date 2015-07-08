using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node_BHV : MonoBehaviour {

	public GameObject God;
	public List <GameObject> NextNodes;
	public List <int> ArrivingSparks;
	public GameObject [] SparkTypes; 
	public GameObject [] ConnectionArt;
	public bool DragingWire = false;
	public bool DragingDestroy = false;
	public GameObject CenterArt;
	public Material DrawMaterial;
	public Material DestroyMaterial;

	private int BlendSpark (){

		int DefaultColor = 0;
		bool HasRed = ArrivingSparks.Contains (0);
		bool HasGreen = ArrivingSparks.Contains (1);
		bool HasBlue = ArrivingSparks.Contains (2);
		bool HasCyan = ArrivingSparks.Contains (3);
		bool HasYellow = ArrivingSparks.Contains (4);
		bool HasMagenta = ArrivingSparks.Contains (5);
		bool HasWhite = ArrivingSparks.Contains (6);

		if (HasWhite){

			DefaultColor = 6;

		}
		else if (HasYellow){

			if (HasBlue || HasMagenta || HasCyan){DefaultColor = 6;}
			else                                 {DefaultColor = 4;}

		}
		else if (HasMagenta){
			
			if (HasGreen || HasCyan){DefaultColor = 6;}
			else                    {DefaultColor = 5;}
			
		}
		else if (HasCyan){
			
			if (HasRed){DefaultColor = 6;}
			else       {DefaultColor = 3;}
			
		}

		//Only primaries
		else if ( HasRed &&  HasGreen &&  HasBlue){ DefaultColor = 6;}
		else if (!HasRed &&  HasGreen &&  HasBlue){ DefaultColor = 3;}
		else if ( HasRed && !HasGreen &&  HasBlue){ DefaultColor = 5;}
		else if ( HasRed &&  HasGreen && !HasBlue){ DefaultColor = 4;}
		else if ( HasRed && !HasGreen && !HasBlue){ DefaultColor = 0;}
		else if (!HasRed &&  HasGreen && !HasBlue){ DefaultColor = 1;}
		else if (!HasRed && !HasGreen &&  HasBlue){ DefaultColor = 2;}

		return DefaultColor;

	}

	private void RefreshArt (){

		if (NextNodes.Count > 0){
			
			CenterArt.GetComponent <MeshRenderer> ().enabled = true;
			
		}
		else {
			
			CenterArt.GetComponent <MeshRenderer> ().enabled = false;
			
		}

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

		if (God.GetComponent <God_BHV> ().PathFactor >= 1.0f && ArrivingSparks.Count != 0){

			int SparkType = BlendSpark ();

			for (int i = 0 ; i < NextNodes.Count ; i++){
				
				GameObject NewSpark = (GameObject)Instantiate (SparkTypes [SparkType] , transform.position, Quaternion.identity);
				NewSpark.GetComponent <GaloLerp> ().SetPath (gameObject, NextNodes [i]);
				NewSpark.GetComponent <GaloLerp> ().God = God;
				
			}

			ArrivingSparks.Clear ();
			
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
