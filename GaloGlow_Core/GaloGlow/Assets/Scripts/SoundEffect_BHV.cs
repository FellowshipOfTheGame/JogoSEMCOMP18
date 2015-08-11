using UnityEngine;
using System.Collections;

public class SoundEffect_BHV : MonoBehaviour {

	public AudioClip SoundToPlay;
	public int Pitch;
	public float Volume = 1.0f;

	// Use this for initialization
	void Start () {

		GetComponent <AudioSource> ().pitch = Mathf.Pow (1.05945454f, (float)Pitch);
		GetComponent <AudioSource> ().clip = SoundToPlay;
		//GetComponent <AudioSource> ().volume = Volume;
		GetComponent <AudioSource> ().Play ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!GetComponent <AudioSource> ().isPlaying){

			Destroy (gameObject);

		}
	
	}
}
