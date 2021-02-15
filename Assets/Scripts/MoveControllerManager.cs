using UnityEngine;

public class MoveControllerManager : MonoBehaviour
{
    [SerializeField] private ClawMachineController _clawMachineController;

    private BaseMoveController[] _moveControllers;

    private void Awake()
    {
        _moveControllers = GetComponents<BaseMoveController>();
    }

    private void FixedUpdate()
    {
        if (_clawMachineController.IsHitting)
        {
            return;
        }

        bool isNotHandled = true;

        foreach (var moveController in _moveControllers)
        {
            isNotHandled &= !moveController.TryHandleInput(_clawMachineController);
        }

        if(isNotHandled)
        {
            _clawMachineController.Stop();
        }
    }
}
