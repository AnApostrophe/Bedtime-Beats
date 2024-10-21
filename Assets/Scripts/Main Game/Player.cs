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
    public GameObject GameOverScreen;
    public GameObject ParentCanvas;

    private string animState;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource source;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        GameOverScreen.SetActive(false);
        ParentCanvas.SetActive(true);
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
        PlaySounds();
    }

    public void LoseGame()
    {
        StartCoroutine(GameOverPullUp());

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

    void PlaySounds()
    {
        if (!rb.velocity.Equals(Vector2.zero))
        {
            source.UnPause();
        }
        else
        {
            source.Pause();
        }
    }

    IEnumerator GameOverPullUp()
    {
        Debug.Log("coroutine started");
        yield return new WaitForSeconds(0.15f);
        Debug.Log("waited part 1");
        ParentCanvas.SetActive(true);
        GameOverScreen.SetActive(true);
        Debug.Log("panel should be active" + GameOverScreen.activeSelf + "," + GameOverScreen.activeInHierarchy);
        Debug.Log(GameOverScreen.transform.parent.gameObject.activeSelf + "," + GameOverScreen.transform.parent.gameObject);
        yield return new WaitForSeconds(2.5f);
        Debug.Log("waited part 2");
        SceneManager.LoadScene(0);
        Debug.Log("scene loaded");

    }
}
