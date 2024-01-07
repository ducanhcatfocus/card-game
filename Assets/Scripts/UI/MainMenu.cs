
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private SaveDataService saveDataService= new SaveDataService();
    [SerializeField] Button continueButton;
    [SerializeField] TextMeshProUGUI continueText;


 

    private void Start()
    {
        CheckContinueButton();
    }

    private void CheckContinueButton()
    {
        if (saveDataService.CheckFileExist())
        {
            continueButton.interactable = true;
            return;
        }   
        continueButton.interactable = false;
        continueText.color = Color.grey;
        continueText.outlineColor = Color.clear;
        
    }

    public void ContinueButton()
    {
        AudioManager.Ins.PlayClickSound();
        SceneManager.LoadScene(1);
    }


    public void Quit()
    {
        Application.Quit();
    }



    public void PlayButton()
    {
        AudioManager.Ins.PlayClickSound();
        saveDataService.DeleteSaveFile();
        SceneManager.LoadScene(1);
    }
  
}
