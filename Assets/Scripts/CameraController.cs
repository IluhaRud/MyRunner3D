using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform targetRight;
    public Transform targetLeft;

    public PlayerController player;

    private Vector3 _position;

    private void Start()
    {
        _position = target.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        if (player.currentLane == Lane.Right)
            target = targetRight;
        if (player.currentLane == Lane.Left)
            target = targetLeft;

        var currentPosition = target.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position, currentPosition, 4f * Time.deltaTime);
        
        var currentRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, 2f * Time.deltaTime);
    }
}
