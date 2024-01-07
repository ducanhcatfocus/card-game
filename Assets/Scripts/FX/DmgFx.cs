using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgFx : FX
{
    public void Init(int dmg)
    {
        textDisplay.text = "-" + dmg;
    }
}
