using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerTurnManager : MonoBehaviour
{
    private enum TurnState { CHAR1, GREY, CHAR2 } // CHAR1_NAR, CHAR2_NAR }
    private TurnState _state;
    

    [SerializeField]
    private Player1Controller _char1;
    [SerializeField]
    private Player2Controller _char2;
    [SerializeField]
    private GreyLady _greyLady; 

    [SerializeField]
    private Camera _camera;

    private List<Controller.Directions> _pathList;

    private Controller.Directions[] _dirs;


    private static PlayerTurnManager _instance;

    private bool _nextNarrativeReady;
    private bool _inNarrativeScreen; 

    //public bool ReleaseTheGrey;
    private bool _isFirstTurn;

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

    }
    void Start()
    {
        _state = TurnState.CHAR1;
        _nextNarrativeReady = false;
        _dirs = new Controller.Directions[3];
        _isFirstTurn = true;
        _inNarrativeScreen = false;

    }
    IEnumerator PrepareGrey(float time)
    {

        yield return new WaitForSeconds(time);

        _greyLady.SetPosition(_char2.transform.position + _char2.transform.up);

        _camera.transform.parent = null;
        _camera.transform.position = new Vector3(_greyLady.transform.position.x, _greyLady.transform.position.y, _camera.transform.position.z);
        Quaternion rotation = Quaternion.FromToRotation(_camera.transform.up, -_greyLady.transform.up);
        rotation *= Quaternion.Euler(0, 0, 180);
        
        RandomizeDirections();
        _camera.transform.rotation = rotation;
        _camera.transform.parent = _greyLady.transform;

        SetPathList(_char1.GetMoveList());
        _greyLady.SetActive(true);
        _greyLady.StartTurn();
        Grid.Instance.ResetSpriteAlpha();

        _state = TurnState.GREY;
        

    }
    IEnumerator PrepareChar1(float time)
    {
        yield return new WaitForSeconds(time);
        _camera.transform.parent = null;
        _camera.transform.position = new Vector3(_char1.transform.position.x, _char1.transform.position.y, _camera.transform.position.z);
        Quaternion rotation = Quaternion.FromToRotation(_camera.transform.up, -_char1.transform.up);
        _camera.transform.rotation = rotation;
        _camera.transform.parent = _char1.transform;
        _char1.StartTurn();

        _char1.SetActive(true);
        ClearPathList();
        _state = TurnState.CHAR1;
    }
    IEnumerator PrepareChar2(float time)
    {
        yield return new WaitForSeconds(time);
        _camera.transform.parent = null;
        _camera.transform.position = new Vector3(_char2.transform.position.x, _char2.transform.position.y, _camera.transform.position.z);
        Quaternion rotation = Quaternion.FromToRotation(_camera.transform.up, _char2.transform.up);
        rotation *= Quaternion.Euler(0, 0, 180);

        _camera.transform.rotation = rotation;
        _camera.transform.parent = _char2.transform;

        _char2.SetActive(true);
        _char2.StartTurn();      
        _state = TurnState.CHAR2;
        
    }
    public void ChangeTurn()
    {
        //FindObjectOfType<AudioManager>().Play("background");

        if (_state == TurnState.CHAR1)
        {

            if (!_inNarrativeScreen && _nextNarrativeReady)
            {
                _inNarrativeScreen = true;
                StartCoroutine(Character1NarrativeMode(_char1.GetMovementSpeed()));
                return;
            }

            _inNarrativeScreen = false;
            _char1.SetActive(false);
            _char2.SetActive(false);

            StartCoroutine(PrepareGrey(_char1.GetMovementSpeed()));


        } 
        else if (_state == TurnState.GREY)
        {

            _char1.SetActive(false);
            _greyLady.SetActive(false);

            StartCoroutine(PrepareChar2(_greyLady.GetMovementSpeed()));
        }
        else
        {

            if (!_inNarrativeScreen && _nextNarrativeReady)
            {
                _inNarrativeScreen = true;
                StartCoroutine(Character2NarrativeMode(_char2.GetMovementSpeed()));
                _nextNarrativeReady = false;
                return;
            }
            _inNarrativeScreen = false;


            _greyLady.SetActive(false);
            _char2.SetActive(false);
           
            StartCoroutine(PrepareChar1(_char2.GetMovementSpeed()));
            
            if (_isFirstTurn)
            {
                _isFirstTurn = false;
                UIManager.Instance.EraseInputText();
            }
        }
    }
    public void RandomizeDirections()
    {
        _dirs[0] = Controller.Directions.UP; // "U";
        _dirs[1] = Controller.Directions.LEFT;// "L";
        _dirs[2] = Controller.Directions.RIGHT;// "R";
        for (int i = 0; i < 3; i++)
        {
            Controller.Directions temp = _dirs[i];
            int randomLoc = Mathf.FloorToInt(Random.Range(0, 3));
            _dirs[i] = _dirs[randomLoc];
            _dirs[randomLoc] = temp;
        }
        if (_dirs[0] == Controller.Directions.UP && _dirs[1] == Controller.Directions.LEFT && _dirs[2] == Controller.Directions.RIGHT) RandomizeDirections();
    }
    public Controller.Directions [] GetRandomizedDirections()
    {
        return _dirs;
    }
    public void SetNarrativeReady()
    {
        _nextNarrativeReady = true;
    }

    IEnumerator Character1NarrativeMode(float time)
    {
        _inNarrativeScreen = true;
        yield return new WaitForSeconds(time);
        NarrativeManager.Instance.DisplayNarrativeElement();
        _char1.EnterUIScreen();
        NarrativeManager.Instance.IncrementNarrativePoint();
    }
    IEnumerator Character2NarrativeMode(float time)
    {
        _inNarrativeScreen = true;
        yield return new WaitForSeconds(time);
        _char2.EnterUIScreen();
        NarrativeManager.Instance.DisplayNarrativeElement();
        _nextNarrativeReady = false;
        StartCoroutine(PrepareChar1(_char2.GetMovementSpeed()));
        _camera.transform.rotation *= Quaternion.Euler(0, 0, 180); 
    }

    IEnumerator Character2LOSSScreen(float time)
    {
        Start();


        _inNarrativeScreen = true;
        yield return new WaitForSeconds(time);
        _char2.EnterUIScreen();
        NarrativeManager.Instance.DisplayCustomScreen("Press Space to Restart");
        _nextNarrativeReady = false;
        _camera.transform.rotation *= Quaternion.Euler(0, 0, 180);
    }
    public void SetPathList(List<Controller.Directions> pL)
    {
        _pathList = pL;
    }
    public List<Controller.Directions > GetPathlist()
    {
        return _pathList;
    }
    public void ClearPathList()
    {
        _pathList.Clear();
    }
    public void ShowLossScreen()
    {
        StartCoroutine(Character2LOSSScreen(_char2.GetMovementSpeed()));
    }
    public void OnLossRestart()
    {
        //_char1.transform.position = new Vector3(-0.5f, 0.5f, 0f);
        _state = TurnState.CHAR2;
        //ChangeTurn();
        //StartCoroutine(PrepareChar1(_char2.GetMovementSpeed()));

    }

    public Transform GetPlayerOneTransform()
    {
        return _char1.transform;
    }

    public Transform GetPlayerTwoTransform()
    {
        return _char2.transform;
    }
    public Transform GetPlayerGreyTransform()
    {
        return _greyLady.transform;
    }
    public bool IsPlayerOnesTurn()
    {
        return (_state == TurnState.CHAR1);
    }
    public bool IsPlayerTwosTurn()
    {
        return (_state == TurnState.CHAR2);
    }

    public bool GetIsFirstTurn()
    {
        return _isFirstTurn;
    }

    public bool IsInNarrativeScreen()
    {
        return _inNarrativeScreen; 
    }
     
    public int GetLengthOfMelody()
    {
        return _pathList.Count; 
    }

}
