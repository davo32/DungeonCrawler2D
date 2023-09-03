using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Unit_BattlePanel : MonoBehaviour
{
    public GameObject Indicator;

    public enum Status {Idle,Defending,Attacked };
    public Status UnitStatus = Status.Idle;

    public IEnumerator ShowStatus(float x)
    {
        Indicator.GetComponent<CanvasGroup>().alpha = 1.0f;

        yield return new WaitForSeconds(x);
        UnitStatus = Status.Idle;
        Indicator.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    
}
