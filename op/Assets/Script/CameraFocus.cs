using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour
{

    public GameObject target = null;
    public bool orbitY = false;
    public int speed = 0;
    Vector3 position;


    // Use this for initialization
    void Start()
    {
        if (target != null)
        {
             
            position = new Vector3 (target.transform.position.x, target.transform.position.y , target.transform.position.z);
            //target.transform.position.Set(target.transform.position.x, target.transform.position.y+100, target.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            if (orbitY)
            {
                transform.RotateAround(position, Vector3.up, speed * Time.deltaTime);

            }
        }
    }
}
