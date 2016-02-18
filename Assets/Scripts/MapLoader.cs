using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameObject mapTilePrefab;
    private List<GameObject> _mapTiles;
    public bool justone = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameData.CurrentGpsPosition.GoodFix && justone)
	    {
	        var go = Instantiate(mapTilePrefab);
	        go.name = GameData.CurrentGpsPosition.OSMTileX + "x" + GameData.CurrentGpsPosition.OSMTileY;
	        go.transform.parent = this.transform;
	        go.transform.position = new Vector2(GameData.CurrentGpsPosition.OSMTileX, GameData.CurrentGpsPosition.OSMTileY);
	        justone = false;

	        foreach (var towerData in Config.towers)
	        {
	            var goT = Instantiate(towerPrefab);
	            var pos = Helper.WorldToTilePos(towerData.Latitude, towerData.Longitude, GameData.CurrentZoom);

	            pos.y = Mathf.FloorToInt(pos.y) + 1 - (pos.y - Mathf.FloorToInt(pos.y));

	            goT.transform.position = pos;

	        }

	    }
	}
}
