using UnityEngine;

public class JoystickMoveController : BaseMoveController
{
    [SerializeField] private JoystickController _joystickController;
    [SerializeField] private ButtonController _buttonController;

    public override bool TryHandleInput(ClawMachineController clawMachineController)
    {
        bool result = false;

        if (_joystickController.IsControlActive)
        {
            clawMachineController.Move(_joystickController.ForceDirection);

            result |= true;
        }

        if(_buttonController.IsTrigger)
        {
            clawMachineController.Hit();

            result |= true;
        }

        return result;
    }
}
