using System;
using UnityEngine;

public class ResolutionManager
{
    public float CameraPosZ = (float)-((float)(Screen.width) / (float)(Screen.height)
                                      * ((1.333 / 6.5) / (2.16533333333 / 5.5)));
    

    public void SetCameraPosition(float CameraPosZ)
    {
        var transformPosition = Camera.main.transform.position;
        transformPosition.z = CameraPosZ;
        Camera.main.transform.position = transformPosition;
        Debug.Log("Camera Position:" + CameraPosZ);
    }
}

