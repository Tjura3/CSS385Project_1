using Beginner2D;
using UnityEngine;
using System;


public class EnemyController : MonoBehaviour
{
    bool broken = true;
    public bool isBroken { get { return broken; } }

    // Public variables
    public float speed;
    //public bool vertical;
    public float changeTime = 3.0f;

    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    //float timer;
    //int direction = 1;

    //audio
    AudioSource audioSource;
    PlayerController playerController;

    public ParticleSystem smokeParticleEffect;

    //counter
    public event Action OnFixed;

    //move twards player
    private Vector2 moveDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //timer = changeTime;
        audioSource = GetComponent<AudioSource>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
    }


    void Update()
    {
        /*
        timer -= Time.deltaTime;


        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        */
        if (!broken || playerController == null) return;
        Vector2 playerPosition = playerController.transform.position;
        Vector2 currentPosition = rigidbody2d.position;
        moveDirection = playerPosition - currentPosition;
        moveDirection.Normalize();
        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
    }


    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        //Vector2 position = rigidbody2d.position;
        /*
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        */
        Vector2 position = rigidbody2d.position + moveDirection * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        audioSource.Stop();
        smokeParticleEffect.Stop();
        playerController.PlayEnemyFixSound();
        OnFixed?.Invoke();

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}