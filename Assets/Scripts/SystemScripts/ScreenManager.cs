using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenManager
{
    public static void SetSceenOrientation()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }
}
