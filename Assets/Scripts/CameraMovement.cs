using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private int _cameraSpeed;
    [SerializeField]
    private float _heightY;
    [SerializeField]
    private float _minXLimit;
    [SerializeField]
    private float _maxXLimit;
    [SerializeField]
    private float _minZLimit;
    [SerializeField]
    private float _maxZLimit;

    private float _moveX;
    private float _moveZ;
    private Vector3 _cameraMovement;
    private void Awake()
    {
        _cameraMovement = new Vector3(_moveX, _heightY, _moveZ);
    }
    private void FixedUpdate()
    {
        //Translation
        _moveX = transform.position.x;
        _moveZ = transform.position.z;
        _moveX += Input.GetAxisRaw("Horizontal") * _cameraSpeed;
        _moveZ += Input.GetAxisRaw("Vertical") * _cameraSpeed;
        _moveX = Mathf.Clamp(_moveX, -_minXLimit, _maxXLimit);
        _moveZ = Mathf.Clamp(_moveZ, -_minZLimit, _maxZLimit);

        _cameraMovement.Set(_moveX, _heightY, _moveZ);
        transform.position = _cameraMovement;
    }

}
