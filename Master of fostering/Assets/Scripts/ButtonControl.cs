using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : Button
{
    Image image;

    protected override void Start()
    {
        image = GetComponent<Image>();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        switch(state)
        {
            case SelectionState.Normal:
                image.sprite = Resources.Load("Image/Sprites/StartNewGame", typeof(Sprite)) as Sprite;
                //normal
                break;
            case SelectionState.Highlighted:
                image.sprite = Resources.Load("Image/Sprites/StartNewGame__", typeof(Sprite)) as Sprite; 
                //highlight
                break;

        }
    }
}
