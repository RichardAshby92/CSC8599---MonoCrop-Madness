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

        //Translation
        moveX = transform.position.x;
        moveZ = transform.position.z;
        moveX += Input.GetAxisRaw("Horizontal") * cameraSpeed;
        moveZ += Input.GetAxisRaw("Vertical") * cameraSpeed;
        moveX = Mathf.Clamp(moveX, -500, 500);
        moveZ = Mathf.Clamp(moveZ, -1000, 500);

        cameraMovement = new Vector3(moveX, cameraZoom, moveZ);
        transform.position = cameraMovement;

        //cameraMovement *= cameraSpeed;
        //cameraMovement.Normalize();
        //transform.Translate(cameraMovement, Space.World);
    }


}
