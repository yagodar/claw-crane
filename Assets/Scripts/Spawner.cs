using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    [SerializeField] private List<GameObject> _templates;
    [SerializeField] private int _spawnQuantity;

    private IEnumerator Start()
    {
        int i = 0;
        while (i < _spawnQuantity)
        {
            var template = _templates[Random.Range(0, _templates.Count)];
            var go = Instantiate(template, template.transform.parent);
            go.transform.localPosition = transform.localPosition;
            i++;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
