using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillManagerRefactor : MonoBehaviour
{
    public GunWithModes gunWithModes;
    public GameObject skillPanel;
    public GameObject skillCellPrefab;
    public Sprite skillCellSelected;
    public Sprite initalSprite;

    [SerializeField] private float explosiveCooldown = 5f;
    [SerializeField] private float iceGunCooldown = 5f;

    
    private int selectedIndex = 0;
    public List<GunWithModes.GunMode> activeModes = new List<GunWithModes.GunMode>();
    private float[] cooldownTimers;

    private void Start()
    {
        cooldownTimers = new float[] { 0f, explosiveCooldown, iceGunCooldown }; // Initialize cooldown timers
        foreach (GunWithModes.GunMode mode in activeModes)
        {
            selectedIndex = activeModes.IndexOf(mode);
            gunWithModes.SetMode(mode);
            GameObject skillCell = Instantiate(skillCellPrefab, skillPanel.transform);
            skillCell.name = mode.modeName;
            skillCell.GetComponent<Image>().sprite = mode.displayImage != null ? mode.displayImage : initalSprite; 
        }
    }

    private void Update()
    {
        HandleInput();
        HandleCooldowns();
        ResetToDefaultMode();
    }

    private void HandleInput()
    {
        for (int i = 0; i < gunWithModes.gunModes.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && CanSwitchMode(i))
            {
                SwitchMode(i);
                StartCooldown(i);
            }
        }
    }

    private void HandleCooldowns()
    {
        for (int i = 1; i < cooldownTimers.Length; i++)
        {
            if (cooldownTimers[i] > 0)
            {
                cooldownTimers[i] -= Time.deltaTime;
            }
        }
    }

    private void ResetToDefaultMode()
    {
        for (int i = 1; i < cooldownTimers.Length; i++)
        {
            if (cooldownTimers[i] <= 0 && selectedIndex == i)
            {
                SwitchMode(0); // Reset to default mode
                Debug.Log($"Cooldown finished, resetting to default mode.");
            }
        }
    }

    private bool CanSwitchMode(int index)
    {
        return index == 0 || cooldownTimers[index] <= 0;
    }

    private void StartCooldown(int index)
    {
        if (index > 0 && index < cooldownTimers.Length)
        {
            cooldownTimers[index] = index == 1 ? explosiveCooldown : iceGunCooldown;
        }
    }

    private void SwitchMode(int index)
    {
        selectedIndex = index;
        gunWithModes.SetMode(gunWithModes.gunModes[selectedIndex]);
        Debug.Log($"Switched to mode: {gunWithModes.gunModes[selectedIndex].modeName}");
    }

    public void ActivateMode(string modeName)
    {
        foreach (GunWithModes.GunMode mode in gunWithModes.gunModes)
        {
            if (mode.modeName == modeName)
            {
                Debug.Log(mode.modeName);
                if (!activeModes.Contains(mode))
                {
                    activeModes.Add(mode);
                    selectedIndex = activeModes.IndexOf(mode);
                    gunWithModes.SetMode(mode);
                    GameObject skillCell = Instantiate(skillCellPrefab, skillPanel.transform);
                    skillCell.name = modeName;
                    skillCell.GetComponent<Image>().sprite = mode.displayImage != null ? mode.displayImage : initalSprite;
                }
            }
        }
      
        if (activeModes.Count > 0)
        { 
            gunWithModes.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
