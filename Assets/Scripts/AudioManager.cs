using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;


    void Awake()
    {
        if (instance!=null)
        {
            Debug.Log("More than one Audiomanager in the scene");
        }
        else
        {
            instance = this;
        }
        
    }


    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }
}
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public float volume = 0.7f;
    public float pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;

    }

    // Update is called once per frame
    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

    }

}