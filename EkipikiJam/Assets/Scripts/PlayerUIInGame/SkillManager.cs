using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
   public GunWithModes gunWithModes;
   public GameObject skillPanel;
   public GameObject skillCellPrefab;
   public Sprite skillCellSelected;
   
   private int selectedIndex = 0;
   private List<GunWithModes.GunMode> activeModes = new List<GunWithModes.GunMode>();

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.E))
      {
         SwitchMode(1);
      } else if (Input.GetKeyDown(KeyCode.Q))
      {
         SwitchMode(-1);
      }
      foreach (var skillCell in skillPanel.GetComponentsInChildren<Image>())
      {
         if (gunWithModes.gunModes[gunWithModes.currentModeIndex].modeName == skillCell.name) 
            skillCell.GetComponent<Image>().sprite = skillCellSelected;
         else 
            skillCell.GetComponent<Image>().sprite = null;
      }
   }

   public void ActivateMode(string modeName)
   {
      GunWithModes.GunMode mode = gunWithModes.gunModes.Where(gunMode => gunMode.modeName == modeName).FirstOrDefault();
      if (mode != null && !activeModes.Contains(mode))
      {
         activeModes.Add(mode);
         gunWithModes.SetMode(mode);
         GameObject skillCell = Instantiate(skillCellPrefab, skillPanel.transform);
         skillCell.name = modeName;
      }
   }
   
   private void SwitchMode(int direction)
   {
      selectedIndex = (selectedIndex + direction + gunWithModes.gunModes.Length) % gunWithModes.gunModes.Length;
      gunWithModes.SetMode(gunWithModes.gunModes[selectedIndex]);
      Debug.Log($"Switched to mode: {gunWithModes.gunModes[selectedIndex].modeName}");
   }
}
