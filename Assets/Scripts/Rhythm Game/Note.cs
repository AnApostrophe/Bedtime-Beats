using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static bool paused;

    public List<Sprite> sprites;
    SpriteRenderer sr;
    float flapSpeed;

    void Start()
    {
        //started = false;
        sr = GetComponent<SpriteRenderer>();
        flapSpeed = Random.Range(0.2f, 0.5f);
        sr.sprite = sprites[Random.Range(0, 2)];
        StartCoroutine(flyAnimation());
    }

    IEnumerator flyAnimation()
    {
        while (true)
        {
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(flapSpeed);
            sr.sprite = sr.sprite.Equals(sprites[0]) ? sprites[1] : sprites[0];
        }
    }
}
