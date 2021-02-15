using UnityEngine;

public abstract class BaseMoveController : MonoBehaviour
{
    public abstract bool TryHandleInput(ClawMachineController _clawMachineController);
}
