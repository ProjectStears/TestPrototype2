using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static float MinGpsAcc = 40;
    public static float TimeToGoodGpsFix;
    public static Vector3 MaxCameraOffset;

    public static float MapServiceRunAtMostEvery = 5f;
    public static int MapLoadSourroundingTiles = 2;
    public static float MapTileStartLoadingTimeout = 5;
    public static FilterMode MapFilterMode = FilterMode.Bilinear;
    public static string MapLoaderBaseUrl = "http://a.tile.openstreetmap.org/";

    public static bool UseDebugGpsPosition;
    public static Helper.LocationData DebugGpsPosition;

    public static List<Helper.TowerData> Towers;

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

        Towers = new List<Helper.TowerData>
        {
            new Helper.TowerData() {Name = "Badeparadies", Latitude = 47.90756f, Longitude = 8.15947f},
            new Helper.TowerData() {Name = "SaSa", Latitude = 48.05238f, Longitude = 8.20802f},
            new Helper.TowerData() {Name = "I-Bau", Latitude = 48.05003f, Longitude = 8.21053f},
            new Helper.TowerData() {Name = "Kirche", Latitude = 48.05060f, Longitude = 8.20918f}
        };

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
