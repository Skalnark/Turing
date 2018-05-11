using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCell : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.tag = "actualCell";
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.tag = "cellTape";
    }
}
