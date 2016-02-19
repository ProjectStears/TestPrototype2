using System.Collections.Generic;
using System.Security;
using UnityEngine;

public static class Config
{
    public static float MinGPSAcc = 40;
    public static float TimeToGoodGPSFix;
    public static Vector2 MaxCameraOffset;

    public static float MapServiceRunAtMostEvery = 5f;
    public static int MapLoadSourroundingTiles = 2;
    public static float MapTileStartLoadingTimeout = 5;
    public static FilterMode MapFilterMode = FilterMode.Bilinear;

    public static bool UseDebugGpsPosition;
    public static Helper.LocationData DebugGpsPosition;


    public static List<Helper.TowerData> towers;

    static Config()
    {
        DebugGpsPosition.Latitude = 48.05080f;
        DebugGpsPosition.Longitude = 8.20934f;
        DebugGpsPosition.Altitude = 800;
        DebugGpsPosition.HorizontalAccuracy = 10;
        DebugGpsPosition.VerticalAccuracy = 10;
        DebugGpsPosition.Timestamp = 5;
        DebugGpsPosition.Status = "Debugging";

        towers = new List<Helper.TowerData>();
        towers.Add(new Helper.TowerData() {name = "Badeparadies", Latitude = 47.90756f, Longitude = 8.15947f});
        towers.Add(new Helper.TowerData() { name = "SaSa", Latitude = 48.05238f, Longitude = 8.20802f });
        towers.Add(new Helper.TowerData() { name = "I-Bau", Latitude = 48.05003f, Longitude = 8.21053f });
        towers.Add(new Helper.TowerData() { name = "Kirche", Latitude = 48.05060f, Longitude = 8.20918f });

        MaxCameraOffset = new Vector2(1f, 1f);
#if UNITY_EDITOR
        UseDebugGpsPosition = true;
        TimeToGoodGPSFix = 1;
        Debug.Log("Using debug position.");
#elif UNITY_ANDROID
        UseDebugGpsPosition = false;
        TimeToGoodGPSFix = 10;
#endif
    }
}
