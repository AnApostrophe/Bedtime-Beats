using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float moveSpeed = 10f;
    public bool moveDisabled = false;
    public KeyCode interactKey = KeyCode.Space;
    public bool sleeping = true;

    private Vector2 sleepPos;

    private Rigidbody2D rb;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sleepPos = transform.position;
    }

    void Update()
    {
        if (!moveDisabled)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(1);
    }
}
