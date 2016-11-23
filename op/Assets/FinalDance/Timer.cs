using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float time = 3f; //30 seconds for you
    AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (time > 0 )
        {
            time -= Time.deltaTime;
        }
        else if(!audio.isPlaying) {
            Debug.Log("Play Audio Here -- Timer Over!!");
            audio.Play();
        }
    }
}
