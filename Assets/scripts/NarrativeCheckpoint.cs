using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeCheckpoint : MonoBehaviour
{

    [SerializeField]
    private int _narrativeIndex; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        NarrativeManager.Instance.SetNarrativePoint(_narrativeIndex);
        PlayerTurnManager.Instance.SetNarrativeReady();
    }
 
}
