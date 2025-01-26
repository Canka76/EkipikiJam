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
        public AudioClip gunShot;                // Gunshot sound
    }

    public GunMode[] gunModes;                  // Array of available gun modes
    public Transform firePoint;                 // Where the projectiles spawn
    public int currentModeIndex = 0;            // Current mode index

    private float fireCooldown = 0f;            // Cooldown timer for firing
    private bool isFiring = false;              // For automatic firing control
    public AudioSource audioSource;            // For playing gunshot sounds

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on the GameObject. Gun sounds will not play.");
        }
    }

    private void Update()
    {
        HandleInput();
        HandleFireCooldown();
        HandleUltimateGun();
    }

    private void HandleUltimateGun()
    {
        if (currentModeIndex != 2 || UltGun == null) return;

        if (Input.GetMouseButton(0))
        {
            if (!isHolding)
            {
                isHolding = true;
                holdTime = 0f; // Start tracking hold time
                UltGun.SetActive(true);
                UltGun.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), holdDuration);
            }

            holdTime += Time.deltaTime;

            if (holdTime >= holdDuration)
            {
                TriggerUltimateEffect();
                ResetHold();
            }
        }
        else
        {
            ResetHold();
        }
    }

    private void ResetHold()
    {
        isHolding = false;
        holdTime = 0f;

        if (UltGun != null)
        {
            UltGun.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
            UltGun.SetActive(false);
        }
    }

    private void TriggerUltimateEffect()
    {
        if (currentModeIndex < 0 || currentModeIndex >= gunModes.Length) return;

        var currentMode = gunModes[currentModeIndex];
        isFiring = true;
        TryFire(currentMode);
    }

    private void HandleInput()
    {
        if (gunModes.Length > 0 && currentModeIndex >= 0)
        {
            var currentMode = gunModes[currentModeIndex];

            if (currentMode.autoFire && Input.GetButton("Fire1"))
            {
                isFiring = true;
                TryFire(currentMode);
            }
            else if (Input.GetButtonDown("Fire1"))
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
            if (mode.burstCount > 1)
            {
                StartCoroutine(FireBurst(mode));
            }
            else
            {
                FireProjectile(mode);
            }

            fireCooldown = 1f / mode.fireRate;
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

            if (audioSource != null && mode.gunShot != null)
            {
                audioSource.PlayOneShot(mode.gunShot);
            }
        }
        else
        {
            Debug.LogWarning("Projectile prefab or fire point is not assigned!");
        }
    }

    public void SetMode(GunMode mode)
    {
        if (mode == null)
        {
            currentModeIndex = -1;
            return;
        }
        currentModeIndex = System.Array.IndexOf(gunModes, mode);
    }
}
