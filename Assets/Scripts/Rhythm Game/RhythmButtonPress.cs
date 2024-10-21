using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RhythmButtonPress : MonoBehaviour
{
    public static bool createMode;
    public KeyCode key;
    bool active = false;
    GameObject lastNote;
    SpriteRenderer frog;
    public List<Sprite> noteSprites;
    public Sprite pressedImage;
    public Sprite defaultImage;
    public GameObject n;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        frog = transform.GetChild(0).GetComponent<SpriteRenderer>();
        createMode = manager.createMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Note.paused)
        {
            if (createMode && Input.GetKeyDown(key))
            {
                if (Input.GetKeyDown(key))
                {
                    frog.sprite = pressedImage;
                    GameObject noteFly = Instantiate(n, GetComponent<BoxCollider2D>().bounds.center, Quaternion.identity, GameManager.Instance.oldNotes.transform);
                    noteFly.GetComponent<Note>().sprites = noteSprites;
                }
            }
            else
            {
                if (Input.GetKeyDown(key))
                {
                    frog.sprite = pressedImage;
                    if (active)
                    {
                        Destroy(lastNote);
                    }
                    else
                    {
                        manager.DecreaseHealth();
                    }
                }

                if (Input.GetKeyUp(key))
                {
                    ResetFrog();
                }

            }
        }
    }

    public void ResetFrog()
    {
        frog.sprite = defaultImage;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        if (col.gameObject.tag == "Note")
        {
            lastNote = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }
}
