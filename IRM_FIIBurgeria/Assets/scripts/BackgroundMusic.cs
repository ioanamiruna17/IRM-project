using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private AudioSource audioSource; // Referință la AudioSource-ul muzicii

    void Awake()
    {
        // Păstrează o singură instanță a GameObject-ului
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Menține GameObject-ul între scene

            // Inițializează volumul la 0.5
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = 0.8f;
            }
        }
        else
        {
            Destroy(gameObject); // Distruge duplicatele
        }
    }
}