using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameboy : MonoBehaviour
{
    [SerializeField] GameObject instructions;
    [SerializeField] List<Sprite> sprites;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 2f)
        {
            instructions.GetComponent<TextMeshProUGUI>().enabled = !GamePopup.Instance.popupVisible;

            if (Input.GetKeyDown(Player.Instance.interactKey))
            {
                GamePopup.Instance.ToggleGamePopup();
                GetComponent<SpriteRenderer>().sprite = GamePopup.Instance.popupVisible ? sprites[1] : sprites[0];
            }
        }
        else
        {
            instructions.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
