using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerUIClass : MonoBehaviour
{
    public TextMeshProUGUI workerTypeText;
    public TextMeshProUGUI workerBonusText;
    public TextMeshProUGUI workerSpeedText;
    public TextMeshProUGUI workerCostText;
    [SerializeField] private GameObject[] tierStars;

    public void SetUpTier(int tierNumber)
    {
        for (int i = 0; i < tierStars.Length; i++)
            tierStars[i].SetActive(false);
            
        tierStars[tierNumber].SetActive(true);
    }
}
