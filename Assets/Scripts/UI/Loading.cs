using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tipText;
    [SerializeField] List<string> tipList;
    
    
    void Start()
    {
        tipText.text = tipList.GetRandomListItem();
        StartCoroutine(HideLoadingPanel());
    }

   
    IEnumerator HideLoadingPanel()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
