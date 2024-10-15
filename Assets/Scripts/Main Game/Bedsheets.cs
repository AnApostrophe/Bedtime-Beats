using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedsheets : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Stop();
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Player.Instance.sleeping ? 4 : 2;
    }

    void OnTriggerEnter2D()
    {
        GetComponent<AudioSource>().Play();
    }
}
