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
                int posx = x + GameData.CurrentGpsPosition.OsmTileX;
                int posy = y + GameData.CurrentGpsPosition.OsmTileY;

                if (!_mapTileD.Check(GameData.CurrentZoom, posx, posy))
                {
                    var go = Instantiate(MapTilePrefab);

                    _mapTileD.Add(GameData.CurrentZoom, posx, posy, go);

                    go.name = GameData.CurrentZoom + "x" + posx + "x" + posy;
                    go.transform.parent = transform;
                    go.transform.position = new Vector2(posx, -posy);

                    go.GetComponent<MapTileLoader>().Pos = new Vector2(posx, posy);
                }
            }
        }

        _coroutineRunning = false;
        yield return null;
    }
}
