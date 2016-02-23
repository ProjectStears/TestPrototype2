using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _currentCameraOffset;
    private Vector3 _upperCameraOffsetClamp;
    private Vector3 _lowerCameraOffsetClamp;
    private Camera _mainCamera;
    private Transform _player;
    private float _cameraAngle;
    private float _cameraDistance;

    void Start()
    {
        _mainCamera = Camera.main;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currentCameraOffset = this.transform.position;
        _currentCameraOffset.z = _mainCamera.transform.position.z;
        _lowerCameraOffsetClamp = _currentCameraOffset - Config.MaxCameraOffset;
        _upperCameraOffsetClamp = _currentCameraOffset + Config.MaxCameraOffset;
    }

    void Update()
    {

#if UNITY_EDITOR
        _currentCameraOffset.x += Input.GetAxis("Horizontal")*Time.deltaTime;
        _currentCameraOffset.y += Input.GetAxis("Vertical")*Time.deltaTime;
        _currentCameraOffset.z += Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime;

        _player.transform.Rotate(Vector3.forward, Input.GetAxis("Rotation"));

#elif UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            _currentCameraOffset.x = Mathf.Clamp(_currentCameraOffset.x - (Input.GetTouch(0).deltaPosition.x * Time.deltaTime) /5, -Config.MaxCameraOffset.x, Config.MaxCameraOffset.x);
            _currentCameraOffset.y = Mathf.Clamp(_currentCameraOffset.y - (Input.GetTouch(0).deltaPosition.y * Time.deltaTime) /5, -Config.MaxCameraOffset.y, Config.MaxCameraOffset.y);
        }

        if (Input.touchCount == 2)
        {
            var newAngle = Vector2.Angle(Input.GetTouch(0).position, Input.GetTouch(1).position);

            if (_cameraAngle != 0)
            {
                _player.transform.Rotate(Vector3.forward, _cameraAngle - newAngle);
            }
            _cameraAngle = newAngle;

            var newDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            GameData.DebugTouchDist = newDistance;
            if (_cameraDistance != 0)
            {
                _currentCameraOffset.z += (_cameraDistance - newDistance)*Time.deltaTime*Config.PinchZoomSensitivity;
            }
            _cameraDistance = newDistance;
        }
        else
        {
            _cameraAngle = 0;
            _cameraDistance = 0;
        }
#endif

        _currentCameraOffset.x = Mathf.Clamp(_currentCameraOffset.x, _lowerCameraOffsetClamp.x, _upperCameraOffsetClamp.x);
        _currentCameraOffset.y = Mathf.Clamp(_currentCameraOffset.y, _lowerCameraOffsetClamp.y, _upperCameraOffsetClamp.y);
        _currentCameraOffset.z = Mathf.Clamp(_currentCameraOffset.z, _lowerCameraOffsetClamp.z, _upperCameraOffsetClamp.z);

        transform.localPosition = new Vector3(_currentCameraOffset.x, _currentCameraOffset.y, 0);
        _mainCamera.transform.localPosition = new Vector3(0, 0, _currentCameraOffset.z);
    }
}

