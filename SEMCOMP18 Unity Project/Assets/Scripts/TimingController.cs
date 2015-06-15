using UnityEngine;
using System.Collections;

public class TimingController : MonoBehaviour {

    public int bpm = 120;

    private float timer;

    public int beatCounter; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void LateUpdate() {
        int beatsToRun;
        float delta = Time.deltaTime;
        timer += delta;
        beatsToRun = (int)(timer * bpm/60f);
        timer = (timer * bpm / 60f) - (int)(timer * bpm / 60f);
        beatCounter += beatsToRun;
        while(beatsToRun > 0){
            FindObjectOfType<Grid>().Beat();
            beatsToRun--;
        }
	}
}
