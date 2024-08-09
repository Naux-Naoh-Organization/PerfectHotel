using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Vector3 vectorOffset = Vector3.zero;
    [SerializeField] private Transform tfmFollow;


    void Start()
    {
        _camera = GetComponent<Camera>();
    }



    void Update()
    {
        _camera.transform.position = tfmFollow.position + vectorOffset;
    }
}
