using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NarrativeCheckpoint : MonoBehaviour
{

    [SerializeField]
    private int _narrativeIndex;
    [SerializeField]
    private bool _isEndGame = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEndGame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
        }
        NarrativeManager.Instance.SetNarrativePoint(_narrativeIndex);
        PlayerTurnManager.Instance.SetNarrativeReady();
    }

}
