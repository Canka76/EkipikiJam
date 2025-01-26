using UnityEngine;

public class Gun3Interact : Interactable
{
    public SkillManagerRefactor skillManager;
    
    public override void OnFocus()
    {
    }

    public override void OnLoseFocus()
    {
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with Gun3");
        skillManager.ActivateMode("Ice");
    }
}
