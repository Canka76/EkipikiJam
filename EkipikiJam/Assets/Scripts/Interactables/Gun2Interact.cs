using UnityEngine;

public class Gun2Interact : Interactable
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
        Debug.Log("Interacted with Gun2");
        skillManager.ActivateMode("ExplodingBullet");
    }
}
