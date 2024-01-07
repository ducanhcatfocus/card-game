using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapManager : MonoBehaviour
{

    [SerializeField] List<Node> buttonListFloor = new List<Node>();

   [SerializeField] List<Node> partLists = new List<Node>();

 

    [SerializeField] LineRenderer lineRenderer;
    private GameManager GameManager => GameManager.Ins;

    private AudioManager AudioManager => AudioManager.Ins;


    private void Start()
    {
       
        PrepareEncounters();

       // DrawLine();

    }

    private void PrepareEncounters()
    {
        for (int i = 0; i < buttonListFloor.Count; i++)
        {
           var btn = buttonListFloor[i];
           btn.NodeButton.onClick.AddListener(() => OnButtonClick( btn));
            if (GameManager.InitGameplayData.CurrentEncounterId > buttonListFloor.Count)
            {
                GameManager.InitGameplayData.CurrentEncounterId = 0;

            }

            if (GameManager.InitGameplayData.CurrentEncounterId > i)
           {
                btn.EnableNodeCircle();
                partLists.Add(btn);
             
           }
           if (GameManager.InitGameplayData.CurrentEncounterId < i)
           {
                btn.NodeButton.interactable = false;
           }
            
       }
     
    }

    void OnButtonClick( Node currentButton)
    {
        currentButton.NodeButton.interactable = false;
        currentButton.EnableNodeCircle();
        partLists.Add(currentButton);
      //  DrawLine();
        AudioManager.PlayClickSound();
        DetemineDestination(currentButton.NodeType);
    }
    void DrawLine()
    {
        lineRenderer.positionCount = partLists.Count;

        if (partLists.Count > 1)
        {
            for (int i = 0; i < partLists.Count; i++)
            {
             
                Vector3 positionWithZeroZ = new Vector3(partLists[i].transform.position.x, partLists[i].transform.position.y, 0f);
                lineRenderer.SetPosition(i, positionWithZeroZ);
            }
        }
    }

    void DetemineDestination(NodeType nodeType)
    {
        switch (nodeType)
        {
            case NodeType.Monster:
                StartCoroutine(LoadMap(2));
                break;
            case NodeType.Shop:
                UIManager.Ins.OpenUI<Shop>();
                break;
            case NodeType.Treasure:
                break;
        }
    }


    public void OpenShop()
    {
        AudioManager.PlayClickSound();
        UIManager.Ins.OpenUI<Shop>();

    }

    public void RemoveShop()
    {
        AudioManager.PlayClickSound();
        UIManager.Ins.OpenUI<RemoveShop>();
    }


    private IEnumerator LoadMap(int mapIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(mapIndex);
    }
}
