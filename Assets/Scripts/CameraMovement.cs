using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _currentCameraOffset;
    private Vector3 _upperCameraOffsetClamp;
    private Vector3 _lowerCameraOffsetClamp;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        _currentCameraOffset = this.transform.position;
        _currentCameraOffset.z = _mainCamera.transform.position.z;
        _lowerCameraOffsetClamp = _currentCameraOffset - Config.MaxCameraOffset;
        _upperCameraOffsetClamp = _currentCameraOffset + Config.MaxCameraOffset;

    }

    void Update()
    {

#if UNITY_EDITOR
        _currentCameraOffset.x = Mathf.Clamp(_currentCameraOffset.x + Input.GetAxis("Horizontal") * Time.deltaTime, _lowerCameraOffsetClamp.x, _upperCameraOffsetClamp.x);
        _currentCameraOffset.y = Mathf.Clamp(_currentCameraOffset.y + Input.GetAxis("Vertical") * Time.deltaTime, _lowerCameraOffsetClamp.y, _upperCameraOffsetClamp.y);
        _currentCameraOffset.z = Mathf.Clamp(_currentCameraOffset.z + Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime, _lowerCameraOffsetClamp.z, _upperCameraOffsetClamp.z);
#elif UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            _currentCameraOffset.x = Mathf.Clamp(_currentCameraOffset.x - (Input.GetTouch(0).deltaPosition.x * Time.deltaTime) /5, -Config.MaxCameraOffset.x, Config.MaxCameraOffset.x);
            _currentCameraOffset.y = Mathf.Clamp(_currentCameraOffset.y - (Input.GetTouch(0).deltaPosition.y * Time.deltaTime) /5, -Config.MaxCameraOffset.y, Config.MaxCameraOffset.y);
        }
#endif


        transform.localPosition = new Vector3(_currentCameraOffset.x, _currentCameraOffset.y, 0);
        _mainCamera.transform.localPosition = new Vector3(0, 0, _currentCameraOffset.z);
    }
}

