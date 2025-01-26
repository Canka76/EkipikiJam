using System;
using UnityEngine;

public class ThemeTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    
    private Theme themeToActivate;
    private void Awake()
    {
        themeToActivate = GetComponent<Theme>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            if (themeToActivate != null)
            {
                ThemeManager.Instance.ChangeMusic(themeToActivate);
            }
        }
    }
}