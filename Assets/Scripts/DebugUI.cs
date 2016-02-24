using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public GameObject GoStatus;
    public GameObject GoLat;
    public GameObject GoLon;
    public GameObject GoAlt;
    public GameObject GoTime;
    public GameObject GoHacc;
    public GameObject GoVacc;
    public GameObject GoZoom;
    public GameObject GoTilex;
    public GameObject GoTiley;
    public GameObject GoFix;
    public GameObject GoDebugString;

    private Text _textStatus;
    private Text _textLat;
    private Text _textLon;
    private Text _textAlt;
    private Text _textTime;
    private Text _textHacc;
    private Text _textVacc;
    private Text _textZoom;
    private Text _textTilex;
    private Text _textTiley;
    private Text _textFix;
    private Text _textDebugString;

    void Start()
    {
        _textStatus = GoStatus.GetComponent<Text>();
        _textLat = GoLat.GetComponent<Text>();
        _textLon = GoLon.GetComponent<Text>();
        _textAlt = GoAlt.GetComponent<Text>();
        _textTime = GoTime.GetComponent<Text>();
        _textHacc = GoHacc.GetComponent<Text>();
        _textVacc = GoVacc.GetComponent<Text>();
        _textZoom = GoZoom.GetComponent<Text>();
        _textTilex = GoTilex.GetComponent<Text>();
        _textTiley = GoTiley.GetComponent<Text>();
        _textFix = GoFix.GetComponent<Text>();
        _textDebugString = GoDebugString.GetComponent<Text>();
    }

    void Update()
    {
        _textStatus.text = GameData.CurrentGpsPosition.Status;
        _textLat.text = GameData.CurrentGpsPosition.Latitude.ToString();
        _textLon.text = GameData.CurrentGpsPosition.Longitude.ToString();
        _textAlt.text = GameData.CurrentGpsPosition.Altitude.ToString();
        _textHacc.text = GameData.CurrentGpsPosition.HorizontalAccuracy.ToString();
        _textVacc.text = GameData.CurrentGpsPosition.VerticalAccuracy.ToString();
        _textTime.text = GameData.CurrentGpsPosition.Timestamp.ToString();
        _textZoom.text = GameData.CurrentZoom.ToString();
        _textFix.text = GameData.CurrentGpsPosition.GoodFix.ToString();

        _textTilex.text = GameData.CurrentGpsPosition.OsmTileX.ToString() + "x" + GameData.CurrentGpsPosition.OsmTileY.ToString();
        _textTiley.text = GameData.CurrentGpsPosition.OsmTileOffsetX.ToString() + "x" + GameData.CurrentGpsPosition.OsmTileOffsetY.ToString();

        _textDebugString.text = GameData.DebugString;
    }
}
