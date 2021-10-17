using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField]
    private Sounder _leftSound;
    [SerializeField]
    private Sounder _upSound;
    [SerializeField]
    private Sounder _rightSound;
    [SerializeField]
    private Sounder _wrongSound;
    private List<Sounder> _sounds;

    // Start is called before the first frame update
    void Start()
    {
        _sounds = new List<Sounder>()
        {
            _leftSound,
            _upSound,
            _rightSound,
            _wrongSound
        };

        foreach (Sounder s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;



        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Sounder> GetSounds()
    {
        return _sounds; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.SetNewInstrument(this);
    }

    public Sounder GetUpSound()
    {
        return _upSound;
    }

    public Sounder GetWrongSound()
    {
        return _wrongSound;
    }

    public Sounder GetLeftSound()
    {
        return _leftSound;
    }

    public Sounder GetRightSound()
    {
        return _rightSound;
    }
}
