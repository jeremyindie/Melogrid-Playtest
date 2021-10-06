using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _gridDimention;

    [SerializeField]
    private List<GameObject> _tileList;

    [SerializeField]
    private int  _darkSideTileMapDimention = 3; 
    

    private GameObject[,] _tileArray;

    private float _currentAlphaValue; 

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
        _tileArray = new GameObject[_darkSideTileMapDimention, _darkSideTileMapDimention];
    }

    public float GetGridDimention()
    {
        return _gridDimention;
    }

    public void ShiftTilesDown()
    {

    }
    public void ShiftTilesLeft()
    {

    }

    public void ShiftTilesRight()
    {

    }
    public void MoveTileArray(Vector3 newPosition, Vector2 moveDelta)
    {
        newPosition -= new Vector3(0.5f, 0.5f, 0f);
        //moving to the right
        if(moveDelta.x > 0)
        {
            for (int i = _darkSideTileMapDimention - 1; i >= 0; i--)
            {
                for (int j = _darkSideTileMapDimention - 1; j >= 0 ; j--)
                {
                    if (j == 0 || i == 0)
                    {
                        _tileArray[i, j] = Instantiate(_tileList[Mathf.FloorToInt(Random.Range(0, _tileList.Count))]);
                        SetSpriteToCurrentAlpha(_tileArray[i, j]);
                    }
                    else
                    {
                        if (j == _darkSideTileMapDimention - 1 || i == _darkSideTileMapDimention - 1)
                        {
                            Destroy(_tileArray[i, j]);
                        }
                        _tileArray[i, j] = _tileArray[i - 1, j - 1];
                    }
                    _tileArray[i, j].transform.position = newPosition + (i - (_darkSideTileMapDimention - 1) / 2 + 1) * _gridDimention * Vector3.right + (j - (_darkSideTileMapDimention - 1) / 2 + 1) * _gridDimention * Vector3.up;
                }
            }

        } else
        {
            for (int i = 0; i < _darkSideTileMapDimention; i++)
            {
                for (int j = _darkSideTileMapDimention - 1; j >= 0; j--)
                {
                    if (j == 0 || ((moveDelta.x < 0) && (i == _darkSideTileMapDimention - 1))) {
                        _tileArray[i, j] = Instantiate(_tileList[Mathf.FloorToInt(Random.Range(0, _tileList.Count))]);
                        SetSpriteToCurrentAlpha(_tileArray[i, j]);
                    } else
                    {
                        if (j == _darkSideTileMapDimention - 1 || ((moveDelta.x < 0) && (i == 0)))
                        {
                            Destroy(_tileArray[i, j]);
                        }
                        if ((moveDelta.x < 0))
                        {
                            _tileArray[i, j] = _tileArray[i + 1, j - 1];
                        }
                        else
                        {
                            _tileArray[i, j] = _tileArray[i, j - 1];
                        }
                    }

                    _tileArray[i, j].transform.position = newPosition + (i - (_darkSideTileMapDimention - 1) / 2) * _gridDimention * Vector3.right + (j - (_darkSideTileMapDimention - 1) / 2) * _gridDimention * Vector3.up;
                }
            }

        }
        

    }


    public void CreateTileArray(Vector3 centerPosition)
    {
        centerPosition -= new Vector3(0.5f, 0.5f, 0f);
        for (int i = 0; i < _darkSideTileMapDimention; i++)
        {
            for (int j = 0; j < _darkSideTileMapDimention; j++)
            {
                _tileArray[i,j] = Instantiate(_tileList[Mathf.FloorToInt(Random.Range(0, _tileList.Count))]);
                _tileArray[i, j].transform.position = centerPosition + (i - (_darkSideTileMapDimention - 1) / 2) * _gridDimention * Vector3.right + (j - (_darkSideTileMapDimention - 1) / 2) * _gridDimention * Vector3.up;
            }
        }
    }

    public void SetSpriteToCurrentAlpha(GameObject tile)
    {
        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(_currentAlphaValue, _currentAlphaValue, _currentAlphaValue, 1.0f);

    }
    public void DarkenSprites(int numberOfWrongMoves)
    {
        numberOfWrongMoves = (numberOfWrongMoves > 4) ? 4 : numberOfWrongMoves;
        for (int i = 0; i < _darkSideTileMapDimention; i++)
        {
            for (int j = 0; j < _darkSideTileMapDimention; j++)
            {
                SpriteRenderer spriteRenderer = _tileArray[i, j].GetComponent<SpriteRenderer>();
                Color spriteColour = spriteRenderer.color;
                /*float r = spriteColour.r;
                float g = spriteColour.g;
                float b = spriteColour.b; 
                spriteRenderer.color = new Color(r,g,b,(1f - (numberOfWrongMoves / 4f)));*/
                float factor = 1f - (numberOfWrongMoves / 4f);
                _currentAlphaValue = factor;
                spriteRenderer.color = new Color(factor, factor, factor, 1.0f);


            }
        }
    }
    public void ResetSpriteAlpha()
    {
        _currentAlphaValue = 1.0f; 
        for (int i = 0; i < _darkSideTileMapDimention; i++)
        {
            for (int j = 0; j < _darkSideTileMapDimention; j++)
            {
                SpriteRenderer spriteRenderer = _tileArray[i, j].GetComponent<SpriteRenderer>();
                Color spriteColour = spriteRenderer.color;
                float r = 1.0f;
                float g = 1.0f;
                float b = 1.0f;
                spriteRenderer.color = new Color(r, g, b, 1.0f);
            }
        }
    }

}
