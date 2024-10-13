using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopup : MonoBehaviour
{
    public static GamePopup Instance;
    public GameObject screen;

    public float popupTime = 0.3f;
    public bool popupVisible = false;
    private float popupProgress;
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
        screen.SetActive(!screen.activeInHierarchy);
    }
}
