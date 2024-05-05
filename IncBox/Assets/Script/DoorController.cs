using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public int pointsNeeded = 5;
    public Text notificationText; // Referensi ke komponen Text untuk menampilkan notifikasi

    private bool isDoorOpened = false;
    public GameObject Door;
    private Animator am;
    public QuizManager quizManager;

    private void Start()
    {
        am = Door.GetComponent<Animator>();
        // Sembunyikan notifikasi saat awal
        HideNotification();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDoorOpened)
        {
            if (quizManager.gameScore >= pointsNeeded)
            {
                OpenDoor();
            }
            else
            {
                ShowNotification("Anda memerlukan " + pointsNeeded + " poin untuk membuka pintu.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Sembunyikan notifikasi saat pemain meninggalkan area pintu
            HideNotification();
        }
    }

    private void OpenDoor()
    {
        AudioManager.Instance.PlaySFX("dooropen");
        Debug.Log("Pintu dibuka!");
        isDoorOpened = true;
        am.SetBool("DoorOpen", true);
        quizManager.gameScore -= 3;
        Destroy(gameObject, 5f);
    }

    private void ShowNotification(string message)
    {
        // Pastikan reference ke notificationText sudah terisi
        if (notificationText != null)
        {
            notificationText.text = message;
            notificationText.gameObject.SetActive(true); // Aktifkan teks notifikasi
        }
        else
        {
            Debug.LogWarning("NotificationText reference belum diatur.");
        }
    }

    private void HideNotification()
    {
        // Pastikan reference ke notificationText sudah terisi
        if (notificationText != null)
        {
            notificationText.text = ""; // Atur teks menjadi kosong
            notificationText.gameObject.SetActive(false); // Nonaktifkan teks notifikasi
        }
        else
        {
            Debug.LogWarning("NotificationText reference belum diatur.");
        }
    }
}
