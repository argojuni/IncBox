using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyName : MonoBehaviour
{
    //public ButtonController buttonController; // Referensi ke skrip ButtonController yang terdapat pada tombol.

    public QuizGameUI quizGameUI;

    public GameObject nameParent;

    public string enemyName;

    private void Start()
    {
        enemyName = nameParent.name;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika collider lain (misalnya, pemain) menyentuh musuh.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Mendapatkan nama musuh dari nama game object.
            enemyName = nameParent.name;

            // Kirimkan nama musuh ke tombol menggunakan method di ButtonController.
            quizGameUI.SetEnemyName(enemyName);
        }
    }

}
