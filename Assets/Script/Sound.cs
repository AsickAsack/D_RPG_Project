using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private static Sound instance=null;

    public static Sound Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Sound>();
                if(instance ==null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SoundManager";
                    instance = obj.AddComponent<Sound>();
                    DontDestroyOnLoad(obj);

                }
            }
            return instance;
        }

    }

   // List<AudioSource> EffectSources = new List<AudioSource>();
    AudioSource _bgmSource = null;

    AudioSource BGMSource
    {
        get
        {
            if (_bgmSource == null)
            {
                _bgmSource = Camera.main.GetComponent<AudioSource>();
            }
            return _bgmSource;
        }
    }

    public void PlayBGM(AudioClip bgm = null, bool loop = true)
    {
        
        BGMSource.clip = bgm;
        BGMSource.loop = loop;
        BGMSource.Play();

    }




}
