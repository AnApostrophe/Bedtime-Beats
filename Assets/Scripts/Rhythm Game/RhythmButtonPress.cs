using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmButtonPress : MonoBehaviour
{
    public GameObject notes;
    public KeyCode key;
    bool active = false;
    GameObject note;
    public Sprite pressedImage;
    public Sprite defaultImage;
    private SpriteRenderer theSR;
    public bool createMode;
    public GameObject n;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (createMode && Input.GetKeyDown(key))
        {
            if (Input.GetKeyDown(key))
            {
                theSR.sprite = pressedImage;
                Instantiate(n, transform.position, Quaternion.identity, notes.transform);
            }
        }


        else
        {
            if (Input.GetKeyDown(key))
            {
                theSR.sprite = pressedImage;
                if (active)
                {
                    Destroy(note);
                }
                else
                {
                    manager.DecreaseHealth();
                }
            }

            if (Input.GetKeyUp(key))
            {
                theSR.sprite = defaultImage;
            }

        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        if (col.gameObject.tag == "Note")
        {
            note = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!GetComponent<Rigidbody2D>().IsTouchingLayers(5))
        {
            active = false;
        }
    }

    bool getCreateMode()
    {
        return createMode;
    }
}
