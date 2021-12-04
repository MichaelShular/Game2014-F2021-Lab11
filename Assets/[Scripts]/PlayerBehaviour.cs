using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float sensativity; 

    [Header("Movement")] 
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;

    [Range(0.1f, 0.9f)]
    public float airControlFactor;

    private Rigidbody2D playerRigidbody;

    private Animator animatorController;

    [Header("Sound FX")]
    public AudioSource jumpSound;

    [Header("Dust Trail")]
    public ParticleSystem dustTrail;
    public Color dustColor;

    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin perlin;
    public float shakeInten;
    public float shakeDur;
    public float shakeTimer;
    public bool isShaking;



    // Start is called before the first frame update
    void Start()
    {
        isShaking = false;
        shakeTimer = shakeDur;

        animatorController = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
        dustTrail = GetComponentInChildren<ParticleSystem>();

        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();

        if (isShaking)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0.0f)
            {
                perlin.m_AmplitudeGain = 0;
                shakeTimer = shakeDur;
                isShaking = false;
            }
        }
    }

    private void Move()
    {
        float x = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * sensativity;

        if (isGrounded)
        {
            // Keyboard Input
            float y = (Input.GetAxisRaw("Vertical") + joystick.Vertical) * sensativity;
            float jump = Input.GetAxisRaw("Jump") + ((UIController.jumpButtonDown) ? 1.0f : 0.0f);

            if(jump > 0)
            {
                jumpSound.Play();
                CreateDustTrail();
               
            }

            // Check for Flip

            if (x != 0)
            {
                x = FlipAnimation(x);
                animatorController.SetInteger("AnimationState", (int)PlayerAnimationStates.RUN);//Run state
                CreateDustTrail();
            }
            else
            {
                animatorController.SetInteger("AnimationState", (int)PlayerAnimationStates.IDLE);//Idle state

            }

            // Touch Input
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce; 

            float mass = playerRigidbody.mass * playerRigidbody.gravityScale;


            playerRigidbody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            playerRigidbody.velocity *= 0.99f; // scaling / stopping hack
        }
        else //Air control
        {
            animatorController.SetInteger("AnimationState", (int)PlayerAnimationStates.JUMP); //Jump State

            if (x != 0)
            {
                x = FlipAnimation(x);
                float horizontalMoveForce = x * horizontalForce * airControlFactor;
                float mass = playerRigidbody.mass * playerRigidbody.gravityScale;

                playerRigidbody.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
            CreateDustTrail();
        }

    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);
        isGrounded = (hit) ? true : false;
    }

    private float FlipAnimation(float x)
    {
        // depending on direction scale across the x-axis either 1 or -1
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    private void CreateDustTrail()
    {
        dustTrail.GetComponent<Renderer>().material.SetColor("_Color", dustColor);
        dustTrail.Play();
    }

    public void shakeCamera()
    {
        perlin.m_AmplitudeGain = shakeInten;
        isShaking = true;
    }


    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }

}
