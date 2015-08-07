using UnityEngine;
using System.Collections;

public class SoundEffect_BHV : MonoBehaviour {

	public AudioClip SoundToPlay;
	public int Pitch;

	// Use this for initialization
	void Start () {

		GetComponent <AudioSource> ().pitch = Mathf.Pow (1.05945454f, (float)Pitch);
		GetComponent <AudioSource> ().clip = SoundToPlay;
		GetComponent <AudioSource> ().Play ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!GetComponent <AudioSource> ().isPlaying){

			Destroy (gameObject);

		}
	
	}
}
