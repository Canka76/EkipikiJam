using DG.Tweening;
using UnityEngine;

public class GunWithModes : MonoBehaviour
{
    public GameObject UltGun;
   
    [SerializeField]
    private float holdDuration = 2.0f; // Time in seconds to trigger the function

    private float holdTime = 0f;
    private bool isHolding = false;

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
        
        ULtGun();
        
    }

    private void ULtGun()
    {
        if (Input.GetMouseButton(0) && currentModeIndex.Equals(2))
        {
            if (!isHolding)
            {
                isHolding = true; // Start tracking the hold
                holdTime = 0f;
            }

            // Increment the hold time
            holdTime += Time.deltaTime;
            
            UltGun.SetActive(true);
            UltGun.transform.DOScale(new Vector3(.30f, .30f, .30f), holdDuration);
            
            
            if (holdTime >= holdDuration)
            {
                TriggerFunction();
                ResetHold();
                    
            }
        }
        else
        {
            // Reset if the mouse button is released
            ResetHold();
        }
    }
    
    private void ResetHold()
    {
        isFiring = false;
        holdTime = 0f;
        UltGun.transform.DOScale(new Vector3(.10f, .10f, .10f), 0.3f);
        UltGun.SetActive(false);
    }
        
    private void TriggerFunction()
    {
        var currentMode = gunModes[currentModeIndex];
        isFiring = true;
        TryFire(currentMode);
    }

    private void HandleInput()
    {

        // Fire based on the current mode
        if (gunModes.Length > 0)
        {
            var currentMode = gunModes[currentModeIndex];

            if (currentMode.autoFire && Input.GetButton("Fire1") && !currentModeIndex.Equals(3)) // Hold down for auto-fire modes
            {
                isFiring = true;
                TryFire(currentMode);
            }
            else if (Input.GetButtonDown("Fire1") && !currentModeIndex.Equals(3)) // Single shot or burst on mouse click
            {
                isFiring = true;
                TryFire(currentMode);
            }
            else if (Input.GetButtonUp("Fire1") && !currentModeIndex.Equals(3))
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

    public void SetMode(GunMode mode)
    {
        currentModeIndex = System.Array.IndexOf(gunModes, mode);
    }
}
