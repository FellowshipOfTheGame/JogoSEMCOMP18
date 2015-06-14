using UnityEngine;
using System.Collections;

public class TimingController : MonoBehaviour {

    public int bpm = 120;

    private float timer;

    public int compassCounter; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update() {
        int beatsToRun;
        float delta = Time.deltaTime;
        timer += delta;
        beatsToRun = (int)(timer * bpm/60);
        timer = (timer * bpm / 60) - (int)(timer * bpm / 60);
	}
}
