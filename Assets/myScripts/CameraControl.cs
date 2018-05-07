using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject machine;
    public float speed = 2;

    private void Update()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            machine.transform.position += new Vector3(Time.deltaTime*speed, 0, 0);
        }
        
        if(Input.GetAxis("Horizontal") < 0)
        {
            machine.transform.position -= new Vector3(Time.deltaTime*speed, 0, 0);
        }
    }
}