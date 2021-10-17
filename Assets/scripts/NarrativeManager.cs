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
        //DontDestroyOnLoad(gameObject);
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
        _text.enabled = true; 
        _image.enabled = true;
        _pressSpaceText.enabled = true;
        _text.text = _forTesting[_narrativePoint];
    }
    private void PopulateNarrativeList()
    {
        _forTesting.Add("He arrived at the foot of a mountain. " +
            "There sat a squadron of knights brooding over a way to lower the guard of the great dragon, terror of all men and sheep. " +
            "Seeing the knights struggle, the young man offered his help which they accepted gladly. " +
            "With this, the young man climbed the mountain to greet the mother dragon, offering her wisdoms and stories of his travels. " +
            "The dragon drifts to sleep, promising in her heart to one day let her child hear of these tales, not knowing that she would never wake to do so. " +
            "The knights thanked the young man for saving the kingdom from the fearsome dragon and gave him his reward in the form of a decorated box and a map. ");
        _forTesting.Add("Little did he know that the majestic beast was the last of her kind.Born from hurricanes of fire, created to rule the skies and to inspire, " +
            "she returns to the winds as a breeze of bittersweet memories.");
        _forTesting.Add("After descending the mountain, the young man strolled towards the sound of the rivers and woods.  There stood a man plagued with incessant worries. " +
            " The eldest prince was in love with a girl that loved him back. He intends to ask for her hand in marriage, but he could not find the words.  " +
            "Seeing the prince struggle, the young man gave him the box he found earned from the dragon’s cave. When the lid was lifted, beautiful music from another world played." +
            " They both knew, it was the perfect gift to start the Conversation.");
        _forTesting.Add("Like many fires, fires of the heart can keep warm, but they can also burn. Some seek to control it, some let it turn into a forest fire and others try to stamp it out no matter the cost.");
        _forTesting.Add("The path which the young man chose after descending the mountain led him towards the villages. Before entering the village, he was stopped by a man " +
            "in well decorated clothes. He was curious of the traveller and his reasons to visit such a small village, so he invited the young man to a drink. " +
            "They shared their travel stories and past experiences until the sky turned pink.  Looking at the rise of tomorrow, man said to the young man: " +
            "\"You know what I see ? The same day as yesterday.A day where the farmers slave away their lives for land that isn't theirs.\" The young man looked and answered: \"" +
            "I see a beautiful sunrise. If everyone were to do their  part, the world might just be a better place.\" With those  words, he left and continued his journey.");
        _forTesting.Add("Inspired by the sunrise and the young man's words, the man  goes his own way. Maybe he'd right a book on the topics that emerged from last night's conversations.");
    }

}
