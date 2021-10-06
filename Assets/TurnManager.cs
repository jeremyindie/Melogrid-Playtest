using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnState { CHAR1, CHAR2 }
    public TurnState state;

    public GameObject char1;
    private CharacterController cc1;
    public Transform char1SpritePos;
    public GameObject char2;
    private Char2Controller cc2;

    public List<string> pathList;

    private static TurnManager _instance;
    
    public static TurnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                TurnManager singleton = GameObject.FindObjectOfType<TurnManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<TurnManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        state = TurnState.CHAR1;
        cc1 = char1.GetComponent<CharacterController>();
        cc2 = char2.GetComponent<Char2Controller>();
    }
    public void ChangeTurn()
    {
        FindObjectOfType<AudioManager>().Play("background");
        

        if (state == TurnState.CHAR1)
        {
            cc1.isActive = false;
            cc2.isActive = true;
            cc2.moves = 0;
            char2.GetComponent<CircleCollider2D>().enabled = true;
            char1SpritePos.position = char1.GetComponent<Transform>().position;
            SetPathList(cc1.moveList);
            state = TurnState.CHAR2;
        }
        else
        {
            cc2.isActive = false;
            cc1.isActive = true;
            char2.GetComponent<CircleCollider2D>().enabled = false;
            ClearPathList();
            state = TurnState.CHAR1;
        }
    }
    public void SetPathList(List<string> pL)
    {
        pathList = pL;
    }
    public void ClearPathList()
    {
        pathList.Clear();
    }

}
