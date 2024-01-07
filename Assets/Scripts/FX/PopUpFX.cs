using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpFX : FX
{


    public void Init(string text, bool isNegative)
    {
        if(text == "Blocked!")
        {
            textDisplay.color = Color.blue;
            textDisplay.text = text;
            return;

        }
        if (isNegative)
        {
            textDisplay.color = Color.red;
        }
        textDisplay.text = text;
       
    }

}

