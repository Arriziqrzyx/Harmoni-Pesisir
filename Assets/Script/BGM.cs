using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;

    private void Awake()
    {
        // Cek apakah instance sudah ada
        if (instance == null)
        {
            // Jika tidak ada instance, inisialisasi instance dengan objek saat ini
            instance = this;

            // Jangan hancurkan objek ini saat berpindah scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Jika instance sudah ada, hancurkan objek saat ini
            Destroy(gameObject);
        }
    }
}
