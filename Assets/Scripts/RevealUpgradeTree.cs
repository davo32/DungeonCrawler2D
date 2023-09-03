using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealUpgradeTree : MonoBehaviour
{
    public CanvasGroup UpgradeTree;





    public void Show()
    {
        UpgradeTree.alpha = 1.0f;
        UpgradeTree.interactable = true;
        UpgradeTree.blocksRaycasts = true;
    }

    public void Hide()
    {
        UpgradeTree.alpha = 0.0f;
        UpgradeTree.interactable = false;
        UpgradeTree.blocksRaycasts = false;
    }
}
