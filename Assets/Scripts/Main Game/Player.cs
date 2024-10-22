using System.Collections;
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

    public void WinGame()
    {
        StartCoroutine(WinGamePullUp());
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
        yield return new WaitForSeconds(0.15f);
        ParentCanvas.SetActive(true);
        GameOverScreen.SetActive(true);
        GameOverScreen.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator WinGamePullUp()
    {
        yield return new WaitForSeconds(0.15f);
        ParentCanvas.SetActive(true);
        GameOverScreen.SetActive(true);
        GameOverScreen.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }
}
