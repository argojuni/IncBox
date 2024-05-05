using UnityEngine;

public class FollowChild : MonoBehaviour
{
    // Referensi ke objek anak yang akan diikuti
    public Transform childTransform;

    void Update()
    {
        // Memperbarui posisi objek induk sesuai dengan posisi objek anak
        transform.position = childTransform.position;

        // Memperbarui rotasi objek induk sesuai dengan rotasi objek anak
        transform.rotation = childTransform.rotation;
    }
}
