using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static bool paused;
    public Vector2 freezePosition = Vector2.zero;

    public List<Sprite> sprites;
    public bool flapping;
    SpriteRenderer sr;
    float flapSpeed;
    public GameObject endHold;
    public bool isEndHold;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        flapSpeed = Random.Range(0.2f, 0.5f);
        sr.sprite = sprites[Random.Range(0, 2)];
    }

    void Update()
    {
        if (freezePosition != Vector2.zero)
        {
            transform.position = freezePosition;
            if (endHold.transform.position.y < transform.position.y - 0.25f)
            {
                GameManager.Instance.DecreaseHealth();
                endHold.gameObject.transform.position = Vector2.right * 15;
                Destroy(endHold.gameObject);
                transform.position = Vector2.right * 15;
                Destroy(gameObject);
            }
        }
        if (isEndHold)
        {
            List<Vector3> points = new List<Vector3>
            {
                transform.position
            };
            if (endHold != null)
            {
                points.Add(endHold.transform.position);
                GetComponent<LineRenderer>().SetPositions(points.ToArray());
                sr.sprite = endHold.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                GetComponent<LineRenderer>().enabled = false;
            }
        }
        else if (!paused && !flapping)
        {
            StartCoroutine(Flap());
        }
    }

    IEnumerator Flap()
    {
        flapping = true;
        yield return new WaitForSeconds(flapSpeed);
        sr.sprite = sr.sprite.Equals(sprites[0]) ? sprites[1] : sprites[0];
        flapping = false;
    }
}
