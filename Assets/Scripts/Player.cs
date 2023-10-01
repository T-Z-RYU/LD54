using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float downSpeed = 1.0f;
    public float upSpeed = 1.0f;
    [HideInInspector] public bool isAbsorbing;
    [HideInInspector] public bool isBacking;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private ParticleSystem waterEffect;
    [SerializeField] private GameController gameController;

    [Header("Audio")]
    [SerializeField] private AudioSource playerUpAudio;
    [SerializeField] private AudioSource playerDownAudio;
    private bool audioSwitcher = true;

    private float originY = 3.3f;
    private float lowestY = -4f;

    private void Update()
    {
        if (gameController.isGameStart)
        {
            if (Input.GetKey(KeyCode.Space) && !isBacking)
            {
                if (audioSwitcher)
                {
                    playerDownAudio.Play();
                    audioSwitcher = false;
                }
                spriteRenderer.sprite = sprites[0];
                if (transform.position.y < lowestY)
                {
                    transform.position = new Vector2(transform.position.x, lowestY);
                }
                else
                {
                    rb.velocity = Vector2.down * downSpeed;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space) && transform.position.y < originY)
            {
                if (!audioSwitcher)
                {
                    playerUpAudio.Play();
                    audioSwitcher = true;
                }
                spriteRenderer.sprite = sprites[1];
                isBacking = true;
                rb.velocity = Vector2.up * upSpeed;
            }
            else if (isBacking && transform.position.y >= originY)
            {
                spriteRenderer.sprite = sprites[0];
                isBacking = false;
                rb.velocity = Vector2.zero;
                transform.position = new Vector2(0, originY);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            waterEffect.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            waterEffect.Play();
        }
    }
}
