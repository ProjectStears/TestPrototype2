using System;
using UnityEngine;
using System.Collections;

public class MapTileLoader : MonoBehaviour
{
    private GameController GC;

    private Vector2 _pos;
    private bool _startLoading;
    private float _timeoutTimer;
    public Sprite errorSprite;

    public Vector2 Pos
    {
        set
        {
            _startLoading = true;
            _pos = value;
        }
    }

    IEnumerator Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        _timeoutTimer = Config.MapTileStartLoadingTimeout;

        while (!_startLoading)
        {
            _timeoutTimer -= 0.2f;

            if (_timeoutTimer < 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        if (_startLoading)
        {
            StartCoroutine(LoadTile());
        }
    }

    IEnumerator LoadTile()
    {
        var url = Config.MapLoaderBaseUrl + GC.CurrentZoom + "/" + Mathf.FloorToInt(_pos.x) + "/" + Mathf.FloorToInt(_pos.y) + ".png";
        WWW www = new WWW(url);
        yield return www;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            renderer.sprite = errorSprite;
        }
        else
        {
            var tex = www.texture;
            tex.filterMode = Config.MapFilterMode;
            renderer.sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0, 0), 256);
        }
    }
}
