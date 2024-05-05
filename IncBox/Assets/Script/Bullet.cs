using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet;
    private Rigidbody2D rb;
    public GameObject BulletParticleDestroy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right* speedBullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDamage enemyDamage = collision.GetComponent<EnemyDamage>();

        if(enemyDamage != null)
        {
            AudioManager.Instance.PlaySFX("shootexplo");

            enemyDamage.TakeDamage(1);

            GameObject bulletParticleInstance = Instantiate(BulletParticleDestroy, transform.position, transform.rotation);

            Destroy(bulletParticleInstance, 0.5f);

            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            AudioManager.Instance.PlaySFX("shootexplo");

            GameObject bulletParticleInstance = Instantiate(BulletParticleDestroy, transform.position, transform.rotation);

            Destroy(bulletParticleInstance, 0.5f);

            Destroy(gameObject);
        }
    }        
}
