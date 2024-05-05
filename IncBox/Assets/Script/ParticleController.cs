using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem dieParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0,0.2f)]
    [SerializeField] float dustFormationPeriod;
    
    [SerializeField] Rigidbody2D playerRb;

    float counter;
    bool isOnGround;

    private void Start()
    {
        touchParticle.transform.parent = null;
    }
    private void Update()
    {
        counter += Time.deltaTime;

        if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayTouchParticle(Vector2 pos)
    {
        touchParticle.transform.position = pos;
        touchParticle.Play();
    }

    public void PlayMovementParticle(Vector2 pos)
    {
        movementParticle.transform.position = pos;
        movementParticle.Play();
    }

    public void PlayFallParticle(Vector2 pos)
    {
        fallParticle.transform.position = pos;
        fallParticle.Play();
    }
    public void PlayDieParticle(Vector2 pos)
    {
        dieParticle.transform.position = pos;
        dieParticle.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
