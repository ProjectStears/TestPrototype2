using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _cameraOffset;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = new Vector3(0,0,-5);
    }

    // Update is called once per frame
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

        this.transform.localPosition = _cameraOffset;
    }
}

