using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private GameObject EnemyParticleDie;
    [SerializeField] private float healthEnemy;
    //public ButtonController buttonController; // Referensi ke skrip ButtonController yang terdapat pada tombol.

    public QuizGameUI quizGameUI;

    public GameObject PopUpQuiz;
    public bool isHealt;

    public GameObject nameParent;

    public string enemyName;

    private void Start()
    {
        enemyName = nameParent.name;
    }
    public void TakeDamage(float damage)
    {
        healthEnemy -= damage;
        if(healthEnemy <= 0 && isHealt)
        {
            GameObject enemyParticleInstance = Instantiate(EnemyParticleDie, transform.position, transform.rotation);

            Destroy(enemyParticleInstance, 0.5f);

            AudioManager.Instance.PlaySFX("die");

            Destroy(gameObject);
        }
        else if (healthEnemy <= 0 && !isHealt)
        {
            Time.timeScale = 0f;
            // Mendapatkan nama musuh dari nama game object.
            enemyName = nameParent.name;

            // Kirimkan nama musuh ke tombol menggunakan method di ButtonController.
            quizGameUI.SetEnemyName(enemyName);

            PopUpQuiz.SetActive(true);
        }
    }
}
