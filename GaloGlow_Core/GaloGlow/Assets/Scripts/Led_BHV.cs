using UnityEngine;
using System.Collections;

public class Led_BHV : MonoBehaviour {

	public GameObject God;
	public GameObject Node;
	public AudioClip GlowSound;
	public AudioClip BrokenSound;
	public GameObject SoundEffect;

	public int [] ColorPattern;
	public int [] NotePattern;
	private int PatternIterator = 0;

	public GameObject LedCenter;
	public GameObject LedRing;
	public Material TurnOffMaterial;

	public AnimationCurve CenterBrightness;
	public AnimationCurve RingBrightness;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//On pulse end.
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(1) && ColorPattern.Length > 0){
			
			PatternIterator++;
			
			if (PatternIterator >= ColorPattern.Length){
				
				PatternIterator = 0;
				
			}

			int SparkType = Node.GetComponent <Node_BHV> ().GetColor ();

			if (SparkType > -1){

				GameObject newSound =  (GameObject)Instantiate (SoundEffect, transform.position, Quaternion.identity);

				if (ColorPattern [PatternIterator] == SparkType){
				
					//GetComponent <AudioSource> ().pitch = Mathf.Pow (1.05945454f, (float)NotePattern[PatternIterator]);
					//GetComponent <AudioSource> ().PlayOneShot (GlowSound);
					newSound.GetComponent <SoundEffect_BHV> ().SoundToPlay = GlowSound;
					newSound.GetComponent <SoundEffect_BHV> ().Pitch = NotePattern[PatternIterator];
				
				}
				else{

					//GetComponent <AudioSource> ().pitch = Mathf.Pow (1.05945454f, (float)SparkType);
					//GetComponent <AudioSource> ().PlayOneShot (BrokenSound);
					newSound.GetComponent <SoundEffect_BHV> ().SoundToPlay = BrokenSound;
					newSound.GetComponent <SoundEffect_BHV> ().Pitch = Random.Range (-3, 3);

				}

			}
			if (ColorPattern [PatternIterator] == SparkType){

				God.GetComponent <God_BHV> ().IncrementCompletionScore();

			}

		}

		//On pulse middle.
		if (God.GetComponent <God_BHV> ().GetSemipathTrigger(0) && ColorPattern.Length > 0){

			int PresentColor = Node.GetComponent <Node_BHV> ().GetColor();

			if (PresentColor > -1){

				LedCenter.GetComponent <MeshRenderer> ().material = God.GetComponent <God_BHV> ().ColorMaterials [PresentColor];

			}

			else {

				LedCenter.GetComponent <MeshRenderer> ().material = TurnOffMaterial;

			}

			LedRing.GetComponent <MeshRenderer> ().material = God.GetComponent <God_BHV> ().GetColorMaterial (ColorPattern[(PatternIterator+1)%ColorPattern.Length]);
		}

		LedCenter.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", CenterBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
		LedRing.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", RingBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));

	}

}
