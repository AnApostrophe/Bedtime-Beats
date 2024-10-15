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

    private string animState;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        PlayAnimations();
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(0);
    }

    void PlayAnimations()
    {
        if (rb.velocity.x < 0) animState = "WalkLeft";
        else if (rb.velocity.x > 0) animState = "WalkRight";
        else if (rb.velocity.y > 0) animState = "WalkUp";
        else if (rb.velocity.y < 0) animState = "WalkDown";
        else animState = "Idle";
        anim.Play(animState);
    }
}
