using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static bool paused;

    Rigidbody2D rb;
    public float beatTempoPM;
    float beatTempoPS;
    //bool started;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //started = false;
        beatTempoPS = beatTempoPM / 60;
    }

    // Update is called once per frame
    void Update()
    {
        //comment out if need to create map?
        /*if(!started)
        {
            if( Input.anyKeyDown )
            {
                started = true;
            }
        }
        else*/
        if (!paused)
        {
            transform.position -= new Vector3(0f, beatTempoPS * Time.deltaTime, 0f);
        }
    }
}
