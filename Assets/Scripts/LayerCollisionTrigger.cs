using System;
using UnityEngine;

public class LayerCollisionTrigger : MonoBehaviour
{
    private int _detectLayer;
    private Action _onDetect;

    public void Init(int detectLayer, Action onDetect)
    {
        _detectLayer = detectLayer;
        _onDetect = onDetect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _detectLayer)
        {
            _onDetect.Invoke();
        }
    }
}
