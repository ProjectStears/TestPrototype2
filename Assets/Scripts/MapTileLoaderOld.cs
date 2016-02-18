using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapTileLoaderOld : MonoBehaviour
{

    public string url;
    public Vector2 pos;

    // Use this for initialization
    IEnumerator Start()
    {

        pos.x = GameData.CurrentGpsPosition.OSMTileX;
        pos.y = GameData.CurrentGpsPosition.OSMTileY;

        url = "http://a.tile.openstreetmap.org/" + GameData.CurrentZoom + "/" + pos.x + "/" + pos.y + ".png";
        GameObject.Find("url").GetComponent<Text>().text = url;
        WWW www = new WWW(url);
        yield return www;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
    }

    // Update is called once per frame
    void Update()
    {

    }
}