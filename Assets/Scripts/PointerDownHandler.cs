using System;
using UnityEngine;

public class PointerDownHandler : MonoBehaviour
{
    private Camera _camera;
    private Action _onPointerDown;

    public void Init(Camera camera, Action onPointerDown)
    {
        _camera = camera;
        _onPointerDown = onPointerDown;
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var screenPosFar = Input.mousePosition;
        screenPosFar.z = _camera.farClipPlane;
        var posFar = _camera.ScreenToWorldPoint(screenPosFar);

        var screenPosNear = Input.mousePosition;
        screenPosNear.z = _camera.nearClipPlane;
        var posNear = _camera.ScreenToWorldPoint(screenPosNear);

        if(Physics.Raycast(posNear, posFar - posNear, out var hit) && hit.transform.gameObject == gameObject)
        {
            _onPointerDown.Invoke();
        }
    }
}
