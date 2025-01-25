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

   private void Start()
   {
      gunWithModes.SetMode(null);
   }

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
      foreach (var mode in gunWithModes.gunModes)
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
            }
         }
      }
      
      if (activeModes.Count > 0)
      { 
         gunWithModes.GetComponent<MeshRenderer>().enabled = true;
      }
   }
   
   private void SwitchMode(int direction)
   {
      if (activeModes.Count > 0)
      {
         selectedIndex = (selectedIndex + direction + activeModes.Count) % activeModes.Count;
         gunWithModes.SetMode(activeModes[selectedIndex]);
         Debug.Log($"Switched to mode: {activeModes[selectedIndex].modeName}");
      }
   }
}
