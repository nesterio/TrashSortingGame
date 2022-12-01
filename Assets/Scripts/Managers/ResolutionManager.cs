using System;
using UnityEngine;

public class ResolutionManager
{
    public int CameraPosZ = (int)-(Screen.currentResolution.width / Screen.currentResolution.height * ((1.333 / 6.5) / (2.16533333333 / 5.5)));
}

