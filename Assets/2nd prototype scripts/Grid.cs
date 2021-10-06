using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _gridDimention;

    [SerializeField]
    private List<GameObject> _tileList;

    private GameObject[,] _tileArray;
    /// <summary>
    /// /////////////////////////////////////////////////////
    /// Copy paste all of this
    /// Change "SingletonPattern everywhere for the name of your class.
    /// YOU'RE DONE. You can now use SingletonPattern.Instance.WhateverYouLike(); from ANYWHERE
    /// </summary>
    private static Grid s_instance;

    public static Grid Instance
    {
        get
        {
            if (s_instance == null)
            {
                Grid singleton = GameObject.FindObjectOfType<Grid>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<Grid>();
                }
            }
            return s_instance;
        }
    }

    private void Awake()
    {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
        _tileArray = new GameObject[7,7];
    }

    public float GetGridDimention()
    {
        return _gridDimention;
    }


    public void MoveTileArray(Vector3 newPosition)
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject temp = _tileArray[i, j];
                int randI = Mathf.FloorToInt(Random.Range(0, 7));
                int randJ = Mathf.FloorToInt(Random.Range(0, 7));
                _tileArray[i, j] = _tileArray[randI, randJ];
                _tileArray[randI, randJ] = temp;
                _tileArray[i, j].transform.position = newPosition + (i - 3) * _gridDimention * Vector3.right + (j - 3) * _gridDimention * Vector3.up;
            }
        }

    }

    public void CreateTileArray(Vector3 centerPosition)
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                _tileArray[i,j] = Instantiate(_tileList[Mathf.FloorToInt(Random.Range(0, _tileList.Count))]);
                _tileArray[i, j].transform.position = centerPosition + (i - 3) * _gridDimention * Vector3.right + (j - 3) * _gridDimention * Vector3.up;
            }
        }
    }
    public void DarkenSprites(int numberOfWrongMoves)
    {
        numberOfWrongMoves = (numberOfWrongMoves > 4) ? 4 : numberOfWrongMoves;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                SpriteRenderer spriteRenderer = _tileArray[i, j].GetComponent<SpriteRenderer>();
                Color spriteColour = spriteRenderer.color;
                /*float r = spriteColour.r;
                float g = spriteColour.g;
                float b = spriteColour.b; 
                spriteRenderer.color = new Color(r,g,b,(1f - (numberOfWrongMoves / 4f)));*/
                float factor = 1f - (numberOfWrongMoves / 4f);
                spriteRenderer.color = new Color(factor, factor, factor, 1.0f);


            }
        }
    }
    public void ResetSpriteAlpha()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                SpriteRenderer spriteRenderer = _tileArray[i, j].GetComponent<SpriteRenderer>();
                Color spriteColour = spriteRenderer.color;
                float r = spriteColour.r;
                float g = spriteColour.g;
                float b = spriteColour.b;
                spriteRenderer.color = new Color(r, g, b, 1.0f);
            }
        }
    }

}
