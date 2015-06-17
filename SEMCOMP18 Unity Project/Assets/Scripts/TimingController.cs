using UnityEngine;
using System.Collections;

public class TimingController : MonoBehaviour {

    public int bpm = 120;

    private float timer = 0;

    public int beatCounter;

    private float delta = 0;
    private int beatsToRun = 0;

	// Use this for initialization
    void Update() {
	}
	
	// Update is called once per frame
    void LateUpdate() {
        delta = Time.deltaTime;
        timer += delta;
        beatsToRun = (int)(timer * bpm / 60f);
        timer = (timer * bpm / 60f) - (int)(timer * bpm / 60f);
        beatCounter += beatsToRun;
        if(beatsToRun > 0){
            FindObjectOfType<Grid>().Beat(beatCounter);
            beatsToRun = 0;
        }
	}
}
