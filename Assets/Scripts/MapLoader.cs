using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class MapLoader : MonoBehaviour
{
    private GameController GC;

    public GameObject TowerPrefab;
    public GameObject MapTilePrefab;

    private Helper.ThreeDimensionalDictionary<int, int, int, GameObject> _mapTileD;
    private float _tileServiceTimer;
    private bool _tileServiceRunning;

    private List<TowerData> _towers;
    private float _towerServiceTimer;
    private bool _towerServiceRunning;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        _tileServiceRunning = false;
        _mapTileD = new Helper.ThreeDimensionalDictionary<int, int, int, GameObject>();
        _towers = new List<TowerData>();
    }

    void Update()
    {
        //Todo: Combine calling code for Tile- and Tower-Service
        if (_tileServiceTimer <= 0 && !_tileServiceRunning && GC.CurrentGpsPosition.GoodFix)
        {
            _tileServiceTimer = Config.MapServiceRunAtMostEvery;
            _tileServiceRunning = true;
            StartCoroutine(MapTileLoaderService());
        }
        else
        {
            _tileServiceTimer -= Time.deltaTime;
        }

        if (_towerServiceTimer <= 0 && !_towerServiceRunning && GC.CurrentGpsPosition.GoodFix)
        {
            _towerServiceTimer = Config.MapServiceRunAtMostEvery;
            _towerServiceRunning = true;
            StartCoroutine(MapTowerLoaderService());
        }
        else
        {
            _towerServiceTimer -= Time.deltaTime;
        }
    }

    IEnumerator MapTileLoaderService()
    {
        for (int y = -Config.MapLoadSourroundingTiles; y <= Config.MapLoadSourroundingTiles; y++)
        {
            for (int x = -Config.MapLoadSourroundingTiles; x <= Config.MapLoadSourroundingTiles; x++)
            {
                int worldPosX = x + GC.CurrentGpsPosition.OsmWorldPosX;
                int worldPosY = y + GC.CurrentGpsPosition.OsmWorldPosY;
                int tilePosX = x + GC.CurrentGpsPosition.OsmTileX;
                int tilePosY = y + GC.CurrentGpsPosition.OsmTileY;

                if (!_mapTileD.Check(GC.CurrentZoom, tilePosX, tilePosY))
                {
                    var go = Instantiate(MapTilePrefab);

                    _mapTileD.Add(GC.CurrentZoom, tilePosX, tilePosY, go);

                    go.name = GC.CurrentZoom + "x" + tilePosX + "x" + tilePosX;
                    go.transform.parent = transform;
                    go.transform.position = new Vector2(worldPosX, -worldPosY);

                    go.GetComponent<MapTileLoader>().Pos = new Vector2(tilePosX, tilePosY);
                }
            }
        }

        _tileServiceRunning = false;
        yield return null;
    }

    IEnumerator MapTowerLoaderService()
    {
        //Well we only have a function for getting them all for now :/
        var towerstemp = GC.GetTowers_All();
        var toweradd = new List<TowerData>();

        //Todo: Use HashSet for towers so we can avoid loading of duplicates much easier
        foreach (var towertemp in towerstemp)
        {
            var test = true;
            foreach (var tower in _towers)
            {
                if (tower.Id == towertemp.Id)
                {
                    test = false;
                }
            }
            if (test)
            {
                toweradd.Add(towertemp);
            }
        }

        _towers.AddRange(toweradd);

        foreach (var tower in toweradd)
        {
            var go = new GameObject();
            int cnt = 1;

            go.transform.parent = transform;
            go.transform.name = "Tower-" + tower.Id + "-" + tower.Name;
            
            Instantiate(GC.ModulesAvailable[0].prefab).transform.parent = go.transform;

            foreach (var module in tower.Modules)
            {
                var check = false;
                foreach (var modulepf in GC.ModulesAvailable)
                {
                    if (module.type == modulepf.name)
                    {
                        check = true;
                        var mod = Instantiate(modulepf.prefab);
                        mod.transform.parent = go.transform;
                        mod.transform.position = new Vector3(0, 0, cnt * Config.TowerStackOffset);
                        cnt++;
                        break;
                    }
                }

                if (!check)
                {
                    Debug.LogError("Module not found!");
                }
            }

            Vector3 towerpos = Helper.WorldToTilePos(tower.Latitude, tower.Longitude, 16) - new Vector2(Config.MapCenterTileOffsetX, Config.MapCenterTileOffsetY);
            towerpos.y = 1 - towerpos.y;
            go.transform.position = towerpos;

        }

        _towerServiceRunning = false;
        yield return null;
    }
}
