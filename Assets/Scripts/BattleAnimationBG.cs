using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationBG : MonoBehaviour
{
    public List<GameObject> tilesToEnable = new List<GameObject>();
    public GameObject UI;
    public float delay = 1f;
    public bool Reverse = true;
    private void Start()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(RunAnimation(Reverse));
        
    }

    public IEnumerator RunAnimation(bool reverse)
    {
        if (!reverse)
        {
            tilesToEnable.Reverse();
            
            for (int i = 0; i < tilesToEnable.Count; i++)
            {
                tilesToEnable[i].SetActive(false);
                yield return new WaitForSeconds(delay);
            }
            
            UI.SetActive(false);
        }
        else
        {
            for (int i = 0; i < tilesToEnable.Count; i++)
            {
                tilesToEnable[i].SetActive(true);
                yield return new WaitForSeconds(delay);
            }
            UI.SetActive(true);
        }
        GetComponent<AudioSource>().Stop();
    }
    

    private void OnDisable()
    {
        for (int i = 0; i < tilesToEnable.Count; i++)
        {
            tilesToEnable[i].SetActive(false);
        }
    }
}
