using UnityEngine;
using System.Collections;

public class SceneChangeButton_BHV : MonoBehaviour {

	public string LevelName;
	public bool IsActive = true;
	public GameObject LedArt;
	public GameObject TextArt;
	public GameObject LevelSelectGod;
	public GameObject SoundEffect;
	public AudioClip HoverSound;
	public AudioClip ClickSound;
	public bool UseGodPitch = true;
	public int Pitch = 0;
	public int Chord = 0;

	public float SpringConstant = 1.0f;
	public float Dampening = 0.5f;
	public float MaxGlow = 0.5f;
	public float MinGlow = 0.05f;

	public float BaseFrequency = 0.5f;
	public float BaseAmplitude = 0.05f;
	public float BaseOffset = 0.1f;
	private float BasePhase;
	private float time = 0.0f;

	private bool MousePressed = false;
	private bool MouseOver = false;
	
	private float Glow = 0.0f;
	private float GlowEquilibrium = 0.0f;
	private float GlowSpeed = 0.0f;
	private float GlowAcceleration = 0.0f;

	public void ActivateButton(){

		IsActive = true;
		GlowEquilibrium = MinGlow;

		Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit Hit;

		if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && Hit.collider.gameObject == gameObject){

			MouseOver = true;
			GlowEquilibrium = MaxGlow;
			
			GameObject newSound =  (GameObject)Instantiate (SoundEffect, transform.position, Quaternion.identity);
			newSound.GetComponent <SoundEffect_BHV> ().SoundToPlay = HoverSound;
			
			if (UseGodPitch){
				
				newSound.GetComponent <SoundEffect_BHV> ().Pitch = LevelSelectGod.GetComponent <God_BHV> ().GetHoverPitch (Chord);
				
			}
			else {
				
				newSound.GetComponent <SoundEffect_BHV> ().Pitch = Pitch;
			}

		}

	}

	// Use this for initialization
	void Start () {

		GlowEquilibrium = MinGlow;
		Glow = MinGlow;
		BasePhase = Random.Range (0.0f, 1.0f/BaseFrequency);
		BaseFrequency = BaseFrequency+Random.Range (-0.5f ,0.5f);

	}
	
	// Update is called once per frame
	void Update () {

		if (IsActive){

			if (Input.GetMouseButtonDown (0)&& MouseOver){

				MousePressed = true;

				GameObject newSound =  (GameObject)Instantiate (SoundEffect, transform.position, Quaternion.identity);
				newSound.GetComponent <SoundEffect_BHV> ().SoundToPlay = ClickSound;
				GlowSpeed = 3.0f;

				if (UseGodPitch){
				
					newSound.GetComponent <SoundEffect_BHV> ().Pitch = LevelSelectGod.GetComponent <God_BHV> ().GetHoverPitch (Chord);
				
				}
				else {
				
					newSound.GetComponent <SoundEffect_BHV> ().Pitch = Pitch;
				}
			
			}

			if (Input.GetMouseButtonUp (0)){

				if (MouseOver && MousePressed){

					if (LevelName != ""){

						LevelSelectGod.GetComponent <God_BHV> ().JumpToScene(LevelName);
				
					}

				}

				MousePressed = false;

			}

		}
		else {

			GlowEquilibrium = 0.0f;
			MousePressed = false;

		}
		time = (time+Time.deltaTime)%(1.0f/BaseFrequency);
		GlowAcceleration = -SpringConstant*(Glow-GlowEquilibrium) - GlowSpeed*Dampening;
		GlowSpeed = GlowSpeed+GlowAcceleration*Time.deltaTime;
		Glow = Glow+GlowSpeed*Time.deltaTime;

		LedArt.GetComponent <MeshRenderer> ().materials [0].SetFloat("_Brightness", Glow+BaseAmplitude*Mathf.Cos (2.0f*Mathf.PI*BaseFrequency*time+2.0f*Mathf.PI*BasePhase)+BaseOffset); //Mathf.Max (0.0f, Glow));

	}

	void OnMouseEnter () {

		if (IsActive){
			MouseOver = true;
			GlowEquilibrium = MaxGlow;

			GameObject newSound =  (GameObject)Instantiate (SoundEffect, transform.position, Quaternion.identity);
			newSound.GetComponent <SoundEffect_BHV> ().SoundToPlay = HoverSound;
		
			if (UseGodPitch){
			
				newSound.GetComponent <SoundEffect_BHV> ().Pitch = LevelSelectGod.GetComponent <God_BHV> ().GetHoverPitch (Chord);
			
			}
			else {
			
				newSound.GetComponent <SoundEffect_BHV> ().Pitch = Pitch;
			}

		}

	}

	void OnMouseExit () {

		if (IsActive){

			MouseOver = false;
			GlowEquilibrium = MinGlow;

		}

	}

}
