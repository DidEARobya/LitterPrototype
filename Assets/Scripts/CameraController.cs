using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float movespeed;
    public Vector3 offset;
    public float followdistance;
    public Quaternion rotation;

    private void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position, Player.position + offset + -transform.forward * followdistance, movespeed * Time.deltaTime);
        transform.position = pos;
    }
}
