using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Daftar musuh yang akan diatur di editor Unity.
    public List<GameObject> enemies = new List<GameObject>();

    // Method untuk menambahkan musuh ke daftar.
    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    // Method untuk menghapus musuh dari daftar.
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    // Method untuk menghancurkan musuh berdasarkan nama.
    public void DestroyEnemyByName(string name)
    {
        // Mencari musuh dengan nama yang cocok.
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = enemies[i];
            // Mendapatkan nama musuh dari GameObject langsung.
            string enemyName = enemy.name;

            // Membandingkan nama musuh dengan nama yang diberikan.
            if (enemyName == name)
            {
                Destroy(enemy);
                // Hapus musuh dari daftar setelah dihancurkan.
                enemies.RemoveAt(i);
                return; // Keluar dari method setelah musuh ditemukan dan dihancurkan.
            }
        }

        Debug.LogWarning("Musuh dengan nama " + name + " tidak ditemukan.");
    }
}
