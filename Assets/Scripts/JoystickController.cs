using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PointerDownHandler _leverPointerDownHandler;
    [SerializeField] private Rigidbody _leverRigidbody;
    [SerializeField] private float _force = 100.0f;

    public bool IsControlActive { get; private set; }
    public Vector3 ForceDirection { get; private set; }
    
    private Vector3 _mouseStartPosition;

    private void Awake()
    {
        _leverPointerDownHandler.Init(_camera, OnLeverPointerDown);
    }

    private void OnLeverPointerDown()
    {
        IsControlActive = true;
        _mouseStartPosition = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        if (!IsControlActive)
        {
            return;
        }

        if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            IsControlActive = false;
            return;
        }

        var forceDirection = (Input.mousePosition - _mouseStartPosition).normalized;
        forceDirection.z = forceDirection.y;
        forceDirection.y = 0;

        ForceDirection = forceDirection;

        _leverRigidbody.AddForce(_force * forceDirection, ForceMode.Force);
    }
}
