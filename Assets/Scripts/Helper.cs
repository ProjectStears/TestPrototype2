using System;
using UnityEngine;

public static class Helper {

    public static Vector2 WorldToTilePos(float lat, float lon, int zoom)
    {
        Vector2 p = new Vector2
        {
            x = (lon + 180.0f) / 360.0f * (1 << zoom),
            y = (1.0f - Mathf.Log(Mathf.Tan(lat * Mathf.PI / 180.0f) + 1.0f / Mathf.Cos(lat * Mathf.PI / 180.0f)) / Mathf.PI) / 2.0f * (1 << zoom)
        };

        return p;
    }

    private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }

    public struct LocationData
    {
        public double Timestamp { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public float HorizontalAccuracy { get; set; }
        public float VerticalAccuracy { get; set; }
        public string Status { get; set; }
        public bool GoodFix { get; set; }
        public int OSMTileX { get; set; }
        public int OSMTileY { get; set; }
        public float OSMOnTilePosX { get; set; }
        public float OSMOnTilePosY { get; set; }

    }

    public struct TowerData
    {
        public string name;
        public float Latitude;
        public float Longitude;
    }
}
