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
            Helper.LocationData locationData = new Helper.LocationData();

            locationData.Latitude = Input.location.lastData.latitude;
            locationData.Longitude = Input.location.lastData.longitude;
            locationData.Altitude = Input.location.lastData.altitude;
            locationData.HorizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            locationData.VerticalAccuracy = Input.location.lastData.verticalAccuracy;
            locationData.Timestamp = Input.location.lastData.timestamp;
            locationData.Status = Input.location.status.ToString();

            if (locationData.HorizontalAccuracy < Config.MinGpsAcc && locationData.VerticalAccuracy < Config.MinGpsAcc)
            {
                if (_goodFixTimer > 0)
                {
                    _goodFixTimer -= Time.deltaTime;
                    locationData.GoodFix = false;
                }
                else
                {
                    locationData.GoodFix = true;
                }
            }
            else
            {
                _goodFixTimer = Config.TimeToGoodGpsFix;
                locationData.GoodFix = false;
            }

            GameData.CurrentGpsPosition = locationData;
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
    }
}
