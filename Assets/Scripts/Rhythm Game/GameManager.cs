using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool createMode;
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject notesPrefab;
    private GameObject oldNotes;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetMinigame();
    }

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!createMode)
        {
            Destroy(col.gameObject);
            DecreaseHealth();
        }
    }

    public void WinMinigame()
    {
        // TODO
        Debug.Log("You Win");
    }

    public void LoseMinigame()
    {
        ResetMinigame();
        GamePopup.Instance.Unpause();
    }

    public void ResetMinigame()
    {
        health = numOfHearts;
        if (oldNotes != null) Destroy(oldNotes);
        oldNotes = Instantiate(notesPrefab, transform.parent);
        oldNotes.SetActive(true);
        GamePopup.Instance.music.time = 0;
        GamePopup.Instance.music.Pause();
    }

    public void DecreaseHealth()
    {
        health--;
        if (health < 1)
        {
            LoseMinigame();
        }
    }
}
