using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RhythmButtonPress : MonoBehaviour
{
    public static bool createMode;
    public KeyCode key;
    SpriteRenderer frog;
    public List<Sprite> noteSprites;
    public Sprite pressedImage;
    public Sprite defaultImage;
    public GameObject n;
    public List<GameObject> touchingNotes;
    GameObject noteFly = null;
    GameObject currentPress = null;

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
            if (createMode)
            {
                if (Input.GetKeyDown(key))
                {
                    frog.sprite = pressedImage;
                    noteFly = Instantiate(n, GetComponent<BoxCollider2D>().bounds.center, Quaternion.identity, GameManager.Instance.oldNotes.transform);
                    noteFly.GetComponent<Note>().sprites = noteSprites;
                }
                else if (Input.GetKeyUp(key))
                {
                    frog.sprite = defaultImage;
                    if (noteFly != null && !touchingNotes.Contains(noteFly))
                    {
                        noteFly.GetComponent<Note>().isEndHold = true;
                        GameObject noteFlyEnd = Instantiate(noteFly, GetComponent<BoxCollider2D>().bounds.center, Quaternion.identity, GameManager.Instance.oldNotes.transform);
                        noteFlyEnd.GetComponent<Note>().isEndHold = false;
                        noteFlyEnd.GetComponent<Note>().endHold = noteFly;
                        noteFly.GetComponent<Note>().endHold = noteFlyEnd;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(key))
                {
                    if (touchingNotes.Count > 0) currentPress = touchingNotes.First();
                    frog.sprite = pressedImage;
                    if (currentPress == null)
                    {
                        manager.DecreaseHealth();
                    }
                    else if (currentPress.GetComponent<Note>().isEndHold)
                    {
                        currentPress.GetComponent<Note>().freezePosition = currentPress.transform.position;
                    }
                    else if (currentPress.GetComponent<Note>().endHold != null)
                    {
                        Debug.Log("bop");
                        GameManager.Instance.DecreaseHealth();
                        currentPress.GetComponent<Note>().endHold.transform.position = Vector2.right * 15;
                        Destroy(currentPress.GetComponent<Note>().endHold);
                        currentPress.transform.position = Vector2.right * 15;
                        Destroy(currentPress);
                        currentPress = null;
                    }
                    else
                    {
                        currentPress.transform.position = Vector2.right * 15;
                        Destroy(currentPress);
                        currentPress = null;
                    }
                }
                else if (Input.GetKeyUp(key))
                {
                    if (currentPress != null)
                    {
                        if (currentPress.GetComponent<Note>().endHold != null)
                        {
                            if (!touchingNotes.Contains(currentPress.GetComponent<Note>().endHold))
                            {
                                manager.DecreaseHealth();
                            }
                            currentPress.GetComponent<Note>().endHold.gameObject.transform.position = Vector2.right * 15;
                            Destroy(currentPress.GetComponent<Note>().endHold.gameObject);
                        }
                        currentPress.transform.position = Vector2.right * 15;
                        Destroy(currentPress);
                    }
                    currentPress = null;

                    ResetFrog();
                }

            }
        }
    }

    public void ResetFrog()
    {
        if (frog != null) frog.sprite = defaultImage;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Note")
        {
            touchingNotes.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Note")
        {
            touchingNotes.Remove(col.gameObject);
        }
    }
}
