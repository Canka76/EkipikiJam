using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuPages
{
    public GameObject page;
    public bool isOpen;
    public MainMenuPages(GameObject page)
    {
        this.page = page;
        this.isOpen = false;
    }
    
    public void Toggle()
    {
        var width = page.GetComponent<RectTransform>().rect.width;
        if (isOpen)
        {
            page.transform.position += new Vector3(width, 0, 0);
            isOpen = false;
        }
        else
        {
            page.transform.position -= new Vector3(width, 0, 0);
            isOpen = true;
        }
    }
    
    
    
}
