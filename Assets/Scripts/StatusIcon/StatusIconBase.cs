using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusIconBase : MonoBehaviour
{
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI statusValueText;

    public StatusIconData MyStatusIconData { get; private set; } = null;

    public Image StatusImage => statusImage;

    public TextMeshProUGUI StatusValueText => statusValueText;

    public void SetStatus(StatusIconData statusIconData)
    {

        MyStatusIconData = statusIconData;
        StatusImage.sprite = statusIconData.IconSprite;

    }

    public void SetStatusValue(int statusValue, StatusType statusType)
    {
        if (statusType == StatusType.Bleed || statusType == StatusType.Poison || statusType == StatusType.Curse)
        {
            statusValueText.color = Color.red;
        }
        StatusValueText.text = statusValue.ToString();
    }
}

