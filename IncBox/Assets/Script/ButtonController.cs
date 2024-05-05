using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public EnemyManager enemyManager;
    public InputField enemyNameInput; // InputField untuk memasukkan nama musuh. ubah jadi type string jika tidak mau input

    public GameObject EnemyParticleDie;

    public GameController gameController;
    // Method untuk mengatur nama musuh.
    public void SetEnemyName(string name)
    {
        // Mengatur nama musuh pada input field.
        enemyNameInput.text = name;
    }

    // Method yang akan dipanggil saat tombol ditekan.
    public void DestroyEnemy()
    {
        // Mengambil nama musuh dari input field.
        string enemyName = enemyNameInput.text;
        Time.timeScale = 1f;

        if (enemyName == enemyNameInput.text)
        {
            // Dapatkan transform dari musuh berdasarkan nama
            Transform enemyTransform = GameObject.Find(enemyName).transform;

            // Instansiasi prefab EnemyParticleDie
            GameObject enemyParticleInstance = Instantiate(EnemyParticleDie, enemyTransform.position, enemyTransform.rotation);

            Destroy(enemyParticleInstance, 0.5f);

            enemyManager.DestroyEnemyByName(enemyName);

            HideFirstChild(gameObject);
        }
        
    }

    public void DestroyPlayer()
    {
        gameController.Die();
    }
    public void HideFirstChild(GameObject parent)
    {
        // Mengambil komponen Transform dari parent.
        Transform parentTransform = parent.transform;

        // Jika parent memiliki setidaknya satu child.
        if (parentTransform.childCount > 0)
        {
            // Mengambil child pertama.
            GameObject firstChild = parentTransform.GetChild(0).gameObject;

            // Menyembunyikan child pertama.
            firstChild.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Parent tidak memiliki child.");
        }
    }
}
