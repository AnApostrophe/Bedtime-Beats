using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShadow : MonoBehaviour
{
    public float meanParentTime = 0.7f;
    public float randomTime = 0.2f;
    public float meanSpeed = 0.5f;

    private Vector2 oldPos, targetPos;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldPos = transform.position;

        StartCoroutine(ParentAppear());
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(transform.localPosition.x, 1f));
    }

    IEnumerator ParentAppear()
    {
        while (true)
        {
            yield return new WaitForSeconds(meanParentTime + Random.Range(-randomTime, randomTime));
            // Play sound
        }
    }
}
