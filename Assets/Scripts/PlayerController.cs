using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        //TODO: make position calculation more streamlined
        transform.localPosition = new Vector2(GameData.CurrentGpsPosition.OsmWorldPosX + GameData.CurrentGpsPosition.OsmOnTilePosX, (GameData.CurrentGpsPosition.OsmWorldPosY - 1 + GameData.CurrentGpsPosition.OsmOnTilePosY) * -1);
    }
}
