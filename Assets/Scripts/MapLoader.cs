using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject MapTilePrefab;
    private Helper.ThreeDimensionalDictionary<int, int, int, GameObject> _mapTileD;
    private float timer;

    private bool coroutineRunning;

	// Use this for initialization
	void Start ()
	{
	    coroutineRunning = false;
        _mapTileD = new Helper.ThreeDimensionalDictionary<int, int, int, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (timer <= 0 && !coroutineRunning && GameData.CurrentGpsPosition.GoodFix)
	    {
	        timer = Config.MapServiceRunAtMostEvery;
	        coroutineRunning = true;
            StartCoroutine(MapTileLoaderService());

            /*
            //TODO: Move this to a TowerLoader class
            foreach (var towerData in Config.towers)
            {
                //TODO: make position calculation more streamlined
                var goT = Instantiate(TowerPrefab);
                var pos = Helper.WorldToTilePos(towerData.Latitude, towerData.Longitude, GameData.CurrentZoom);

                pos.y = (Mathf.FloorToInt(pos.y) - 1 + (pos.y - Mathf.FloorToInt(pos.y)))*-1;

                goT.transform.position = pos;

            }
            */
        }
	    else
	    {
	        timer -= Time.deltaTime;
	    }
	}

    IEnumerator MapTileLoaderService()
    {
        for (int y = -Config.MapLoadSourroundingTiles; y <= Config.MapLoadSourroundingTiles; y++)
        {
            for (int x = -Config.MapLoadSourroundingTiles; x <= Config.MapLoadSourroundingTiles; x++)
            {
                int posx = x + GameData.CurrentGpsPosition.OSMTileX;
                int posy = y + GameData.CurrentGpsPosition.OSMTileY;

                if (!_mapTileD.Check(GameData.CurrentZoom, posx, posy))
                {
                    var go = Instantiate(MapTilePrefab);

                    _mapTileD.Add(GameData.CurrentZoom, posx, posy, go);

                    go.name = GameData.CurrentZoom + "x" + posx + "x" + posy;
                    go.transform.parent = this.transform;
                    go.transform.position = new Vector2(posx, -posy);

                    go.GetComponent<MapTileLoader>().Pos = new Vector2(posx, posy);
                }
            }
        }

        coroutineRunning = false;
        yield return null;
    }
}
