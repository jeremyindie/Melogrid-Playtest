using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidBackground : MonoBehaviour
{
    public Sprite[] frames;
    private float Timer;
    private int CurrentFrame;
    public SpriteRenderer BackgroundRenderer;

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= 0.04f)
        {
            CurrentFrame += 1;
            if(CurrentFrame >= frames.Length)
            {
                CurrentFrame = 0;
            }
            Timer = 0f;
        }

        BackgroundRenderer.sprite = frames[CurrentFrame];

    }

}
