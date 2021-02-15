using System;
using UnityEngine;

public class ClawMachineAnchor : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public event Action OnHitMotor;
    public event Action OnHitRoof;
    public event Action OnHitSpot;

    private float _moveSpeed;
    private Vector3 _spotPosition;

    private Vector3 _moveDirection;

    private bool _isProcessMove;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(float moveSpeed, Vector3 spotPosition)
    {
        _moveSpeed = moveSpeed;
        _spotPosition = spotPosition;
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
        _isProcessMove = true;
    }

    public void MoveToSpot()
    {
        Move((_spotPosition - Rigidbody.position).normalized);
    }

    public void Stop()
    {
        _isProcessMove = false;
        Rigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (!_isProcessMove)
        {
            return;
        }

        var translate = _moveDirection * _moveSpeed * Time.fixedDeltaTime;
        Rigidbody.MovePosition(Rigidbody.position + translate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollision(collision);
    }

    private void OnCollision(Collision collision)
    {
        if (OnHitMotor != null && collision.gameObject.layer == LayerMask.NameToLayer("Motor"))
        {
            OnHitMotor.Invoke();
        }
        if (OnHitRoof != null && collision.gameObject.layer == LayerMask.NameToLayer("Roof"))
        {
            OnHitRoof.Invoke();
        }
        if (OnHitSpot != null && collision.gameObject.layer == LayerMask.NameToLayer("Spot"))
        {
            OnHitSpot.Invoke();
        }
    }
}
