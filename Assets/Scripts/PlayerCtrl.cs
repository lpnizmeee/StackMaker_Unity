using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;

    private void Awake()
    {
        PlayerCtrl.instance = this;
    }
}
