using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    public enum TurnState { CHAR1, CHAR2 }
    public TurnState state;

    public Player1Controller char1;
    public Player2Controller char2;

    public Camera camera;

    public List<string> pathList;

    private static PlayerTurnManager _instance;

    private bool _nextNarrativeReady; 
    
    public static PlayerTurnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                PlayerTurnManager singleton = GameObject.FindObjectOfType<PlayerTurnManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<PlayerTurnManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _nextNarrativeReady = false;
    }
    void Start()
    {
        state = TurnState.CHAR1;
    }
    public void ChangeTurn()
    {
        //FindObjectOfType<AudioManager>().Play("background");

        Debug.Log("ChaningTurn");
        if (state == TurnState.CHAR1)
        {
            camera.transform.parent = null;
            camera.transform.position = new Vector3(char2.transform.position.x, char2.transform.position.y, camera.transform.position.z);
            camera.transform.parent = char2.transform;
            
            char1.SetActive(false);
            char2.SetActive(true);
            char2.StartTurn();
            Grid.Instance.ResetSpriteAlpha();
            SetPathList(char1.GetMoveList());
            state = TurnState.CHAR2;
        }
        else
        {
            camera.transform.parent = null;
            camera.transform.position = new Vector3(char1.transform.position.x, char1.transform.position.y, camera.transform.position.z);
            camera.transform.parent = char1.transform;
            char1.StartTurn();
            char2.SetActive(false);
            char1.SetActive(true);
            ClearPathList();
            state = TurnState.CHAR1;
            if(_nextNarrativeReady)
            {
                NarrativeManager.Instance.DisplayNarrativeElement();
                _nextNarrativeReady = false;
                char1.EnterUIScreen();
            }

        }
    }
    public void SetNarrativeReady()
    {
        _nextNarrativeReady = true; 
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
