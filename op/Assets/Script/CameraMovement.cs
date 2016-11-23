using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject target = null;
    public int speed = 1;
    private Transform cameraTransform;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //target.transform.Translate(Vector3.forward * speed);
                transform.position += cameraTransform.forward * (Time.deltaTime * speed);
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 80, 2 * Time.deltaTime);
            }
           
        }
           
	}
}
