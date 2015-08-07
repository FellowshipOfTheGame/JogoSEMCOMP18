using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class God_BHV : MonoBehaviour {

	//////////////
	// Gameplay //
	//////////////

	//Values
	public bool HasReachedLevelCompletion = false;   //True if a cicle has been perfectly completed in this scene instance. 
	public int CicleDuration = 3;                    //The duration of a completion cicle in game pulses.
	public int CompletionScore = 1;                  //The score needed to complete the level.
	public int CurrentCompletion = 0;                //The current completion score of the level.
	private bool CanReset = false;                   //Whether the completion score can reset in late update.
	public int PulseCounter = 0;                     //Stores the amount of pulses played in the current cicle.
	public GameObject LevelConclusionButton;         //The button that gets activated when the level is compleated.

	//Methodes
	public bool HasAcheivedVictory (){

		return HasReachedLevelCompletion;

	}            //Returns weather a cull cicle has been perfectly completed in this scene instance. 
	public void ResetCompletionScore (){

		CurrentCompletion = 0;

	}          //Sets the level completio score to 0.
	public void IncrementCompletionScore (){

		CurrentCompletion++;

	}      //Increments the level completio score.

	//_________________________________________________________________________________________________________//

	//////////
	// Time //
	//////////

	//Values
	public float Frequency = 1.0f;                  //The frequency in Hz in which the game pulses;
	public float PathFactor = 0.0f;                 //Current normalized time of a fraction of a game pulse.
	//public float SemiPathFactor = 0.5f;
	public int SemipathCount = 2;                   //The amount of subdivisions of a game pulse.
	private float [] Semipath;                      //Current normalized time of a fraction of a game pulse.
	private bool [] SemipathTrigger;                //Completion status of the current semipath.
	private int TimeIndex = 0;                      //Index of the curent semipath.

	//Methodes
	public bool GetSemipathTrigger (int Index){

		if (Index >= 0 && Index <= Semipath.Length){

			return SemipathTrigger [Index];

		}
		else {

			return false;

		}

	}  //Returns whether a semipath was completed in this frame.

	//_________________________________________________________________________________________________________//

	////////////
	// Colors //
	////////////

	//Values
	public Material [] ColorMaterials;                     //Material list with the correct color indexes.
	public Material OffColor;                              //Material corresponding to color index -1.
	//Methodes
	public Material GetColorMaterial (int ColorIndex){

		if (ColorIndex >= 0 && ColorIndex < ColorMaterials.Length){

			return ColorMaterials [ColorIndex];
		
		}
		else{

			return OffColor;

		}
	
	}  //Returns the material of corresponding color index.
	public int BlendSpark (List <int> ColorList){
		
		int DefaultColor = -1;
		bool HasRed     = ColorList.Contains (0);
		bool HasGreen   = ColorList.Contains (1);
		bool HasBlue    = ColorList.Contains (2);
		bool HasCyan    = ColorList.Contains (3);
		bool HasYellow  = ColorList.Contains (4);
		bool HasMagenta = ColorList.Contains (5);
		bool HasWhite   = ColorList.Contains (6);
		
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
		
	}       //Takes a list of color indexes and returns the index of its blended color.

	//_________________________________________________________________________________________________________//

	//////////////////
	// Scene Change //
	//////////////////

	//Values
	public GameObject FadePlane;                    //The big black thing the gets in front of everything.
	public float FadeOutTime = 2.0f;                //The duration of a fade in or out.
	public float FadingTime;                        //The current fading time.
	private bool ChangingScene = false;             //Variable that controls whether a fade is in progress
	private string NextScene = "LevelSelect";       //The name of the scene that will load after the fade out ends.

	//Methodes
	public void JumpToScene (string SceneName){

		if (!ChangingScene){

			ChangingScene = true;
			NextScene = SceneName;
		
		}

	}  //Start fading to the given scene.
	public bool IsChangingScene (){

		return ChangingScene;

	}              //Checks whether the scene is fading in or out.
	public float GetFadeFactor (){

		return 1.0f-FadingTime/FadeOutTime;

	}               //Returns the state of the fade in/out of the scene.

	//_________________________________________________________________________________________________________//

	///////////
	// Sound //
	///////////

	//Values
	public AudioMixer MasterMixer;             //The audio mixer used in fade out.
	public int [,] HoverSoundPitch;            //Matrix containing the chords. This is not set in the inspector, its set in start.
	private int CurrentPitch = 0;              //The current note of the arpeggio.

	//Methodes
	public int GetHoverPitch (int Chord){
		
		CurrentPitch = (CurrentPitch+1)%HoverSoundPitch.GetLength (1); //Random.Range (0, HoverSoundPitch.Length-1)
		
		return HoverSoundPitch [Chord,CurrentPitch];
		
	}   //Returns an arpeggio sequence of the given chord index; 	
	public int GetPitch (int Chord){

		return HoverSoundPitch [Chord,CurrentPitch];

	}        //Returns the current pitch of the arpeggio in the given chord.

	//_________________________________________________________________________________________________________//

	//////////////
	// Messages //
	//////////////

	// Use this for initialization
	void Start () {

		HoverSoundPitch = new int [,] {{-12,-9,-5,0,3,7,12,15,19,24},{-13,-10,-5,-1,2,7,11,14,19,23},{-12,-7,-4,0,5,8,12,17,20,24}};
		//print (HoverSoundPitch.GetLength(1));
		//print (HoverSoundPitch.GetLength (1));
		//print (HoverSoundPitch.GetLength (2));
		//print (HoverSoundPitch [1,3]);
		Semipath = new float[SemipathCount];
		SemipathTrigger = new bool[SemipathCount];
		FadingTime = FadeOutTime;

	}
	
	// Update is called once per frame
	void Update () {

		for (int i = Semipath.Length-1 ; i >= 0 ; i--){

			if (TimeIndex == i){

				Semipath [i] = Semipath [i] + Time.deltaTime*Frequency*((float)Semipath.Length);

				if (Semipath [i] >= 1.0f){
				
					Semipath [i] = 0.0f;
					SemipathTrigger [i] = true;	

					if (TimeIndex+1 < Semipath.Length){

						TimeIndex ++;

					}
					else{

						TimeIndex = 0;
						PulseCounter = (PulseCounter+1)%CicleDuration;

						if (PulseCounter == 0){

							CanReset = true;

						}

					}

				}

			}
			else {

				SemipathTrigger [i] = false;	

			}

		}
		PathFactor = (1.0f/((float)Semipath.Length))*((float)TimeIndex+Semipath[TimeIndex]);

		if (ChangingScene) {

			if (FadingTime < FadeOutTime){

				FadingTime = FadingTime+Time.deltaTime;
				MasterMixer.SetFloat ("MasterVolume", -80.0f*(1.0f-GetFadeFactor()));
				FadePlane.GetComponent <MeshRenderer> ().materials [0].SetFloat ("_Darkness", GetFadeFactor ());

			}
			else {

				Application.LoadLevel (NextScene);
				ChangingScene = false;
				FadingTime = 0.0f;

			}

		}
		else {
		
			if (FadingTime > 0.0f) {

				FadingTime = FadingTime-Time.deltaTime;
				MasterMixer.SetFloat ("MasterVolume", -80.0f*(1.0f-GetFadeFactor()));
				FadePlane.GetComponent <MeshRenderer> ().materials [0].SetFloat ("_Darkness", GetFadeFactor ());

			}
			else {

				FadingTime = 0.0f;
				FadePlane.GetComponent <MeshRenderer> ().materials [0].SetFloat ("_Darkness", GetFadeFactor ());
				MasterMixer.SetFloat ("MasterVolume", -80.0f*(1.0f-GetFadeFactor()));

			}

		}

	}

	void LateUpdate (){

		if (CanReset){

			CanReset = false;
			if (CurrentCompletion == CompletionScore && !HasReachedLevelCompletion){
				
				HasReachedLevelCompletion = true;

				if (LevelConclusionButton != null){
				
					LevelConclusionButton.GetComponent <SceneChangeButton_BHV> ().ActivateButton ();
				
				}

			}
			else {
				
				ResetCompletionScore ();
			}

		}

	}

}
