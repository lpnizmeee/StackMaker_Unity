using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawnPos : MonoBehaviour
{
    [SerializeField] GameObject trsf;
    private void Update()
    {
        SetTransform();
    }
    private void SetTransform() { 
        gameObject.transform.position = trsf.transform.position;
    }
}
