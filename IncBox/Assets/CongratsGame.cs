using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratsGame : MonoBehaviour
{
    public GameObject congratsPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlayMusic("congrats");
            congratsPanel.SetActive(true);
        }
    }
}
