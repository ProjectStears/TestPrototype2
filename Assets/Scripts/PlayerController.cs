using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController GC;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //TODO: make position calculation more streamlined
        transform.localPosition = new Vector2(GC.CurrentGpsPosition.OsmWorldPosX + GC.CurrentGpsPosition.OsmOnTilePosX, (GC.CurrentGpsPosition.OsmWorldPosY - 1 + GC.CurrentGpsPosition.OsmOnTilePosY) * -1);
    }
}
