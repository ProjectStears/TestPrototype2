using System;
using UnityEngine;

public class GPSHandler : MonoBehaviour
{
    private GameController GC;

    private float _goodFixTimer;

    void Start()
    {
        try
        {
            GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        catch (Exception)
        {
            throw;
        }

        _goodFixTimer = Config.TimeToGoodGpsFix;

        if (Config.UseDebugGpsPosition)
        {
            GC.CurrentGpsPosition = Config.DebugGpsPosition;
        }
        else
        {
            if (Input.location.isEnabledByUser)
                Input.location.Start(1, 1);
        }
    }

    void Update()
    {
        if (!Config.UseDebugGpsPosition)
        {
            GC.CurrentGpsPosition.Latitude = Input.location.lastData.latitude;
            GC.CurrentGpsPosition.Longitude = Input.location.lastData.longitude;
            GC.CurrentGpsPosition.Altitude = Input.location.lastData.altitude;
            GC.CurrentGpsPosition.HorizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            GC.CurrentGpsPosition.VerticalAccuracy = Input.location.lastData.verticalAccuracy;
            GC.CurrentGpsPosition.Timestamp = Input.location.lastData.timestamp;
            GC.CurrentGpsPosition.Status = Input.location.status.ToString();

            if (GC.CurrentGpsPosition.HorizontalAccuracy < Config.MinGpsAcc && GC.CurrentGpsPosition.VerticalAccuracy < Config.MinGpsAcc)
            {
                if (_goodFixTimer > 0)
                {
                    _goodFixTimer -= Time.deltaTime;
                    GC.CurrentGpsPosition.GoodFix = false;
                }
                else
                {
                    GC.CurrentGpsPosition.GoodFix = true;
                }
            }
            else
            {
                _goodFixTimer = Config.TimeToGoodGpsFix;
                GC.CurrentGpsPosition.GoodFix = false;
            }
        }

        //Debug GPS stuff here ...
        else
        {
            //GC.CurrentGpsPosition.Latitude += 0.0002f * Time.deltaTime;
            //GC.CurrentGpsPosition.Longitude += 0.0002f * Time.deltaTime;

            if (_goodFixTimer > 0)
            {
                _goodFixTimer -= Time.deltaTime;
            }
            else
            {
                GC.CurrentGpsPosition.GoodFix = true;
            }
        }

        Vector2 tilePos = Helper.WorldToTilePos(GC.CurrentGpsPosition.Latitude, GC.CurrentGpsPosition.Longitude, GC.CurrentZoom);

        GC.CurrentGpsPosition.OsmTileX = Mathf.FloorToInt(tilePos.x);
        GC.CurrentGpsPosition.OsmTileY = Mathf.FloorToInt(tilePos.y);

        GC.CurrentGpsPosition.OsmOnTilePosX = tilePos.x - GC.CurrentGpsPosition.OsmTileX;
        GC.CurrentGpsPosition.OsmOnTilePosY = tilePos.y - GC.CurrentGpsPosition.OsmTileY;

        if (GC.CurrentGpsPosition.GoodFix)
        {
            if (!Config.MapCenterOffsetSet)
            {
                Config.MapCenterTileOffsetX = GC.CurrentGpsPosition.OsmTileX;
                Config.MapCenterTileOffsetY = GC.CurrentGpsPosition.OsmTileY;
                Config.MapCenterOffsetSet = true;
            }
        }
    }
}
