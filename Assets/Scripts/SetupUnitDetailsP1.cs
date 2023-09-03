using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupUnitDetailsP1 : MonoBehaviour
{
    public TextMeshProUGUI UnitHPText;
    public bool ShowORHideUI;
    public Slider HPBar;
    public int HP;

    public void SetupUnitUI(CardObject Unit, UnitInfo UInfo)
    {

        HP = UInfo.UnitHealthCurr;
        
        HPBar.maxValue = HP;
        HPBar.value = HP;


        UnitHPText.text = HP.ToString();
    }

    public void ToggleUnitUI()
    {
        this.GetComponent<CanvasGroup>().alpha = 1.0f;
        this.GetComponent<CanvasGroup>().interactable = true;
    }

    private void Update()
    {
        UnitHPText.text = HP.ToString();
        HPBar.value = HP;
    }
}