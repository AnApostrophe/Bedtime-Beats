using System.Collections;
using UnityEngine;

public class CutsceneShadow : MonoBehaviour
{
    public float period = 3f, periodOffset = 1f;
    public float waitDuration = 1f, waitOffset = 0.5f;
    public float walkingDuration = 5f, walkingProgress = 0f;
    public float checkDuration = 1f;

    public GameObject door, backLight, underLight;
    public AudioClip walkingAudio, scareAudio;

    private bool walkingPaused;
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

        transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(transform.localPosition.x, 1f));
        transform.localPosition = Vector2.Lerp(oldPos, targetPos, walkingProgress / walkingDuration);
        source.panStereo = Mathf.Lerp(-1, 1, (transform.localPosition.x + Mathf.Abs(targetPos.x)) / 2 * Mathf.Abs(targetPos.x));
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
                    break;
                case 1: // Stop, don't check
                    while (walkingProgress < walkingDuration / 2)
                    {
                        yield return null;
                    }
                    walkingPaused = true;
                    source.Pause();
                    yield return new WaitForSeconds(waitDuration + Random.Range(-waitOffset, waitOffset));
                    source.UnPause();
                    walkingPaused = false;
                    break;
                case 2: // Stop, check
                    while (walkingProgress < walkingDuration / 2)
                    {
                        yield return null;
                    }
                    walkingPaused = true;
                    source.Pause();
                    yield return new WaitForSeconds(waitDuration + Random.Range(-waitOffset, waitOffset));
                    yield return OpenDoorAndCheck();
                    source.UnPause();
                    walkingPaused = false;
                    break;
                default: // Nothing
                    break;
            }

            yield return new WaitUntil(() => transform.localPosition.Equals(targetPos));
            StartCoroutine(FadeOutWalking());
            yield return new WaitForSeconds(period + Random.Range(-periodOffset, periodOffset));
        }
    }

    IEnumerator OpenDoorAndCheck()
    {
        door.GetComponent<SpriteRenderer>().enabled = false;
        if (!Player.Instance.sleeping)
        {
            source.clip = scareAudio;
            source.Play();
            backLight.GetComponent<SpriteRenderer>().color = Color.red;
            underLight.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(checkDuration);
            Player.Instance.LoseGame();
        }
        door.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(checkDuration);
        door.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(checkDuration);
    }

    IEnumerator FadeInWalking()
    {
        source.Play();
        while(source.volume < 1)
        {
            source.volume += Time.deltaTime / 3;
            yield return null;
        }
    }

    IEnumerator FadeOutWalking()
    {
        while(source.volume > 0)
        {
            source.volume -= Time.deltaTime / 3;
            yield return null;
        }
        source.Stop();
    }
}
