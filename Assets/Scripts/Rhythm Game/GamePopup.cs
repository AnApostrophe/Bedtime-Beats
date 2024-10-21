using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePopup : MonoBehaviour
{
    public static GamePopup Instance;

    public GameObject screen;
    public TextMeshProUGUI pauseText;
    public AudioSource music;

    public float popupTime = 0.3f;
    public bool popupVisible = false;
    public float popupProgress;
    public const float TUTORIAL_LENGTH = 10f;

    bool paused;

    Vector2 startPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        music = GetComponent<AudioSource>();

        startPos = transform.position;
        paused = true;

        music.Play();
        Pause();
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(startPos, Vector2.zero, popupProgress / popupTime);
        popupProgress = Mathf.Clamp(popupProgress, 0, popupTime) + (popupVisible ? Time.deltaTime : -Time.deltaTime);

        // if (CutsceneManager.Instance.isTutorial && music.time >= TUTORIAL_LENGTH)
        // {
        //     CutsceneManager.Instance.EndTutorial();
        // }

        if (music.time >= music.clip.length)
        {
            GameManager.Instance.WinMinigame();
        }
    }

    public void ToggleGamePopup()
    {
        popupVisible ^= true;
        Player.Instance.moveDisabled ^= true;

        if (popupVisible)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Unpause()
    {
        paused = false;

        StartCoroutine(UnpauseCoroutine());
    }

    public void Pause()
    {
        paused = true;
        foreach (RhythmButtonPress button in GameManager.Instance.buttons)
        {
            button.ResetFrog();
        }

        music.Pause();
        screen.SetActive(false);
    }

    public IEnumerator UnpauseCoroutine()
    {
        screen.SetActive(true);
        Note.paused = true;

        pauseText.text = "3";
        pauseText.gameObject.SetActive(true);
        while (int.Parse(pauseText.text) > 0)
        {
            yield return new WaitForSeconds(1);
            pauseText.text = (int.Parse(pauseText.text) - 1).ToString();
        }

        if (!paused)
        {
            music.UnPause();
            pauseText.gameObject.SetActive(false);
            Note.paused = false;
        }
    }
}
