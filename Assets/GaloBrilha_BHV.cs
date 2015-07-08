using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GaloBrilha_BHV : MonoBehaviour {

	public GameObject God;
	public List <int> ArrivingSparks;
	public GameObject [] SparkTypes; 
	public AudioClip SoundToPlay;
	public int [] ColorPattern;
	public Material [] MaterialByColor;
	public Material TurnOffMaterial;
	public GameObject CenterArt;
	public GameObject RingArt;
	public AnimationCurve BrightnessOverTime;
	public AnimationCurve CenterBrightnessOverTime;

	public int PatternIterator = 0;

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
	
	// Update is called once per frame
	void Update () {

		if (God.GetComponent <God_BHV> ().PathFactor >= 1.0f && ColorPattern.Length > 0){

			PatternIterator++;

			if (PatternIterator >= ColorPattern.Length){

				PatternIterator = 0;

			}

			RingArt.GetComponent <MeshRenderer> ().material = MaterialByColor [PatternIterator];
			CenterArt.GetComponent <MeshRenderer> ().material = TurnOffMaterial;

			if (ArrivingSparks.Count != 0){

				int SparkType = BlendSpark ();

				if (ColorPattern [PatternIterator] == SparkType){

					CenterArt.GetComponent <MeshRenderer> ().material = MaterialByColor [PatternIterator];
					GetComponent <AudioSource> ().PlayOneShot (SoundToPlay);
				}

				ArrivingSparks.Clear ();
			}
			
		}

		RingArt.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", BrightnessOverTime.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
		CenterArt.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", CenterBrightnessOverTime.Evaluate (God.GetComponent <God_BHV> ().PathFactor));

	}
}
