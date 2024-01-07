using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ListExtension
{
    public static T GetRandomListItem<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new IndexOutOfRangeException("List is Empty");

        var randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
 

