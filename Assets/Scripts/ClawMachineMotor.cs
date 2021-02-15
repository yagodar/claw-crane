using UnityEngine;

public class ClawMachineMotor : MonoBehaviour
{
    [SerializeField] private Transform _motorBoxTransform;
    [SerializeField] private Transform _railsTransform;

    public void UpdatePosition(Vector3 position)
    {
        var motorBoxPosition = _motorBoxTransform.position;
        motorBoxPosition.x = position.x;
        motorBoxPosition.z = position.z;
        _motorBoxTransform.position = motorBoxPosition;

        var railsPosition = _railsTransform.position;
        railsPosition.z = motorBoxPosition.z;
        _railsTransform.position = railsPosition;
    }
}
