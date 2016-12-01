using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    [Header("Prefab References")] [UsedImplicitly] public Helper.PrefabRef[] ModulesAvailable;

    [Header("Other Stuff")]
    public int CurrentZoom = 16;
    public Helper.LocationData CurrentGpsPosition;

    public Dictionary<string, string> DebugInfo;

    private List<TowerData> Towers;

    // Use this for initialization
    void Start () {
        DebugInfo = new Dictionary<string, string>();

        Towers = new List<TowerData>();
        var t1mods = new List<ModuleData>();
        t1mods.Add(new ModuleData() { type = "fire-1"} );
        t1mods.Add(new ModuleData() { type = "ice-2"} );
        Towers.Add(new TowerData() { Id = 1, Name = "Aula", Latitude = 48.052015f, Longitude = 8.207707f, Modules = t1mods} );
        var t2mods = new List<ModuleData>();
        t2mods.Add(new ModuleData() { type = "fire-3" });
        t2mods.Add(new ModuleData() { type = "ice-1" });
        t2mods.Add(new ModuleData() { type = "fire-1" });
        Towers.Add(new TowerData() { Id = 2, Name = "Mensa", Latitude = 48.050725f, Longitude = 8.208396f, Modules = t2mods} );
        var t3mods = new List<ModuleData>();
        t3mods.Add(new ModuleData() { type = "ice-2" });
        t3mods.Add(new ModuleData() { type = "ice-3" });
        t3mods.Add(new ModuleData() { type = "fire-2" });
        Towers.Add(new TowerData() { Id = 3, Name = "FCL", Latitude = 48.059039f, Longitude = 8.201796f, Modules = t3mods} );
        var t4mods = new List<ModuleData>();
        t4mods.Add(new ModuleData() { type = "ice-3" });
        t4mods.Add(new ModuleData() { type = "fire-3" });
        t4mods.Add(new ModuleData() { type = "ice-3" });
        t4mods.Add(new ModuleData() { type = "fire-3" });
        Towers.Add(new TowerData() { Id = 4, Name="I-Bau", Latitude = 48.049796f, Longitude = 8.211025f, Modules = t4mods});
        CurrentGpsPosition = new Helper.LocationData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerable<TowerData> GetTowers_All()
    {
        return Towers;
    }
}
