using UnityEngine;
using UnityEngine.UI;

public class UIButtonTransparency : MonoBehaviour
{
    public Image buttonImage;

    private bool isTransparent = false;
    private Color normalColor;
    private Color transparentColor;

    void Start()
    {
        // Simpan warna normal tombol
        normalColor = buttonImage.color;

        // Hitung warna transparan dengan alpha 20%
        transparentColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0.2f);
    }

    public void ToggleTransparency()
    {
        isTransparent = !isTransparent;

        if (isTransparent)
        {
            // Set warna tombol menjadi transparan
            buttonImage.color = transparentColor;
        }
        else
        {
            // Kembalikan ke warna normal
            buttonImage.color = normalColor;
        }
    }
}
