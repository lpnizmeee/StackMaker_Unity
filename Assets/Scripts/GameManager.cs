using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button ReStart;
    [SerializeField] private PlayerMoving playerMoving;
    
    private void Update()
    {
        if (playerMoving.endGame)
        {
            ReStart.gameObject.SetActive(true);
        }
        else
        {
            ReStart.gameObject.SetActive(false);
        }
    }
}
