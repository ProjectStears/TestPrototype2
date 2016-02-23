using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject MapTilePrefab;
    private Helper.ThreeDimensionalDictionary<int, int, int, GameObject> _mapTileD;
    private float _timer;

    private bool _coroutineRunning;

    void Start()
    {
        _coroutineRunning = false;
        _mapTileD = new Helper.ThreeDimensionalDictionary<int, int, int, GameObject>();
    }

    void Update()
    {
        if (_timer <= 0 && !_coroutineRunning && GameData.CurrentGpsPosition.GoodFix)
        {
            _timer = Config.MapServiceRunAtMostEvery;
            _coroutineRunning = true;
            StartCoroutine(MapTileLoaderService());
        }
        else
        {
            _timer -= Time.deltaTime;
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

        _coroutineRunning = false;
        yield return null;
    }
}
