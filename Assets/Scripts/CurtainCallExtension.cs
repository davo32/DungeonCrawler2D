using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainCallExtension : MonoBehaviour
{
    //To use Curtain Call 
    //Call CurtainCallExtension.Extension

    //To Show
    //Call CurtainCallExtension.Extension.ShowObject(CanvasGroup);

    //To Hide
    //Call CurtainCallExtension.Extension.HideObject(CanvasGroup);

    public static CurtainCallExtension Extension { private set; get; }

    private void Awake()
    {
        if (Extension != null && Extension != this)
        {
            Destroy(this);
        }
        else
        {
            Extension = this;
        }
    }

    public void ShowObject(CanvasGroup CG)
    {
        CG.alpha = 1.0f;
        CG.interactable = true;
        CG.blocksRaycasts = true;
    }

    public void HideObject(CanvasGroup CG)
    {
        CG.alpha = 0.0f;
        CG.interactable = false;
        CG.blocksRaycasts = false;
    }
}
