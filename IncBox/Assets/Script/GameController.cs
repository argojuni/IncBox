using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;

    Rigidbody2D PlayerRb;

    public ParticleController particleController;
    private SpriteRenderer sprite;

    // Kecepatan lompatan pemain ketika menginjak musuh
    public float jumpForce = 7f;

    // LayerMask untuk memeriksa musuh
    public LayerMask enemyLayerMask;

    public GameObject PopUpQuiz;

    public bool isPlayerDie;

    private void Awake()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        checkpointPos = transform.position;

        AudioManager.Instance.PlayMusic("BgGame");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void Die()
    {
        AudioManager.Instance.PlaySFX("die");
        particleController.PlayDieParticle(transform.position);
        StartCoroutine(Respawn(0.5f));
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Periksa apakah tabrakan terjadi dengan musuh berdasarkan LayerMask
        if (((1 << collision.gameObject.layer) & enemyLayerMask) != 0)
        {
            // Ambil posisi pemain dan musuh
            Vector2 playerPosition = transform.position;
            Vector2 enemyPosition = collision.transform.position;

            // Hitung perbedaan tinggi antara pemain dan musuh
            float heightDifference = playerPosition.y - enemyPosition.y;

            // Jika pemain berada di atas musuh (menghentak musuh dari atas)
            if (heightDifference > 0)
            {
                // Ambil komponen Rigidbody2D pemain
                Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();

                // Berikan lompatan vertikal kepada pemain
                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                }
                Time.timeScale = 0;

                PopUpQuiz.SetActive(true);
            }
        }
    }


    IEnumerator Respawn(float duration)
    {
        PlayerRb.simulated = false;
        sprite.enabled = false;
        Debug.Log("Die");
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        sprite.enabled = true;
        PlayerRb.simulated = true;
    }
}
