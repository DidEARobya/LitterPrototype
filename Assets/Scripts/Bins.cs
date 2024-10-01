using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bins : MonoBehaviour
{
    int litterCount;

    [SerializeField]
    public TextMeshProUGUI litterCounterText;
    public void BinLitter(int amount)
    {
        litterCount += amount;
        
        litterCounterText.text =  "Binned: " + litterCount.ToString();
    }
}
