using UnityEngine;

[ExecuteInEditMode]
public class CenterOfMassHelper : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform;

    private void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = Vector3.Scale(_centerTransform.localPosition, transform.localScale);
        rigidbody.WakeUp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetComponent<Rigidbody>().worldCenterOfMass, 0.1f);
    }
}
