using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    void Update()
    {
        Player.Instance.sleeping = Vector2.Distance(transform.position, Player.Instance.transform.position) < 1f ? true : false;
    }
}
