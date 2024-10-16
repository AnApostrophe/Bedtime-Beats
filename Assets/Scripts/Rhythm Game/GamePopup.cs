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

    public float popupTime = 0.3f;
    public bool popupVisible = false;
    public float popupProgress;
    private Vector2 startPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        screen.SetActive(false);
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(startPos, Vector2.zero, popupProgress / popupTime);
        popupProgress = Mathf.Clamp(popupProgress, 0, popupTime) + (popupVisible ? Time.deltaTime : -Time.deltaTime);
    }

    public void ToggleGamePopup()
    {
        popupVisible ^= true;
        Player.Instance.moveDisabled ^= true;
        if (popupVisible)
        {
            UnPauseMethod();
        }
        else
        {
            screen.SetActive(false);
        }
    }

    public void UnPauseMethod()
    {
        StartCoroutine(UnPause());
    }

    public IEnumerator UnPause()
    {
        // while (popupProgress >= popupTime / 2)
        // {
        //     yield return null;
        // }
        screen.SetActive(true);
        Note.paused = true;
        pauseText.text = "3";
        pauseText.gameObject.SetActive(true);
        while (int.Parse(pauseText.text) > 0)
        {
            yield return new WaitForSeconds(1);
            pauseText.text = (int.Parse(pauseText.text) - 1).ToString();
        }
        pauseText.gameObject.SetActive(false);
        Note.paused = false;
    }
}
