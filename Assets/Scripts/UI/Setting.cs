using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : UICanvas
{
    [SerializeField] Sprite muteImage;
    [SerializeField] Sprite unmuteImage;
    [SerializeField] Image mute;

    
    public override void Open()
    {
        base.Open();
     
        SetMuteButton();
        AudioManager.PlayClickSound();

    }

    public override void Close()
    {
        base.Close();
        GameManager.Ins.AlowClick(true);

    }

    void SetMuteButton()
    {
      
        if (AudioManager.IsMuted)
        {
            mute.sprite = unmuteImage;
        }
        else
        {
            mute.sprite = muteImage;
        }
        
    }

    public void HomeBtn()
    {
        AudioManager.PlayClickSound();
        SceneManager.LoadScene(0);
    }

    public void MuteButton()
    {
        AudioManager.ToggleMute();
        AudioManager.PlayClickSound();

        if (AudioManager.IsMuted)
        {
            mute.sprite = unmuteImage;
        }
        else
        {
            mute.sprite = muteImage;
        }
    }
}
