using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField] Button nodeButton;
    [SerializeField] GameObject nodeCircle;
    [SerializeField] RectTransform rectTransform;
    public NodeType nodeType;

    public Button NodeButton => nodeButton;

    public GameObject NodeCircle => nodeCircle; 
    
    public NodeType NodeType => nodeType;

    public void EnableNodeCircle()
    {
        nodeCircle.SetActive(true);
    }
}
