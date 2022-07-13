using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private int cameraSpeed;
    [SerializeField]
    private float HeightY;
    [SerializeField]
    private float MoveX;
    [SerializeField]
    private float MoveZ;

    private Vector3 cameraMovement;
    private void Awake()
    {
        cameraMovement = new Vector3(MoveX, HeightY, MoveZ);
    }
    private void FixedUpdate()
    {
        //Translation
        MoveX = transform.position.x;
        MoveZ = transform.position.z;
        MoveX += Input.GetAxisRaw("Horizontal") * cameraSpeed;
        MoveZ += Input.GetAxisRaw("Vertical") * cameraSpeed;
        MoveX = Mathf.Clamp(MoveX, -250, 250);
        MoveZ = Mathf.Clamp(MoveZ, -500, 300);

        cameraMovement.Set(MoveX, HeightY, MoveZ);
        transform.position = cameraMovement;
    }

}
