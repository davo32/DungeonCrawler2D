using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public CanvasGroup PlayMenu;

    public void PlayDungeonButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Debug.Log("Exit Application");
    }

    public void ShowPlayMenu()
    {
        if (PlayMenu.alpha == 1.0f)
        {
            CurtainCallExtension.Extension.HideObject(PlayMenu);
        }
        else
        {
            CurtainCallExtension.Extension.ShowObject(PlayMenu);
        }
    }

}
