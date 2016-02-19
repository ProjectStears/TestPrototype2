using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
        //TODO: make position calculation more streamlined
        this.transform.localPosition = new Vector2(GameData.CurrentGpsPosition.OSMTileX + GameData.CurrentGpsPosition.OSMOnTilePosX, (GameData.CurrentGpsPosition.OSMTileY - 1 + GameData.CurrentGpsPosition.OSMOnTilePosY)*-1);
	}
}
