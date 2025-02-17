using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource; // Kéo Audio Source vào đây

    void Start()
    {
        audioSource.Play(); // Bắt đầu phát nhạc
    }
}
