using UnityEngine;
using System.Collections;

public class Led_BHV : MonoBehaviour {

	public GameObject God;
	public GameObject Node;
	public AudioClip GlowSound;

	public int [] ColorPattern;
	public int [] NotePattern;
	public Material [] MaterialPattern;
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

			if (ColorPattern [PatternIterator] == SparkType && SparkType > -1){

				GetComponent <AudioSource> ().pitch = Mathf.Pow (1.05945454f, (float)NotePattern[PatternIterator]);
				GetComponent <AudioSource> ().PlayOneShot (GlowSound);
				
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

			LedRing.GetComponent <MeshRenderer> ().material = MaterialPattern [(PatternIterator+1)%MaterialPattern.Length];
		}

		LedCenter.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", CenterBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));
		LedRing.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", RingBrightness.Evaluate (God.GetComponent <God_BHV> ().PathFactor));

	}

}
