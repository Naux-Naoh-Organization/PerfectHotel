using Unity.Mathematics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Vector3 vectorRotCam = Vector3.zero;
    [SerializeField] private Vector3 vectorOffset = Vector3.zero;
    [SerializeField] private Transform tfmFollow;


    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.transform.rotation = Quaternion.Euler(vectorRotCam);
    }



    void Update()
    {
        _camera.transform.position = tfmFollow.position + vectorOffset;
        _camera.transform.rotation = Quaternion.Euler(vectorRotCam);
    }
}
