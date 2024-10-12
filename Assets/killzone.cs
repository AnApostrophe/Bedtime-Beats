using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class killzone : MonoBehaviour
{
    GameObject note;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy( note );
    }

    void OnTriggerEnter2D( Collider2D col)
    {
        if( col.gameObject.tag == "Note" )
        {
            note = col.gameObject;
        }
    }
}
