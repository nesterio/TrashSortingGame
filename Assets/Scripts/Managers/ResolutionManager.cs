using System;
using UnityEngine;

public class ResolutionManager
{
    public float CameraPosZ = (float)-((float)(Screen.currentResolution.width) / (float)(Screen.currentResolution.height) * ((2.16533333333 / 5.5)/(1.333 / 6.5)));
}

