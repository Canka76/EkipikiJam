using UnityEngine;

public class AudioClipSwitcher : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Assign different audio clips in the Inspector
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    // Triggered when another collider enters this GameObject's collider (set to Trigger)
    private void OnTriggerEnter(Collider other)
    {
        // Example: Use tags or names to differentiate colliders
        switch (other.gameObject.tag)
        {
            case "Theme2":
                PlayAudioClip(0); // Play the first audio clip
                break;
            case "Theme3":
                PlayAudioClip(1); // Play the second audio clip
                break;
            case "Theme4":
                PlayAudioClip(2); // Play the third audio clip
                break;
            default:
                Debug.Log("No audio clip assigned for this collider.");
                break;
        }
    }

    // Method to play an audio clip based on index
    private void PlayAudioClip(int clipIndex)
    {
        if (audioClips != null && clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid audio clip index or audioClips array is not set.");
        }
    }
}