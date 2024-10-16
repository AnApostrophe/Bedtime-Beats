using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameboy : MonoBehaviour
{
    [SerializeField] GameObject instructions;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 2f)
        {
            instructions.GetComponent<TextMeshProUGUI>().enabled = GetComponent<SpriteRenderer>().enabled;

            if (Input.GetKeyDown(Player.Instance.interactKey))
            {
                GamePopup.Instance.ToggleGamePopup();
                GetComponent<SpriteRenderer>().enabled ^= true;
            }
        }
        else
        {
            instructions.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
