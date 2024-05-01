using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr
{
    public GameObject soundOBJ = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    private float soundValue = 1;

    /// <summary>
    /// 音效大小(0-1)
    /// </summary>
    public float SoundValue
    {
        set
        {
            soundValue = value;
            foreach (var source in soundList)
            {
                source.volume = value;
            }
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public AudioSource PlaySound(string name, bool isLoop = false)
    {
        if (soundOBJ == null)
        {
            soundOBJ = new GameObject("soundOBJ");
            soundOBJ.transform.position = Camera.main.transform.position;
        }

        AudioSource source = soundOBJ.AddComponent<AudioSource>();

        AudioClip audioClip = GameObject.Instantiate(
            Resources.Load<AudioClip>("Music/AudioEffect/" + name)
        );
        source.clip = audioClip;
        soundList.Add(source);
        source.volume = soundValue;
        source.loop = isLoop;
        // yield return audioClip;
        source.Play();
        return source;
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            source.Stop();
            soundList.Remove(source);
            GameObject.Destroy(source);
        }
    }

    public AudioSource bkMusic = null;
    private float bkValue = 1;
    public float BkValue
    {
        set
        {
            bkValue = value;
            bkMusic.volume = bkValue;
        }
    }

    private static MusicMgr instance;
    public static MusicMgr Instance
    {
        get
        {
            instance ??= new MusicMgr();
            instance.Check();
            return instance;
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBkMusic(string name)
    {
        if (bkMusic == null)
        {
            bkMusic = new GameObject("BkMusic").AddComponent<AudioSource>();
            bkMusic.transform.position = Camera.main.transform.position;
        }
        AudioClip audioClip = GameObject.Instantiate(
            Resources.Load<AudioClip>("Music/Music/" + name)
        );
        bkMusic.clip = audioClip;
        bkMusic.volume = bkValue;
        bkMusic.loop = true;
        // yield return audioClip;
        bkMusic.Play();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Stop();
    }

    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseBkMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Pause();
    }

    public void Check()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (soundList[i] && !soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
}
