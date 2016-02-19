using UnityEngine;

public class GPSHandler : MonoBehaviour
{
    public float goodFixTimer;

    void Start()
    {
        goodFixTimer = Config.TimeToGoodGPSFix;

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

            if (locationData.HorizontalAccuracy < Config.MinGPSAcc && locationData.VerticalAccuracy < Config.MinGPSAcc)
            {
                if (goodFixTimer > 0)
                {
                    goodFixTimer -= Time.deltaTime;
                    locationData.GoodFix = false;
                }
                else
                {
                    locationData.GoodFix = true;
                }
            }
            else
            {
                goodFixTimer = Config.TimeToGoodGPSFix;
                locationData.GoodFix = false;
            }

            GameData.CurrentGpsPosition = locationData;
        }
//Debug GPS stuff here ...
        else
        {
            //GameData.CurrentGpsPosition.Latitude += 0.0002f * Time.deltaTime;
            //GameData.CurrentGpsPosition.Longitude += 0.0002f * Time.deltaTime;

            if (goodFixTimer > 0)
            {
                goodFixTimer -= Time.deltaTime;
            }
            else
            {
                GameData.CurrentGpsPosition.GoodFix = true;
            }
        }

        Vector2 _tilePos = Helper.WorldToTilePos(GameData.CurrentGpsPosition.Latitude, GameData.CurrentGpsPosition.Longitude, GameData.CurrentZoom);

        GameData.CurrentGpsPosition.OSMTileX = Mathf.FloorToInt(_tilePos.x);
        GameData.CurrentGpsPosition.OSMTileY = Mathf.FloorToInt(_tilePos.y);

        GameData.CurrentGpsPosition.OSMOnTilePosX = _tilePos.x - GameData.CurrentGpsPosition.OSMTileX;
        GameData.CurrentGpsPosition.OSMOnTilePosY = _tilePos.y - GameData.CurrentGpsPosition.OSMTileY;
    }
}
