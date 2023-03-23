using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().name=="MenuScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else SceneManager.LoadScene("MenuScene");
    }
}
