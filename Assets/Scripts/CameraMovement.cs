using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private int cameraSpeed;
    [SerializeField]
    private int zoomSpeed;
    [SerializeField]
    private int minZoom;
    [SerializeField]
    private int maxZoom;
    [SerializeField]
    private float cameraZoom;
    [SerializeField]
    private float moveX;
    [SerializeField]
    private float moveZ;

    private Vector3 cameraMovement;

    private void FixedUpdate()
    {
        //Zoom Code
        cameraZoom = transform.localPosition.y;
        cameraZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        cameraZoom = Mathf.Clamp(cameraZoom, minZoom, maxZoom);
        transform.localPosition = new Vector3(transform.localPosition.x, cameraZoom, transform.localPosition.z);

        //Translation
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        cameraMovement = new Vector3(moveX, 0, moveZ);
        cameraMovement.Normalize();
        cameraMovement *= cameraSpeed;
        transform.Translate(cameraMovement, Space.World);
    }


}
