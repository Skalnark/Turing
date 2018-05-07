using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMachine : MonoBehaviour {

    public float speed = 2;
    public GameObject machine;
    	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < machine.transform.position.x + 3.5f)
        {
            transform.position += new Vector3(Time.deltaTime / (10/speed), 0, 0);
        }else if(transform.position.x > machine.transform.position.x + 3.5f)
        {
            transform.position -= new Vector3(Time.deltaTime / (10 / speed), 0, 0);
        }
	}
}
