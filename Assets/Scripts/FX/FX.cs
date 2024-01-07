using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] protected TextMeshPro textDisplay;
    [SerializeField] protected Sprite icon;


    public void DesTroyFX()
    {
        Destroy(gameObject);
    }
}
