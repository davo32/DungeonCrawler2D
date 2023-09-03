using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetInteraction : MonoBehaviour
{
    public static SetInteraction Instance { private set; get; }

    TextMeshProUGUI InteractionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InteractionText = GetComponent<TextMeshProUGUI>();
    }

    public void ActivateInteractionText(string Text,Color color, float WaitForSeconds)
    {
        StartCoroutine(SetInteractionText(Text,color, WaitForSeconds));
    }

    IEnumerator SetInteractionText(string Text, Color color, float WaitForSeconds)
    {
        InteractionText.color = color;
        InteractionText.text = Text;
        yield return new WaitForSeconds(WaitForSeconds);
        InteractionText.color = Color.black;
        InteractionText.text = "...";
        
    }
}
