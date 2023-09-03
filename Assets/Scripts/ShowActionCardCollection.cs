using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowActionCardCollection : MonoBehaviour
{
    public GameObject Menu;
    public bool ShowMenu;

    public void OnClick()
    {
        ShowMenu = !ShowMenu;

        if (ShowMenu == true)
        {
            Menu.SetActive(true);
        }
        else
        {
            Menu.SetActive(false);
        }
    }
}
