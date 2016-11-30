using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static float MinGpsAcc = 40;
    public static float TimeToGoodGpsFix;
    public static Vector3 MaxCameraOffset;

    public static float MapServiceRunAtMostEvery = 5f;
    public static int MapLoadSourroundingTiles = 3;
    public static float MapTileStartLoadingTimeout = 5;
    public static FilterMode MapFilterMode = FilterMode.Point;
    public static string MapLoaderBaseUrl = "http://a.tile.openstreetmap.org/";

    public static float PinchZoomSensitivity = 0.1f;

    public static bool UseDebugGpsPosition;
    public static Helper.LocationData DebugGpsPosition;

    public static bool MapCenterOffsetSet;
    public static int MapCenterTileOffsetX;
    public static int MapCenterTileOffsetY;

    static Config()
    {
        DebugGpsPosition = new Helper.LocationData();
        DebugGpsPosition.Latitude = 48.05080f;
        DebugGpsPosition.Longitude = 8.20934f;
        DebugGpsPosition.Altitude = 800;
        DebugGpsPosition.HorizontalAccuracy = 10;
        DebugGpsPosition.VerticalAccuracy = 10;
        DebugGpsPosition.Timestamp = 5;
        DebugGpsPosition.Status = "Debugging";

        MapCenterOffsetSet = false;
        MaxCameraOffset = new Vector3(1f, 1f, 0.5f);
#if UNITY_EDITOR
        UseDebugGpsPosition = true;
        TimeToGoodGpsFix = 1;
        Debug.Log("Using debug position.");
#elif UNITY_ANDROID
        UseDebugGpsPosition = false;
        TimeToGoodGpsFix = 10;
#endif
    }
}
