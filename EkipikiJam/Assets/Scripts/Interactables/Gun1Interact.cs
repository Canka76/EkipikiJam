using UnityEngine;

public class Gun1Interact : Interactable
{
    public SkillManager skillManager;
    
    public override void OnFocus()
    {
    }

    public override void OnLoseFocus()
    {
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with Gun1");
        skillManager.ActivateMode("BasicBullet");
    }
}
