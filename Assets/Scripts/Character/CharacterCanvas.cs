using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvas : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform statusIconRoot;
    [SerializeField] protected TextMeshProUGUI currentHealthText;
    [SerializeField] protected TextMeshProUGUI currentGoldText;
    [SerializeField] Transform dmgPopupPos;
    [SerializeField] protected Slider healthSlider;
    [SerializeField] Transform statusGroup;
    [SerializeField] List<StatusIconData> statusIconDataList;
    protected Canvas TargetCanvas;
    [SerializeField] List<GameObject> iconList;

    protected Dictionary<StatusType, StatusIconBase> StatusDict = new Dictionary<StatusType, StatusIconBase>();


   

    public void UpdateHealthText(int currentHealth, int maxHealth)
    {
        currentHealthText.text = $"{currentHealth}/{maxHealth}";
     
        if (healthSlider == null) return;
        healthSlider.value = (float)currentHealth / maxHealth;
    }


    public void UpdateGoldText(int gold)
    {
        if(currentGoldText == null) return;
        currentGoldText.text = gold.ToString();
    }


    public void ApplyStatus(StatusType targetStatus, int value)
    {
        if (!StatusDict.ContainsKey(targetStatus))
        {
            var targetData = statusIconDataList.Find(item => item.IconStatus == targetStatus);

            if (targetData == null) return;

            var clone = Instantiate(targetData.StatusIconBasePrefab, statusGroup);
            clone.SetStatus(targetData);
            StatusDict.Add(targetStatus, clone);

        }
        StatusDict[targetStatus].SetStatusValue(value, targetStatus);

    }

    public void ClearStatus(StatusType targetStatus)
    {
        if (StatusDict[targetStatus])
        {
            Destroy(StatusDict[targetStatus].gameObject);
        }

        StatusDict.Remove(targetStatus);

    }

    public void UpdateStatusText(StatusType targetStatus, int value)
    {
        if (StatusDict[targetStatus] == null) return;

        StatusDict[targetStatus].StatusValueText.text = $"{value}";
    }

}
