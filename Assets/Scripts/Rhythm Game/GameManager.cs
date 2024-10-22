using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool createMode;
    public float beatTempoPM, beatTempoPS;
    public int health;
    public int numOfHearts;

    public List<GameObject> hearts;
    public List<RhythmButtonPress> buttons;

    public GameObject notesPrefab;
    public GameObject oldNotes;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        beatTempoPS = beatTempoPM / 60;
        numOfHearts = hearts.Count;
        ResetMinigame();
    }

    void Update()
    {
        if (!Note.paused)
        {
            oldNotes.transform.position -= new Vector3(0f, beatTempoPS * Time.deltaTime, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!createMode && !col.GetComponent<Note>().isEndHold)
        {
            // Temp disable due to bug, will lag game out
            // col.gameObject.transform.position = Vector2.right * 15;
            // Destroy(col.gameObject);
            DecreaseHealth();
        }
    }

    public void WinMinigame()
    {
        if (!createMode)
        {
            // TODO
            Debug.Log("You Win");
        }
    }

    public void LoseMinigame()
    {
        ResetMinigame();
        GamePopup.Instance.Unpause();
    }

    public void ResetMinigame()
    {
        health = numOfHearts;
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<SpriteRenderer>().enabled = true;
        }
        foreach (RhythmButtonPress button in buttons)
        {
            button.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = button.defaultImage;
        }
        if (oldNotes != null) Destroy(oldNotes);
        if (createMode)
        {
            oldNotes = notesPrefab;
            oldNotes.SetActive(true);
        }
        else
        {
            oldNotes = Instantiate(notesPrefab, transform.parent);
            oldNotes.name = "CURRENT NOTES";
            oldNotes.SetActive(true);
        }
        GamePopup.Instance.music.time = 0;
        GamePopup.Instance.music.Pause();
    }

    public void DecreaseHealth()
    {
        hearts[health - 1].GetComponent<SpriteRenderer>().enabled = false;
        health--;
        if (health < 1)
        {
            LoseMinigame();
        }
    }
}
