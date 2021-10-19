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

        //0
        _forTesting.Add("He arrived at the foot of a mountain. " +
            "There sat a squadron of knights brooding over a way to lower the guard of the great dragon, terror of all men and sheep. " +
            "Seeing the knights struggle, the young man offered his help which they accepted gladly. " +
            "With this, the young man climbed the mountain to greet the mother dragon, offering her wisdoms and stories of his travels. " +
            "The dragon drifts to sleep, promising in her heart to one day let her child hear of these tales, not knowing that she would never wake to do so. " +
            "The knights thanked the young man for saving the kingdom from the fearsome dragon and gave him his reward in the form of a decorated box and a map. ");

        //1
        _forTesting.Add("Little did he know that the majestic beast was the last of her kind.Born from hurricanes of fire, created to rule the skies and to inspire, " +
            "she returns to the winds as a breeze of bittersweet memories.");

        //2
        _forTesting.Add(" As the young man walked towards the town, he heard"
            + "cries of pain and laughter. A crowd of street boys"
            + "were gathered around a small hooded figure."
            + "They threw rocks, kicked, spit and cursed at the cloaked figure."
            + "\nAnticipating the worst, the young man took on a deep"
            + "voice and yield “HALT!” to which the gang panicked"
            + "and fluttered away from the scene."
            + "Just as quickly, the hooded figure stood up and"
            + "disappeared into the shadows. All that could be"
            + "discerned were the turquoise scales on his arm which"
            + "shone brightly despite his attire."

            );
        //3
        _forTesting.Add("         Firestorm"
             + "Upon returning to his dwelling, the young"
             + "half - dragon found nothing. He knew. He descended."
             + "He ravaged. He burnt. All turned to dust."
             );

        //4 
        _forTesting.Add("As his path turned towards the farmlands and villages,"
             + "the young man came across a drunk bard hanging"
             + "upside down off a tree. He was a plump man, red to the"
             + "ears, with a contagious laugh.The bard offered the"
             + "young man a performance in exchange for fixing his lyre."
             + "\nOnce the lyre fixed, the plump man started to sing"
             + "about nonsense."
             + "He sang of a lady in gray, forever changing."
             + "He sang of blindness, that of love and hate."
             + "His last song was that of freedom and the pursuit of happiness"
             );


        //5

        _forTesting.Add("Upon returning to his mountain, the young"
             + "half - dragon found nothing. He knew. Tears he"
             + "had for his mother have all but evaporated."
             + "The howl of a wounded animal, a crimson night sky,"
             + "a tremor in the earth.He marched towards the town and"
             + "the castle.\n"
             + "Through the roaring fire, a soft melody could be"
             + "discerned.The music spoke of the union between an"
             + "astrologist and a creature of the sky.They cherished"
             + "each other but the lifetime of a man did not allow forevers."
             + "He left her a child and all his riches."
             + "Now that the wind has taken her as well, they are reunited.\n"
             + "The night grew dark again.Tears rolled down once more."
             + "Silence.A plump man leaves a bouquet of wild flowers"
             + "by the Cave entrance as he finishes the elegy."
             + "He sits beside the exhausted young dragon,"
             + "waiting for sunrise to promise another day."
     );
        

        //6


        _forTesting.Add("After descending the mountain, the young man strolled towards the sound of the rivers and woods.  There stood a man plagued with incessant worries. " +
            " The eldest prince was in love with a girl that loved him back. He intends to ask for her hand in marriage, but he could not find the words.  " +
            "Seeing the prince struggle, the young man gave him the box he found earned from the dragon’s cave. When the lid was lifted, beautiful music from another world played." +
            " They both knew, it was the perfect gift to start the Conversation.");
        //7
        _forTesting.Add("Like many fires, fires of the heart can keep warm, but they can also burn. Some seek to control it, some let it turn into a forest fire and others try to stamp it out no matter the cost.");

        //8
        _forTesting.Add("As the young man steered near the river banks,"
     + "he noticed a young woman looking up at the skies,"
     + "looking for answers.\n"
     + "If there is a world beyond these borders, what"
     + "might that look like ? Are there elves ?"
     + "Do they eat like us ? Are they happier than us ?\n"
     + "The young man answered her questions by handing"
     + "her the map that was found in the mountains."
     );

        //9


        _forTesting.Add("She was both his fortune and missfortune.The love they"
+ "shared was real, but in the darkest corner of her heart,"
+ "she seeked adventure and knowledge.\n"
+ "She would exchange stability for answers only to return"
+ "ill.The prince now king sacrificed everything.Now there"
+ "is nothing."

);



        //10

        _forTesting.Add("As the young man walks closer to the castle, he encounters"
+ "a worried old man. The old man introduced himself as the"
+ "royal counciler and was looking for the prince for urgent"
+ "matters.\n"
+ "The young man pointed him in the direction from which he"
+ "came and continued forward."

);



        //11
        _forTesting.Add(" If onlys and what ifs, universal phrases.If only the young"
+ "man hadn't given the old man directions, the prince would"
+ "have lived.If only the eldest prince hadn't died, his"
+ "brother wouldn't have ascended to the throne. If only the"
+ "eldest prince hadn't died, his beloved wouldn't be mournful"
+ "until her own time.\n"
+ "What if the eldest prince had lived? Would the people of the"
+ "kingdom have been as happy as they were under the second"
+ "prince's rule? What if indeed."
);
       



            //12


        _forTesting.Add("The path which the young man chose after descending the mountain led him towards the villages. Before entering the village, he was stopped by a man " +
            "in well decorated clothes. He was curious of the traveller and his reasons to visit such a small village, so he invited the young man to a drink. " +
            "They shared their travel stories and past experiences until the sky turned pink.  Looking at the rise of tomorrow, man said to the young man: " +
            "\"You know what I see ? The same day as yesterday.A day where the farmers slave away their lives for land that isn't theirs.\" The young man looked and answered: \"" +
            "I see a beautiful sunrise. If everyone were to do their  part, the world might just be a better place.\" With those  words, he left and continued his journey.");
        //13
        _forTesting.Add("Inspired by the sunrise and the young man's words, the man  goes his own way. Maybe he'd right a book on the topics that emerged from last night's conversations.");


        //14
        _forTesting.Add("A little hung over, the young man crossed path to a poorer" +
                   "area of the village.On the side of the street, a young boy" +
                   "who was drawing beautiful murals came begging for anything" +
                   "that could help his situation.\n" +
                   "To this, the young man gave him the treasures he aquired from" +
                   "the dragon's den. Sell them and the riches could get him" +
                   "out of his situation.The boy has talent."


                   );



        //15
        _forTesting.Add("Enlightened by the conversations, the man wrote a most" +
           "inspiring book of the dream.The words of his book" +
           "resonated with many people, including the boy who lost his" +
           "hand.He was mistaken for a thief when he tried to sell" +
           "off the treasures he had acquired.\n" +
           "Through the words, the world was born anew.Not better, just" +
           "anew.A world in which dreams suffocate, strangle and kill."


           );












        //16


        _forTesting.Add("Continuing his path towards the center of town," +
                   "the young man came across a drunk bard hanging" +
                   "upside down off a tree.He was a plump man, red to the" +
                   "ears, with a contagious laugh.The bard offered the" +
                   "young man a performance in exchange for fixing his lyre.\n" +
                   "Once the lyre fixed, the plump man started to sing" +
                   "about nonsense.\n" +
                   "He sang of a lady in gray, forever changing." +
                   "He sang of blindness, that of love and hate." +
                   "His last song was that of freedom and the pursuit of" +
                   "happiness." 

                   );



                    //17


                    _forTesting.Add(" Enjoying the next bottle of wine, the plump bard found himself" +
                   "a drinking companion.They walked and wandered the world." +
                   "They watched it change under the dominion of the grey lady." +
                   "They watched as she drew lines on their faces until they fell" +
                   "into deep sleep forever." 


                   );
       
 






    }

}
