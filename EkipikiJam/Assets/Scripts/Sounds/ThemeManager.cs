using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ChangeMusic(Theme newTheme)
    {
        // Change background music if a new theme is selected
        if (audioSource != null && newTheme.backgroundMusic != null)
        {
            // Stop current music if it's playing and then play the new one
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = newTheme.backgroundMusic;
            audioSource.Play();

            Debug.Log($"Music changed to: {newTheme.name}");
        }
    }
}