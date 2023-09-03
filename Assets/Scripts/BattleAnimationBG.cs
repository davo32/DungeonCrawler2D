using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationBG : MonoBehaviour
{
    public List<GameObject> TilesToEnable = new List<GameObject>();
    public GameObject UI;
    public float delay = 2f;

    private void Start()
    {
        StartCoroutine(RunAnimation());
    }

    public IEnumerator RunAnimation()
    {
        if (UI.activeInHierarchy)
        {
            TilesToEnable.Reverse();

            for (int i = 0; i < TilesToEnable.Count; i++)
            {
                TilesToEnable[i].SetActive(true);
                yield return new WaitForSeconds(delay);
            }
            UI.SetActive(false);
        }
        else
        {
            for (int i = 0; i < TilesToEnable.Count; i++)
            {
                TilesToEnable[i].SetActive(true);
                yield return new WaitForSeconds(delay);
            }
            UI.SetActive(true);
        }
    }
}
