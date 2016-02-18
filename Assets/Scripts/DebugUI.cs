using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {

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
    public GameObject GoTouch1;
    public GameObject GoTouch2;
    public GameObject GoTouchAngle;

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
    private Text _textTouch1;
    private Text _textTouch2;
    private Text _textTouchAngle;

    void Start () {
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
        _textTouch1 = GoTouch1.GetComponent<Text>();
        _textTouch2 = GoTouch2.GetComponent<Text>();
        _textTouchAngle = GoTouchAngle.GetComponent<Text>();
    }
	
	void Update ()
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

	    _textTilex.text = GameData.CurrentGpsPosition.OSMTileX.ToString();
	    _textTiley.text = GameData.CurrentGpsPosition.OSMTileY.ToString();
	}
}
