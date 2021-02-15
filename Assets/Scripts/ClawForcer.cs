using System;
using UnityEngine;

public class ClawForcer : MonoBehaviour
{    
    [SerializeField] private Rigidbody _baseRigidbody;
    [SerializeField] private LayerCollisionTrigger _buffleCollisionTrigger;
    [SerializeField] private GameObject _buffleDown;
    [SerializeField] private GameObject _buffleUp;

    [Space]

    [SerializeField] private float _moveSpeed;

    public event Action _onCompleteMove;

    private Rigidbody _rigidbody;
    private FixedJoint _fixedJoint;

    private Vector3 _moveDirection;

    private bool _isProcessMove;

    private void Awake()
    {        
        _rigidbody = GetComponent<Rigidbody>();
        _fixedJoint = GetComponent<FixedJoint>();

        _buffleCollisionTrigger.Init(LayerMask.NameToLayer("Buffle"), onDetect: Stop);
    }

    private void FixedUpdate()
    {
        StabilizePosition();

        if (!_isProcessMove) 
        {
            return;
        }

        var translate = _moveDirection * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + translate);
    }

    private void StabilizePosition()
    {
        var position = _rigidbody.position;
        position.x = _baseRigidbody.position.x;
        position.z = _baseRigidbody.position.z;
        _rigidbody.position = position;

        _rigidbody.rotation = _baseRigidbody.rotation;
    }

    public void MoveDown(Action onComplete = null)
    {
        Move(Vector3.down, onComplete);
    }

    public void MoveUp(Action onComplete = null)
    {
        Move(Vector3.up, onComplete);
    }

    private void Move(Vector3 direction, Action onComplete)
    {
        _moveDirection = direction;
        _onCompleteMove = onComplete;

        _buffleDown.SetActive(_moveDirection == Vector3.down);
        _buffleUp.SetActive(_moveDirection == Vector3.up);

        _isProcessMove = true;
        _rigidbody.isKinematic = true;
        _fixedJoint.connectedBody = null;
    }

    private void Stop()
    {
        _isProcessMove = false;
        _rigidbody.isKinematic = false;
        _fixedJoint.connectedBody = _baseRigidbody;

        if(_onCompleteMove != null)
        {
            _onCompleteMove.Invoke();
        }        
    }
}
