using System;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Vector2 WorldToTilePos(float lat, float lon, int zoom)
    {
        Vector2 p = new Vector2
        {
            x = (lon + 180.0f) / 360.0f * (1 << zoom),
            y = (1.0f - Mathf.Log(Mathf.Tan(lat * Mathf.PI / 180.0f) + 1.0f / Mathf.Cos(lat * Mathf.PI / 180.0f)) / Mathf.PI) / 2.0f * (1 << zoom)
        };

        return p;
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }

    public class LocationData
    {
        public double Timestamp { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public float HorizontalAccuracy { get; set; }
        public float VerticalAccuracy { get; set; }
        public string Status { get; set; }

        private int _osmTileX;
        public int OsmTileX
        {
            get { return _osmTileX; }
            set
            {
                _osmTileX = value;
                OsmWorldPosX = _osmTileX - _osmTileOffsetX;
            }
        }

        private int _osmTileY;
        public int OsmTileY
        {
            get { return _osmTileY; }
            set
            {
                _osmTileY = value;
                OsmWorldPosY = _osmTileY - _osmTileOffsetY;
            }
        }

        public int OsmWorldPosX { get; private set; }
        public int OsmWorldPosY { get; private set; }
        public float OsmOnTilePosX { get; set; }
        public float OsmOnTilePosY { get; set; }

        private int _osmTileOffsetX;
        public int OsmTileOffsetX
        {
            get { return _osmTileOffsetX; }
        }

        private int _osmTileOffsetY;
        public int OsmTileOffsetY
        {
            get { return _osmTileOffsetY; }
        }

        private bool _firstfix;
        private bool _goodFix;
        public bool GoodFix
        {
            get { return _goodFix; }
            set
            {
                _goodFix = value;
                if (_firstfix && value)
                {
                    _osmTileOffsetX = _osmTileX;
                    _osmTileOffsetY = _osmTileY;
                    _firstfix = false;
                }
            }
        }
        public LocationData()
        {
            _firstfix = true;
            _goodFix = false;
        }
    }

    public struct TowerData
    {
        //This is just a dummy imp
        public string Name;
        public float Latitude;
        public float Longitude;
    }

    public class ThreeDimensionalDictionary<TK1, TK2, TK3, TV>
    {
        private readonly Dictionary<TK1, Dictionary<TK2, Dictionary<TK3, TV>>> _dict = new Dictionary<TK1, Dictionary<TK2, Dictionary<TK3, TV>>>();

        public bool Check(TK1 k1, TK2 k2, TK3 k3)
        {
            try
            {
                if (_dict[k1][k2][k3] != null)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public TV Get(TK1 k1, TK2 k2, TK3 k3)
        {
            if (Check(k1, k2, k3))
                return _dict[k1][k2][k3];

            return default(TV);
        }

        public void Add(TK1 k1, TK2 k2, TK3 k3, TV v)
        {
            if (_dict.ContainsKey(k1))
            {
                var k1D = _dict[k1];

                if (k1D.ContainsKey(k2))
                {
                    var k1K2D = k1D[k2];

                    if (!k1K2D.ContainsKey(k3))
                    {
                        k1K2D.Add(k3, v);
                    }
                }
                else
                {
                    k1D.Add(k2, new Dictionary<TK3, TV> { { k3, v } });
                }

            }
            else
            {
                _dict.Add(k1, new Dictionary<TK2, Dictionary<TK3, TV>> { { k2, new Dictionary<TK3, TV> { { k3, v } } } });
            }
        }
    }
}
