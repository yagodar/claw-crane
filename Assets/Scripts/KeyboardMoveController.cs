using UnityEngine;

public class KeyboardMoveController : BaseMoveController
{
    public override bool TryHandleInput(ClawMachineController clawMachineController)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            clawMachineController.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
             clawMachineController.Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            clawMachineController.Move(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            clawMachineController.Move(Vector3.back);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            clawMachineController.Hit();            
        }
        else
        {
            return false;
        }

        return true;
    }
}
