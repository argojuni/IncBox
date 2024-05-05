using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallDie : MonoBehaviour
{
    // Kecepatan lompatan pemain ketika menginjak musuh
    public float jumpForce = 5f;
    public GameObject DieObj;
    public GameObject EnemyParticleDie;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Periksa jika tabrakan terjadi dengan pemain
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil posisi pemain dan musuh
            Vector2 playerPosition = collision.transform.position;
            Vector2 enemyPosition = transform.position;

            // Hitung perbedaan tinggi antara pemain dan musuh
            float heightDifference = playerPosition.y - enemyPosition.y;

            // Jika pemain berada di atas musuh (menghentak musuh dari atas)
            if (heightDifference > 0)
            {
                // Ambil komponen Rigidbody2D pemain
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

                // Buat pemain melompat dengan memberi kecepatan vertikal
                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                }


                // Instansiasi prefab EnemyParticleDie
                GameObject enemyParticleInstance = Instantiate(EnemyParticleDie, transform.position, transform.rotation);

                AudioManager.Instance.PlaySFX("die");

                Destroy(enemyParticleInstance, 0.5f);
                // Hancurkan musuh
                Destroy(DieObj);

                // Anda juga bisa menambahkan kode di sini untuk mengubah skor, mengeluarkan suara, dll.
            }
        }
    }

}
