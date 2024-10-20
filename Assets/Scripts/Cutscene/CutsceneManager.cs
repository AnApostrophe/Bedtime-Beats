using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    public bool isTutorial;

    public GameObject notesPrefab;
    private GameObject oldNotes;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetMinigame();
        isTutorial = true;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }

    public void EndTutorial()
    {
        GamePopup.Instance.ToggleGamePopup();
        Player.Instance.GetComponent<Player>().enabled = false;

    }

    public void ResetMinigame()
    {
        if (oldNotes != null) Destroy(oldNotes);
        oldNotes = Instantiate(notesPrefab, transform.parent);
        oldNotes.SetActive(true);
    }
}
