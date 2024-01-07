
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI drawPileText;
    [SerializeField] private TextMeshProUGUI discardPileText;


    public TextMeshProUGUI DrawPileText => drawPileText;
    public TextMeshProUGUI DiscardPileText => discardPileText;

 
    public void SettingButton()
    {
        GameManager.Ins.AlowClick(false);
        UIManager.Ins.OpenUI<Setting>();
    }

    public void LoseButton()
    {
        GameManager.Ins.AlowClick(false);
        UIManager.Ins.OpenUI<Lose>();
    }

    public void DrawDeckButton()
    {
        GameManager.Ins.AlowClick(false);
        UIManager.Ins.OpenUI<DrawDeck>();
        Close();
    }
    public void DiscardDeckButtton()
    {

        GameManager.Ins.AlowClick(false);
        UIManager.Ins.OpenUI<DiscardDeck>();
        Close();
    }

    public void WinButtton()
    {
        GameManager.Ins.AlowClick(false);
        UIManager.Ins.OpenUI<Win>();
        Close();
    }

    public void SetPileTexts(int drawPileCount, int discardPileCount)
    {
        DrawPileText.text = drawPileCount.ToString();   
        DiscardPileText.text = discardPileCount.ToString();
    }
}
