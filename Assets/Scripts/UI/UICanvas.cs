using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{

    public bool IsDestroyOnClose = false;

    protected RectTransform m_RectTransform;
    //[SerializeField] protected Animator m_Animator;

    protected DeckManager DeckManager;
    protected GameManager GameManager;
    protected AudioManager AudioManager;
    protected Canvas canvas;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        m_RectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }


    public virtual void Setup()
    {
        UIManager.Ins.AddBackUI(this);
        UIManager.Ins.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        if(AudioManager == null)
        {
            AudioManager = AudioManager.Ins;
        }
        if (GameManager == null)
        {
            GameManager = GameManager.Ins;
        }
        gameObject.SetActive(true);


    }

    public virtual void Close()
    {
        UIManager.Ins.RemoveBackUI(this);
        AudioManager.PlayClickSound();
        gameObject.SetActive(false);

        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }

    }

}
