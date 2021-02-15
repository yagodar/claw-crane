using System.Collections;
using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    [SerializeField] private ClawMachineAnchor _anchor;
    [SerializeField] private ClawMachineMotor _motor;
    [SerializeField] private ClawController _claw;

    [Space]

    [SerializeField] private Transform _spotTransform;

    [Space]

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _autoActionAnchorDelaySeconds;
    [SerializeField] private float _autoActionClawDelaySeconds;

    public bool IsHitting { get; private set; }

    private void Awake()
    {
        _anchor.Init(_moveSpeed, _spotTransform.position);
    }

    private void FixedUpdate()
    {
        _motor.UpdatePosition(_anchor.Rigidbody.position);
    }

    public void Move(Vector3 direction)
    {
        _anchor.Move(direction);
    }

    public void Stop()
    {
        _anchor.Stop();
    }

    public void Hit()
    {
        IsHitting = true;

        _anchor.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        _anchor.OnHitMotor += OnAnchorHitMotor;        

        Move(Vector3.down);
    }

    private void OnAnchorHitMotor()
    {
        StartCoroutine(CatchCoroutine());
    }

    private IEnumerator CatchCoroutine()
    {
        _anchor.OnHitMotor -= OnAnchorHitMotor;

        _anchor.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        Stop();

        yield return new WaitForSeconds(_autoActionAnchorDelaySeconds);

        _claw.CloseHooks();

        yield return new WaitForSeconds(_autoActionClawDelaySeconds);

        _anchor.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        Move(Vector3.up);

        _anchor.OnHitRoof += OnAnchorHitRoofOnCatch;
    }

    private void OnAnchorHitRoofOnCatch()
    {
        _anchor.OnHitRoof -= OnAnchorHitRoofOnCatch;

        _anchor.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        _anchor.MoveToSpot();

        _anchor.OnHitSpot += OnAnchorHitSpotOnCatch;
    }

    private void OnAnchorHitSpotOnCatch()
    {
        StartCoroutine(ReleaseCoroutine());
    }

    private IEnumerator ReleaseCoroutine()
    {
        _anchor.OnHitSpot -= OnAnchorHitSpotOnCatch;

        _anchor.Stop();

        yield return new WaitForSeconds(_autoActionAnchorDelaySeconds);

        _claw.OpenHooks();

        yield return new WaitForSeconds(_autoActionClawDelaySeconds);

        IsHitting = false;
    }
}
