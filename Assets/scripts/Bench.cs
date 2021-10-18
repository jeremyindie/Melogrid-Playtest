using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{

    [SerializeField]
    private List<SpriteRenderer> _sprites; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TurnOnSprites()
    {
        foreach(SpriteRenderer sprite in _sprites)
        {
            sprite.enabled = true;
        }
    }

    public void TurnOffSprites()
    {
        foreach (SpriteRenderer sprite in _sprites)
        {
            sprite.enabled = false;
        }
    }
}
