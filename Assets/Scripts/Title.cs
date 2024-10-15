using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] AudioSource bedAudio;

    void Awake()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        Player.Instance.GetComponent<Player>().enabled = true;
        Player.Instance.GetComponent<AudioSource>().Play();
        bedAudio.volume = 1;
        Time.timeScale = 1;
    }
}
