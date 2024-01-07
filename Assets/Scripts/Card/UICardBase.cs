using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardBase : CardBase
{
    [SerializeField] Button rewardButton;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] GameObject soldOutPanel;

    int cost;


    public Button RewardButton => rewardButton;

    public int Cost => cost;

    public TextMeshProUGUI CostText => costText;


   public void SetCost()
   {
        cost = Random.Range(75, 300);
        CostText.text = cost.ToString();
   }

    public void SetFreeCost()
    {
 
        CostText.text = "Free";
    }

    public void OpenSoldOutPanel() {
        soldOutPanel.SetActive(true);
    }
}
