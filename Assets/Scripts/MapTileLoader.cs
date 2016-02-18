using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapTileLoader : MonoBehaviour {

    public string url;
    public Vector2 pos;

    IEnumerator Start()
    {
        pos = new Vector2(GameData.CurrentGpsPosition.OSMTileX, GameData.CurrentGpsPosition.OSMTileY);

        url = "http://a.tile.openstreetmap.org/" + GameData.CurrentZoom + "/" + Mathf.FloorToInt(pos.x) + "/" + Mathf.FloorToInt(pos.y) + ".png";
//        GameObject.Find("url").GetComponent<Text>().text = url;
        WWW www = new WWW(url);
        yield return www;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite.Create(www.texture, new Rect(0,0,256,256), new Vector2(0,0), 256);
    }
}
