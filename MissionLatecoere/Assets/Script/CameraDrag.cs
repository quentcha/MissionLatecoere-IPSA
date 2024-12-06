using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{

    private Vector3 _origin;
    private Vector3 _difference;
    private Vector3 newPos;

    private Camera _mainCamera;

    private bool _isDragging;

    public float smoothTime = 0.3f;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) _origin = GetMousePosition;
        _isDragging = ctx.started || ctx.performed;
    }

    private void LateUpdate()
    {
        /*
        if (!_isDragging && (_origin.x - _difference.x) - transform.position.x < 10f && (_origin.y - _difference.y) - transform.position.y < 10f)
        { 
            return;
        }
        Debug.Log(Vector3.Distance(transform.position, _origin - _difference));
        _difference.x = GetMousePosition.x - transform.position.x;
        _difference.y = GetMousePosition.y - transform.position.y;
        newPos.x=(_origin.x - _difference.x)*0.1f;
        newPos.y=(_origin.y - _difference.y)*0.1f;
        newPos.z=-10;
        transform.position=newPos;
        //Vector3.Scale(_origin - _difference, new Vector3(0.1f, 0.1f, 1f));
        */
        if (!_isDragging)
        { 
            return;
        }

        _difference = GetMousePosition - transform.position;
        transform.position = _origin - _difference;
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}