using UnityEngine;

public class GunWithModes : MonoBehaviour
{
    [System.Serializable]
    public class GunMode
    {
        public string modeName;                  // Name of the mode (e.g., "Single Shot", "Burst")
        public GameObject projectilePrefab;      // Projectile prefab to fire
        public float fireRate = 1f;              // Fire rate in shots per second
        public int burstCount = 1;               // Number of shots for burst mode (if applicable)
        public bool autoFire = false;            // True for automatic firing
    }

    public GunMode[] gunModes;                  // Array of available gun modes
    public Transform firePoint;                 // Where the projectiles spawn
    public int currentModeIndex = 0;            // Current mode index

    private float fireCooldown = 0f;            // Cooldown timer for firing
    private bool isFiring = false;              // For automatic firing control

    void Update()
    {
        HandleInput();
        HandleFireCooldown();
    }

    private void HandleInput()
    {
        // Switch modes (e.g., using the number keys or a button)
        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to switch to the next mode
        {
            SwitchMode(1);
        }
        if (Input.GetKeyDown(KeyCode.Q)) // Press 'Q' to switch to the previous mode
        {
            SwitchMode(-1);
        }

        // Fire based on the current mode
        if (gunModes.Length > 0)
        {
            var currentMode = gunModes[currentModeIndex];

            if (currentMode.autoFire && Input.GetButton("Fire1")) // Hold down for auto-fire modes
            {
                isFiring = true;
                TryFire(currentMode);
            }
            else if (Input.GetButtonDown("Fire1")) // Single shot or burst on mouse click
            {
                isFiring = true;
                TryFire(currentMode);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                isFiring = false;
            }
        }
    }

    private void HandleFireCooldown()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    private void TryFire(GunMode mode)
    {
        if (fireCooldown <= 0f)
        {
            if (mode.burstCount > 1) // Burst mode handling
            {
                StartCoroutine(FireBurst(mode));
            }
            else // Single shot or auto-fire
            {
                FireProjectile(mode);
            }

            fireCooldown = 1f / mode.fireRate; // Reset cooldown based on fire rate
        }
    }

    private System.Collections.IEnumerator FireBurst(GunMode mode)
    {
        for (int i = 0; i < mode.burstCount; i++)
        {
            FireProjectile(mode);
            yield return new WaitForSeconds(1f / mode.fireRate);
        }
    }

    private void FireProjectile(GunMode mode)
    {
        if (mode.projectilePrefab != null && firePoint != null)
        {
            Instantiate(mode.projectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Projectile prefab or fire point is not assigned!");
        }
    }

    private void SwitchMode(int direction)
    {
        currentModeIndex = (currentModeIndex + direction + gunModes.Length) % gunModes.Length;
        Debug.Log($"Switched to mode: {gunModes[currentModeIndex].modeName}");
    }
}
