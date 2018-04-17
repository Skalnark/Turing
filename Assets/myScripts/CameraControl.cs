using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject machine;
    //public GameObject spotlight;
    private Vector3 right, left;
    public TextMesh cellNumber;
	void Start () {
        machine.GetComponent<Transform>();
        //spotlight.GetComponent<Transform>();
        right = new Vector3(1.5f, 0, 0);
        left = new Vector3(-1.5f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            machine.transform.position += right;
            //Utils.Wait(3);
            //Debug.Log("Rodou?");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            machine.transform.position += left;
        }
    }
 
}