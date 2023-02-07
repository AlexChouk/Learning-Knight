using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] soundtrack;
    public Slider volumeSlider;
    
    public GameObject audioObject;

    AudioSource audioSource;
    private float volume = 0.4f;

    public float audioVolume {
        get
        {
            return PlayerPrefs.GetFloat(".audioVolume");
        }
        set
        {
            PlayerPrefs.SetFloat(".audioVolume", volume);
        }
    }
    
    void Awake()
    {
        DontDestroyOnLoad(audioObject.gameObject);
    }

    void Start()
    {
    	
        audioSource = audioObject.GetComponent<AudioSource>();

        if (!audioSource.playOnAwake)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }
    }

    void OnEnable()
    {
        //Register Slider Events
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }

    //Called when Slider is moved
    void changeVolume(float sliderValue)
    {
    	volume = sliderValue;
        audioSource.volume = sliderValue;
    }

    void OnDisable()
    {
        //Un-Register Slider Events
        volumeSlider.onValueChanged.RemoveAllListeners();
    }
}
