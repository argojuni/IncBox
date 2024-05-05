using System.Collections;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    public GameObject player; // Objek GameObject pemain
    public GameObject[] enemies; // Array objek GameObject musuh
    // Jarak maksimum untuk membuat musuh muncul
    public float distanceX = 5f; // Jarak horizontal
    public float distanceY = 5f; // Jarak vertikal

    void Update()
    {
        // Memeriksa apakah objek
        // tidak null
        if (player == null || enemies == null || enemies.Length == 0)
        {
            Debug.LogWarning("Player atau Enemy tidak terhubung ke skrip!");
            return;
        }

        // Mendapatkan posisi pemain
        Vector2 playerPosition = player.transform.position;

        // Loop melalui setiap musuh
        foreach (GameObject enemy in enemies)
        {
            // Memeriksa apakah objek musuh tidak null
            if (enemy != null)
            {
                // Mendapatkan posisi musuh
                Vector2 enemyPosition = enemy.transform.position;

                // Menghitung jarak antara pemain dan musuh
                float distanceToEnemyX = Mathf.Abs(playerPosition.x - enemyPosition.x);
                float distanceToEnemyY = Mathf.Abs(playerPosition.y - enemyPosition.y);

                // Jika pemain berada dalam jarak tertentu di sekitar musuh, tampilkan musuh
                if (distanceToEnemyX <= distanceX && distanceToEnemyY <= distanceY)
                {
                    enemy.SetActive(true); // Tampilkan musuh
                }
                else
                {
                    enemy.SetActive(true); // Sembunyikan musuh
                }
            }
        }
    }
}
