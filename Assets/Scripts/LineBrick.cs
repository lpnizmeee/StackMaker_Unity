using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBrick : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    public bool isActiveMesh { get => mesh.enabled; }

    public void SetActiveMesh(bool isActive)
    {
        mesh.enabled = isActive;
    }
    public void DeActiveMesh()
    {
        mesh.enabled = false;
    }
}
