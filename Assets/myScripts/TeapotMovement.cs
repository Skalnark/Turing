using UnityEngine;
using System.Collections;

public class TeapotMovement : MonoBehaviour
{
    public GameObject sun;
    void Update()
    {
        transform.RotateAround(sun.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}