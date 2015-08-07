using UnityEngine;
using System.Collections;

public class LevelSelectGod_BHV : MonoBehaviour {

	public int [] HoverSoundPitch;
	private int CurrentPitch = 0;

	public int GetHoverPitch (){

		CurrentPitch = (CurrentPitch+1)%HoverSoundPitch.Length; //Random.Range (0, HoverSoundPitch.Length-1)

		return HoverSoundPitch [CurrentPitch];

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
