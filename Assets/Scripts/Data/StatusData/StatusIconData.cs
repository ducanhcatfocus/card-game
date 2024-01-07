using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status Data", menuName = "Data/Status Data", order = 0)]
public class StatusIconData : ScriptableObject
{
    [SerializeField] private StatusType iconStatus;
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private StatusIconBase statusIconBasePrefab;

    public StatusType IconStatus => iconStatus;
    public Sprite IconSprite => iconSprite;

    public StatusIconBase StatusIconBasePrefab => statusIconBasePrefab;
}
