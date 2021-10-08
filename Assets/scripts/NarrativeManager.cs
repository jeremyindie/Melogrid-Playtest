using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{

    private  Image _image;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _pressSpaceText;
    [SerializeField]
    public List <string> _forTesting;
    private int _narrativePoint; 


    /// <summary>
    /// /////////////////////////////////////////////////////
    /// Copy paste all of this
    /// Change "SingletonPattern everywhere for the name of your class.
    /// YOU'RE DONE. You can now use SingletonPattern.Instance.WhateverYouLike(); from ANYWHERE
    /// </summary>
    private static NarrativeManager s_instance;

    public static NarrativeManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                NarrativeManager singleton = GameObject.FindObjectOfType<NarrativeManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<NarrativeManager>();
                }
            }
            return s_instance;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
        _image = GetComponent<Image>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _image.enabled = false;
        PopulateNarrativeList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNarrativePoint(int narrativePoint)
    {
        _narrativePoint = narrativePoint;
    }
    public void IncrementNarrativePoint()
    {
        Debug.Log("4");
        _narrativePoint++;
    }
    public void TurnOffNarrativeScreen()
    {
        _image.enabled = false;
        _text.enabled = false;
        _pressSpaceText.enabled = false;
        //PlayerTurnManager.Instance.ReleaseTheGrey = true;
    }

    public void DisplayCustomScreen(string text)
    {
        _text.enabled = true;
        _image.enabled = true;
        _pressSpaceText.enabled = true;

        _text.text = text; 
    }
    public void DisplayNarrativeElement()
    {
        Debug.Log("3");
        _text.enabled = true; 
        _image.enabled = true;
        _pressSpaceText.enabled = true;
        _text.text = _forTesting[_narrativePoint];
    }
    private void PopulateNarrativeList()
    {
        _forTesting.Add("Once there was an adventurer who ventured to a world long forgotten. " +
            "Though the world was forgotten, his deeds lived on as songs echoed through time and travelled afar.  " +
            "\nHis tales inspired many young adventurers to follow in his footsteps, in hopes to one day find the legacy of their hero.");
        _forTesting.Add("This should be the place, thought the awe inspired young adventurer facing a facade of mist and fog. " +
            "The books only indicated the way so far. Amidst the fog, the shape of a lady in grey could be discerned. " +
            "She was singing, singing of the young man who once set foot here, of times long gone, of times that are and of times that could have been. " +
            "\nInviting the young adventurer to follow, shedisappeared in the veil of the mist leaving only her voice as guide. " +
            "There was once a young man with a heart of gold, with a melody in his steps and a world before him. "); 
        _forTesting.Add("He arrived at the foot of a mountain. " +
            "There sat a squadron of knights brooding over a way to lower the guard of the great dragon, terror of all men and sheep. " +
            "Seeing the knights struggle, the young man offered his help which they accepted gladly. " +
            "With this, the young man climbed the mountain to greet the mother dragon, offering her wisdoms and stories of his travels. " +
            "The dragon drifts to sleep, promising in her heart to one day let her child hear of these tales, not knowing that she would never wake to do so. " +
            "The knights thanked the young man for saving the kingdom from the fearsome dragon and gave him his reward in the form of a decorated box and a map. ");
        _forTesting.Add("Little did he know that the majestic beast was the last of her kind.Born from hurricanes of fire, created to rule the skies and to inspire, " +
            "she returns to the winds as a breeze of bittersweet memories.");
        _forTesting.Add("As the young man walked towards the town, he heard cries of pain and laughter. " +
            "A crowd of street boys were gathered around a small hooded figure. " +
            "They threw rocks, kicked, spit and cursed at the cloaked figure. " +
            "Anticipating the worst, the young man took on a deep voice and yield “HALT!” to which the gang panicked and fluttered away from the scene. " +
            "Just as quickly, the hooded figure stood up and disappeared into the shadows. " +
            "All that could be discerned were the turquoise scales on his arm which shone brightly despite his attire. "); 
    }
}
