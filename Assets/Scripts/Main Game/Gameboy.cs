using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboy : MonoBehaviour
{
    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 2f && Input.GetKeyDown(Player.Instance.interactKey))
        {
            GamePopup.Instance.ToggleGamePopup();
            GetComponent<SpriteRenderer>().enabled ^= true;
        }
    }
}
