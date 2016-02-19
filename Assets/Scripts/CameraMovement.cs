using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _cameraOffset;

    void Start()
    {
        _cameraOffset = new Vector3(0, 0, -5);
    }

    void Update()
    {

#if UNITY_EDITOR
        _cameraOffset.x = Mathf.Clamp(_cameraOffset.x - Input.GetAxis("Horizontal") * Time.deltaTime, -Config.MaxCameraOffset.x, Config.MaxCameraOffset.x);
        _cameraOffset.y = Mathf.Clamp(_cameraOffset.y - Input.GetAxis("Vertical") * Time.deltaTime, -Config.MaxCameraOffset.y, Config.MaxCameraOffset.y);
#elif UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            _cameraOffset.x = Mathf.Clamp(_cameraOffset.x - (Input.GetTouch(0).deltaPosition.x * Time.deltaTime) /5, -Config.MaxCameraOffset.x, Config.MaxCameraOffset.x);
            _cameraOffset.y = Mathf.Clamp(_cameraOffset.y - (Input.GetTouch(0).deltaPosition.y * Time.deltaTime) /5, -Config.MaxCameraOffset.y, Config.MaxCameraOffset.y);
        }
#endif

        transform.localPosition = _cameraOffset;
    }
}

