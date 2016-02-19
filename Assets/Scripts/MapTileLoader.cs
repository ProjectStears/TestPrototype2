using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapTileLoader : MonoBehaviour {

    public string url;
    private Vector2 pos;
    private bool startLoading;
    private float timeoutTimer;

    public Vector2 Pos
    {
        set
        {
            startLoading = true;
            pos = value;
        }
    }

    IEnumerator Start()
    {
        timeoutTimer = Config.MapTileStartLoadingTimeout;

        while (!startLoading)
        {
            timeoutTimer -= 0.2f;

            if (timeoutTimer < 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        if (startLoading)
        {
            StartCoroutine(LoadTile());
        }
    }

    IEnumerator LoadTile()
    {
        url = "http://a.tile.openstreetmap.org/" + GameData.CurrentZoom + "/" + Mathf.FloorToInt(pos.x) + "/" + Mathf.FloorToInt(pos.y) + ".png";
//        GameObject.Find("url").GetComponent<Text>().text = url;
        WWW www = new WWW(url);
        yield return www;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite.Create(www.texture, new Rect(0,0,256,256), new Vector2(0,0), 256);
    }
}
