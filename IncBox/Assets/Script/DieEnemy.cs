using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnemy : MonoBehaviour
{
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sinkronkan status aktif gameObject parent dengan status aktif child
        if (gameObject.activeInHierarchy != child.activeInHierarchy)
        {
            // Setel status aktif gameObject parent ke status aktif child
            gameObject.SetActive(child.activeInHierarchy);
        }

        // Sinkronkan status aktif child dengan status aktif gameObject parent
        if (child.activeInHierarchy != gameObject.activeInHierarchy)
        {
            // Setel status aktif child ke status aktif gameObject parent
            child.SetActive(gameObject.activeInHierarchy);
        }
    }
}
