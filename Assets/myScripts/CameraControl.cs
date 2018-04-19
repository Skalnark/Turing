using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject machine;
    public GameObject spotlight;
    private Vector3 right, left;
    public TextMesh cellNumber;
    private int counter;
	void Start () {
        machine.GetComponent<Transform>();
        //spotlight.GetComponent<Transform>();
        counter = int.Parse(cellNumber.GetComponent<TextMesh>().text);
        right = new Vector3(1.5f, 0, 0);
        left = new Vector3(-1.5f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            machine.transform.position += right;
            StartCoroutine(moveWithDelay(true, spotlight, machine));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(moveWithDelay(false, spotlight, machine));
            machine.transform.position += left;
        }
    }

    public static IEnumerator moveWithDelay(bool side, GameObject objectToMove, GameObject target)
    {
        if (side)
            while (objectToMove.transform.position.x < target.transform.position.x+5)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                objectToMove.transform.position += new Vector3(0.1f, 0, 0);
            }
        else
        {
            while (objectToMove.transform.position.x > target.transform.position.x+5)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                objectToMove.transform.position += new Vector3(-0.1f, 0, 0);
            }
        }
    }
}