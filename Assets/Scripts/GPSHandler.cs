using UnityEngine;

public class GPSHandler : MonoBehaviour
{
    private float _goodFixTimer;

    void Start()
    {
        _goodFixTimer = Config.TimeToGoodGpsFix;

        if (Config.UseDebugGpsPosition)
        {
            GameData.CurrentGpsPosition = Config.DebugGpsPosition;
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
            GameData.CurrentGpsPosition.Latitude = Input.location.lastData.latitude;
            GameData.CurrentGpsPosition.Longitude = Input.location.lastData.longitude;
            GameData.CurrentGpsPosition.Altitude = Input.location.lastData.altitude;
            GameData.CurrentGpsPosition.HorizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            GameData.CurrentGpsPosition.VerticalAccuracy = Input.location.lastData.verticalAccuracy;
            GameData.CurrentGpsPosition.Timestamp = Input.location.lastData.timestamp;
            GameData.CurrentGpsPosition.Status = Input.location.status.ToString();

            if (GameData.CurrentGpsPosition.HorizontalAccuracy < Config.MinGpsAcc && GameData.CurrentGpsPosition.VerticalAccuracy < Config.MinGpsAcc)
            {
                if (_goodFixTimer > 0)
                {
                    _goodFixTimer -= Time.deltaTime;
                    GameData.CurrentGpsPosition.GoodFix = false;
                }
                else
                {
                    GameData.CurrentGpsPosition.GoodFix = true;
                }
            }
            else
            {
                _goodFixTimer = Config.TimeToGoodGpsFix;
                GameData.CurrentGpsPosition.GoodFix = false;
            }
        }

        //Debug GPS stuff here ...
        else
        {
            //GameData.CurrentGpsPosition.Latitude += 0.0002f * Time.deltaTime;
            //GameData.CurrentGpsPosition.Longitude += 0.0002f * Time.deltaTime;

            if (_goodFixTimer > 0)
            {
                _goodFixTimer -= Time.deltaTime;
            }
            else
            {
                GameData.CurrentGpsPosition.GoodFix = true;
            }
        }

        Vector2 tilePos = Helper.WorldToTilePos(GameData.CurrentGpsPosition.Latitude, GameData.CurrentGpsPosition.Longitude, GameData.CurrentZoom);

        GameData.CurrentGpsPosition.OsmTileX = Mathf.FloorToInt(tilePos.x);
        GameData.CurrentGpsPosition.OsmTileY = Mathf.FloorToInt(tilePos.y);

        GameData.CurrentGpsPosition.OsmOnTilePosX = tilePos.x - GameData.CurrentGpsPosition.OsmTileX;
        GameData.CurrentGpsPosition.OsmOnTilePosY = tilePos.y - GameData.CurrentGpsPosition.OsmTileY;

        if (GameData.CurrentGpsPosition.GoodFix)
        {
            if (!Config.MapCenterOffsetSet)
            {
                Config.MapCenterTileOffsetX = GameData.CurrentGpsPosition.OsmTileX;
                Config.MapCenterTileOffsetY = GameData.CurrentGpsPosition.OsmTileY;
                Config.MapCenterOffsetSet = true;
            }
        }
    }
}
