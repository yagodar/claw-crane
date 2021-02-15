using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ControllerButton _button;
    [SerializeField] private float _maxForce = 100.0f;
    public bool IsTrigger { get; private set; }

    private bool _isPushing;

    private void Awake()
    {
        _button.Init(_camera, OnButtonPointerDown, _maxForce);
        _button.OnTrigger += OnButtonTrigger;
        _button.OnTriggerExit += OnButtonTriggerExit;
    }

    private void OnButtonPointerDown()
    {
        _isPushing = true;

        _button.Push();
    }

    private void OnButtonTrigger()
    {
        IsTrigger = true;
    }

    private void OnButtonTriggerExit()
    {
        IsTrigger = false;
    }

    private void FixedUpdate()
    {
        if (!_isPushing)
        {
            return;
        }

        if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            _isPushing = false;

            _button.Release();

            return;
        }
    }
}
