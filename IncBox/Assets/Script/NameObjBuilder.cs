using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameObjBuilder : MonoBehaviour
{
    private string BuildNameString(Transform currentTransform)
    {
        string nameString = currentTransform.name;

        // Rekursif mencari nama parent hingga parent menjadi null (root)
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
            nameString = currentTransform.name;
        }

        return nameString;
    }

    private void Start()
    {
        // Dapatkan Transform parent dari GameObject "Moving3"
        Transform parentTransform = transform.parent;

        // Panggil metode BuildNameString dengan transform parent
        string nameString = BuildNameString(parentTransform);

        // Cetak nama yang disamakan
        Debug.Log("Nama yang disamakan: " + nameString);
    }
}
