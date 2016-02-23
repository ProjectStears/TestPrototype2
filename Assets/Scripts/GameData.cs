using UnityEngine;
using MySql.Data.MySqlClient;

public static class GameData
{
    public static Helper.LocationData CurrentGpsPosition;
    public static int CurrentZoom = 16;

    public static MySqlConnection DbConnection;


    public static float DebugTouchDist;

    static GameData()
    {
        CurrentGpsPosition = new Helper.LocationData();
        DbConnection = new MySqlConnection("server=127.0.0.1;uid=root;pwd=;database=stears;");
        try
        {
            DbConnection.Open();
        }
        catch (MySqlException ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }
}

