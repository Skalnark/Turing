using UnityEngine;
using System.Collections;

public class TeapotMovement : MonoBehaviour
{
    public float speed = 20f;
    public GameObject sun;
    public Vector3 angle = Vector3.up;
    void Update()
    {
        transform.RotateAround(sun.transform.position, angle, speed * Time.deltaTime);
    }
}