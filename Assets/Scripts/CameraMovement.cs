using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int cameraSpeed;

    public Vector3 cameraMovement;

    private void FixedUpdate()
    {
        cameraMovement = GetInput();
        if(cameraMovement.sqrMagnitude > 0)
        {
            cameraMovement = cameraMovement * cameraSpeed;
            transform.Translate(cameraMovement, Space.World);
        }



    }

    Vector3 GetInput()
    {
        Vector3 inputVec = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            inputVec += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVec += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVec += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVec += new Vector3(1, 0, 0);
        }

        return inputVec;
    }
}
