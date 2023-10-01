using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float backSpeed = -10f;
    public float biteValue = 0;
    public float addingValue = 20;
    public float subValue = 80;
    public float maxBiteValue = 100;
    public float bornDistance = 10f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    [SerializeField] private GameController gameController;
    [SerializeField] private MainCamera mainCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource sharkBiteAudio;

    [Header("Effect")]
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem biteEffect;
    [SerializeField] private ParticleSystem waterEffect_0;
    [SerializeField] private ParticleSystem waterEffect_1;

    private bool isBiting;
    private bool isBacking;
    private readonly float deltaBackSpeed = 30f;
    private readonly float seaBottom = -10f;
    private readonly float stayBiteVelocity = 0.2f;
    private readonly float stayBiteTime = 0.3f;
    private float stayBiteCounter = 0;
    private bool hasStayed;
    private bool isBorned;
    private bool isBitingAir;
    private float bitingAirCounter;
    private readonly float bitingAirTime = 0.5f;

    private void Update()
    {
        if (isBitingAir)
        {
            bitingAirCounter += Time.deltaTime;
            if (bitingAirCounter >= bitingAirTime)
            {
                isBitingAir = false;
                sharkBiteAudio.Play();
                mainCamera.Shake();
                bitingAirCounter = 0;
                isBiting = false;
            }
        }
        if (player.isAbsorbing && !isBiting)
        {
            biteValue += addingValue * Time.deltaTime;
            if(biteValue >= maxBiteValue) 
            {
                biteValue = maxBiteValue;
                Born();
                Bite();
            }
        }

        if (isBiting)
        {
            biteValue -= subValue * Time.deltaTime;
            if (biteValue <= 0)
            {
                biteValue = 0;
            }
        }

        if (isBacking)
        {
            rb.velocity -= new Vector2(0,  deltaBackSpeed * Time.deltaTime);
            
            if (rb.velocity.y < stayBiteVelocity && !hasStayed)
            {
                rb.velocity = Vector2.zero;
                stayBiteCounter += Time.deltaTime;
                if (stayBiteCounter >= stayBiteTime)
                {
                    hasStayed = true;
                    stayBiteCounter = 0;
                }
            }
            if (rb.velocity.y < backSpeed)
            {
                rb.velocity = new Vector2(0, backSpeed);
            }
            if (transform.position.y <= seaBottom)
            {
                rb.velocity = Vector2.zero;
                isBacking = false;
                hasStayed = false;
                isBorned = false;
                biteValue = 0;
            }
        }
    }

    private void Born()
    {
        if (!isBorned)
        {
            transform.position = player.transform.position - new Vector3(0, bornDistance, 0);
            isBorned = true;
        }
    }

    private void Bite()
    {
        isBiting = true;
        rb.velocity = Vector2.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartCoroutine(BitePlayer());
        }
        if (collision.CompareTag("Water") && isBacking)
        {
            waterEffect_0.Play();
            waterEffect_1.Play();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && isBiting)
        {
            animator.SetBool("IsBiting", true);
            isBitingAir = true;
            waterEffect_0.Play();
            waterEffect_1.Play();
            biteEffect.Play();
            isBacking = true;
            isBiting = false;
        }
    }

    public void OnAnimatorOver()
    {
        animator.SetBool("IsBiting", false);
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        gameController.Lose();
    }

    private IEnumerator BitePlayer()
    {
        animator.SetBool("IsBiting", true);
        yield return new WaitForSeconds(0.5f);
        sharkBiteAudio.Play();
        mainCamera.Shake();
        rb.velocity = Vector2.zero;
        bloodEffect.Play();
        Destroy(player.gameObject);
        StartCoroutine(GameOver());
    }
}
