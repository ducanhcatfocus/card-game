using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lose : UICanvas
{
    [SerializeField] private UICardBase choiceCardUIPrefab;
    private SaveDataService saveDataService = new SaveDataService();
    [SerializeField] Image panelImage;
    [SerializeField] TextMeshProUGUI totalCollectedCardText;



    public override void Open()
    {
        base.Open();
        GameManager.ResetData();
        saveDataService.DeleteSaveFile();
        BuildReward();
    }

    private void BuildReward()
    { 
       
        totalCollectedCardText.text = "Total card collected: " + GameManager.InitGameplayData.CurrentCardsList.Count;
    }


    public void HomeButtton()
    {
        SceneManager.LoadScene(0);
    }


  
  
}

