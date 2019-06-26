using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    //オーディオコントロールスクリプト
    //Original by Brackeys
    //https://www.youtube.com/watch?v=6OT43pvUyfY

    public Sound[] sounds;
    public static AudioManeger instance;
    public bool[] fadeOutFlag;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
        }
    }
    void Start()
    {

    }

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (fadeOutFlag[i])
            {
                FadeOut(i);
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if(s==null)
        {
            Debug.Log("Sound:" + name + " not found!");
            return;
        }
        s.source.Play();
    }

    //自作、音量をコントロールする関数
    //たぶん今流れているオーディオソースをやる方が速いが、やり方がわからない
    public void SetVolume(string name, float value)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound:" + name + " not found!");
            return;
        }

        s.source.volume = value;

    }

    //自作、ピッチをコントロールする関数
    public void SetPitch(string name,float value)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound:" + name + " not found!");
            return;
        }

        s.source.pitch = value;

    }

    //自作、フェードアウトさせる関数
    void FadeOut(int scene)
    {
        float a = sounds[scene].source.volume;

        if(a == 0.0f)
        {
            fadeOutFlag[scene] = false;
        }

        sounds[scene].source.volume = Mathf.Lerp(a,0.0f,Time.deltaTime*2.5f);
    }
}
