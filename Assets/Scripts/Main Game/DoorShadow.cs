using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorShadow : MonoBehaviour
{
    public float period = 3f, periodOffset = 1f;
    public float waitDuration = 1f, waitOffset = 0.5f;
    public float walkingDuration = 5f, walkingProgress = 0f;
    public float checkDuration = 1f;

    public GameObject doorClosed, doorOpen, doorLight;
    public List<Sprite> parentShadows;
    public List<Sprite> doorShadows;
    public AudioClip walkingAudio, scareAudio;

    private bool walkingPaused;
    private bool flipped;
    private Vector2 oldPos, targetPos;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();

        oldPos = transform.localPosition;
        walkingProgress = 0;
        walkingPaused = true;

        StartCoroutine(ShadowBehavior());
    }

    void Update()
    {
        if (!walkingPaused)
        {
            walkingProgress += Time.deltaTime;
        }

        // transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(transform.localPosition.x, 1f));
        transform.localPosition = Vector2.Lerp(oldPos, targetPos, walkingProgress / walkingDuration);
        source.panStereo = Mathf.Lerp(-1, 1, (transform.position.x + Mathf.Abs(targetPos.x)) / 2 * Mathf.Abs(targetPos.x));
    }

    void SwapShadowSprite(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = parentShadows[index];
        }
    }

    IEnumerator ShadowBehavior()
    {
        yield return new WaitForSeconds(period);
        walkingPaused = false;

        while (true)
        {
            int behaviorIndex = Random.Range(0, 3);
            oldPos = transform.localPosition;
            targetPos = oldPos * new Vector2(-1, 1);
            walkingProgress = 0;

            source.clip = walkingAudio;
            StartCoroutine(FadeInWalking());

            switch (behaviorIndex)
            {
                case 0: // Don't stop, don't check
                    SwapShadowSprite(2);
                    break;
                case 1: // Stop, don't check
                    while (walkingProgress < walkingDuration / 2)
                    {
                        yield return null;
                    }
                    walkingPaused = true;
                    SwapShadowSprite(0);
                    source.Pause();
                    yield return new WaitForSeconds(waitDuration + Random.Range(-waitOffset, waitOffset));
                    source.UnPause();
                    walkingPaused = false;
                    SwapShadowSprite(2);
                    break;
                case 2: // Stop, check
                    while (walkingProgress < walkingDuration / 2)
                    {
                        yield return null;
                    }
                    walkingPaused = true;
                    SwapShadowSprite(0);
                    if (flipped) transform.localScale *= new Vector2(-1, 1);
                    source.Pause();
                    yield return new WaitForSeconds(waitDuration + Random.Range(-waitOffset, waitOffset));
                    yield return OpenDoorAndCheck();
                    source.UnPause();
                    walkingPaused = false;
                    SwapShadowSprite(2);
                    if (flipped) transform.localScale *= new Vector2(-1, 1);
                    break;
                default: // Nothing
                    break;
            }

            yield return new WaitUntil(() => transform.localPosition.Equals(targetPos));
            StartCoroutine(FadeOutWalking());
            yield return new WaitForSeconds(period + Random.Range(-periodOffset, periodOffset));
            flipped = true;
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    IEnumerator OpenDoorAndCheck()
    {
        doorClosed.GetComponent<SpriteRenderer>().enabled = false;
        doorOpen.GetComponent<SpriteRenderer>().enabled = true;
        doorOpen.transform.GetChild(0).gameObject.SetActive(true);
        SwapShadowSprite(1);
        transform.GetChild(1).gameObject.SetActive(true);

        if (!Player.Instance.sleeping)
        {
            source.clip = scareAudio;
            source.Play();
            doorLight.GetComponent<SpriteRenderer>().sprite = doorShadows[1];
            yield return new WaitForSeconds(checkDuration);
            Player.Instance.LoseGame();
        }

        doorOpen.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(checkDuration);
        SwapShadowSprite(0);
        transform.GetChild(1).gameObject.SetActive(false);
        doorClosed.GetComponent<SpriteRenderer>().enabled = true;
        doorOpen.GetComponent<SpriteRenderer>().enabled = false;
        doorOpen.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(checkDuration);
    }

    IEnumerator FadeInWalking()
    {
        source.Play();
        while (source.volume < 1)
        {
            source.volume += Time.deltaTime / 3;
            yield return null;
        }
    }

    IEnumerator FadeOutWalking()
    {
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime / 3;
            yield return null;
        }
        source.Stop();
    }
}
