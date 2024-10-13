using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
