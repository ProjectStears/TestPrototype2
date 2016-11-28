using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class MapLoader : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject MapTilePrefab;

    private Helper.ThreeDimensionalDictionary<int, int, int, GameObject> _mapTileD;
    private float _tileServiceTimer;
    private bool _tileServiceRunning;

    private List<Helper.TowerData> _towers;
    private float _towerServiceTimer;
    private bool _towerServiceRunning;

    void Start()
    {
        _tileServiceRunning = false;
        _mapTileD = new Helper.ThreeDimensionalDictionary<int, int, int, GameObject>();
        _towers = new List<Helper.TowerData>();
    }

    void Update()
    {
        //Todo: Combine calling code for Tile- and Tower-Service
        if (_tileServiceTimer <= 0 && !_tileServiceRunning && GameData.CurrentGpsPosition.GoodFix)
        {
            _tileServiceTimer = Config.MapServiceRunAtMostEvery;
            _tileServiceRunning = true;
            StartCoroutine(MapTileLoaderService());
        }
        else
        {
            _tileServiceTimer -= Time.deltaTime;
        }

        if (_towerServiceTimer <= 0 && !_towerServiceRunning && GameData.CurrentGpsPosition.GoodFix)
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
                int worldPosX = x + GameData.CurrentGpsPosition.OsmWorldPosX;
                int worldPosY = y + GameData.CurrentGpsPosition.OsmWorldPosY;
                int tilePosX = x + GameData.CurrentGpsPosition.OsmTileX;
                int tilePosY = y + GameData.CurrentGpsPosition.OsmTileY;

                if (!_mapTileD.Check(GameData.CurrentZoom, tilePosX, tilePosY))
                {
                    var go = Instantiate(MapTilePrefab);

                    _mapTileD.Add(GameData.CurrentZoom, tilePosX, tilePosY, go);

                    go.name = GameData.CurrentZoom + "x" + tilePosX + "x" + tilePosX;
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
        var towerstemp = GameData.GetTower_All();
        var toweradd = new List<Helper.TowerData>();

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
            var go = Instantiate(TowerPrefab);
            go.transform.parent = transform;
            go.transform.name = "Tower-" + tower.Id;

            //Todo: How do I get the right position again?
            go.transform.position = Helper.WorldToTilePos(tower.Latitude, tower.Longitude, 16);

        }

        _towerServiceRunning = false;
        yield return null;
    }
}
