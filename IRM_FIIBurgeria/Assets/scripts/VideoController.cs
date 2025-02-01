using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    // Referință publică la VideoPlayer
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Verificăm dacă există un VideoPlayer atașat
        if (videoPlayer != null)
        {
            // Pornim redarea video-ului
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer nu este asignat în Inspector!");
        }
    }
}
