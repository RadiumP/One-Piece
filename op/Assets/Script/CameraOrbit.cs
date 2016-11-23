using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    public GameObject target = null;
    public bool orbitY = false;
    public int speed = 100;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(target != null)
        {
            transform.LookAt(target.transform);
            if(orbitY)
            {
                transform.RotateAround(target.transform.position, Vector3.up, speed*Time.deltaTime);

            }
        }
	}
}
