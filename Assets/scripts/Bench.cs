using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{

    [SerializeField]
    private List<SpriteRenderer> _sprites;

    [SerializeField]
    private Sprite _grave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToGraves()
    {
        foreach (SpriteRenderer sprite in _sprites)
        {
            sprite.sprite = _grave;
        }
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
