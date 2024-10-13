using System.Collections;
using UnityEngine;

public class DoorShadow : MonoBehaviour
{
    public float period = 3f, periodOffset = 1f;
    public float waitDuration = 1f, waitOffset = 0.5f;
    public float walkingDuration = 5f, walkingProgress = 0f;

    private bool walkingPaused;
    private Vector2 oldPos, targetPos;

    void Start()
    {
        walkingProgress = walkingDuration;

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
    }

    IEnumerator ShadowBehavior()
    {
        // yield return new WaitForSeconds(period * 3);

        while (true)
        {
            int behaviorIndex = Random.Range(0, 2);
            oldPos = transform.localPosition;
            targetPos = oldPos * Vector2.left;
            walkingProgress = 0;

            switch (behaviorIndex)
            {
                case 0: // Walk past, don't check
                    break;
                case 1: // Walk past, stop, don't check
                    while (walkingProgress < walkingDuration / 2)
                    {
                        yield return null;
                    }
                    walkingPaused = true;
                    yield return new WaitForSeconds(waitDuration + Random.Range(-waitOffset, waitOffset));
                    walkingPaused = false;
                    break;
                case 2: // Walk past, stop, check
                    // TODO
                    break;
                default: // Nothing
                    break;
            }

            yield return new WaitUntil(() => transform.localPosition.Equals(targetPos));
            yield return new WaitForSeconds(period + Random.Range(-periodOffset, periodOffset));
        }
    }
}
