using System;
using UnityEngine;

public class ControllerButton : MonoBehaviour
{
    [SerializeField] private PointerDownHandler _pointerDownHandler;

    [SerializeField] private float _pushDeltaForceMod = 5.0f;

    public Rigidbody Rigidbody { get; private set; }

    public event Action OnTrigger;
    public event Action OnTriggerExit;

    private float _maxForce;

    private float _deltaForceMod;
    private float _force;
    private bool _isProcessPush;

    

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Camera camera, Action onPointerDown, float maxForce)
    {
        _pointerDownHandler.Init(camera, onPointerDown);

        _maxForce = maxForce;
    }

    public void Push()
    {
        _isProcessPush = true;

        _deltaForceMod = _pushDeltaForceMod;
    }

    public void Release()
    {
        _isProcessPush = false;

        _force = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!_isProcessPush)
        {
            return;
        }

        var deltaForce = _deltaForceMod * Time.fixedDeltaTime;
        _force = Mathf.Min(_force + deltaForce, _maxForce);

        Rigidbody.AddForce(Vector3.down * _force);        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (OnTrigger != null && IsTriggerCollision(collision))
        {
            _deltaForceMod = 0.0f;

            OnTrigger.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (OnTriggerExit != null && IsTriggerCollision(collision))
        {
            _deltaForceMod = _pushDeltaForceMod;

            OnTriggerExit.Invoke();
        }
    }

    private bool IsTriggerCollision(Collision collision)
    {
        return collision.gameObject.layer == LayerMask.NameToLayer("Spot");
    }
}
