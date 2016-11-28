using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Helper.LocationData CurrentGpsPosition;
    public static int CurrentZoom = 16;

    public static Dictionary<string, string> DebugInfo;

    private static List<Helper.TowerData> Towers;

    static GameData()
    {
        DebugInfo = new Dictionary<string, string>();

        Towers = new List<Helper.TowerData>();
        Towers.Add(new Helper.TowerData() { Id = 1, Name = "Aula", Latitude = 48.052015f, Longitude = 8.207707f });
        Towers.Add(new Helper.TowerData() { Id = 2, Name = "Mensa", Latitude = 48.050725f, Longitude = 8.208396f });
        Towers.Add(new Helper.TowerData() { Id = 3, Name = "FCL", Latitude = 48.059039f, Longitude = 8.201796f });

        CurrentGpsPosition = new Helper.LocationData();
    }

    // The following functions should later be replaced by REST calls to the backend
    public static IList<Helper.TowerData> GetTower_All()
    {
        return Towers;
    }

}

