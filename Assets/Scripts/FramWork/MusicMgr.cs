using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr
{
    public GameObject soundOBJ = null;
    private readonly List<AudioSource> soundList = new();
    private readonly Dictionary<string, AudioClip> soundDict = new();
    /// <summary>按逻辑音效名（不含随机后缀）记录上次播放时间，用于割草场景限流。</summary>
    private readonly Dictionary<string, float> lastPlayTimeByName = new();
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
                if (source)
                {
                    source.volume = value;
                }
            }
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">逻辑音效名（冷却字典 key，与随机变体无关）</param>
    /// <param name="cooldown">同 name 最短播放间隔（秒），默认 0.016</param>
    public AudioSource PlaySound(
        string name,
        bool isLoop = false,
        int i = 0,
        float cooldown = 0.016f
    )
    {
        string cooldownKey = name;
        if (
            cooldown > 0f
            && lastPlayTimeByName.TryGetValue(cooldownKey, out float lastTime)
            && Time.time - lastTime < cooldown
        )
        {
            return null;
        }

        if (soundOBJ == null)
        {
            soundOBJ = new GameObject("soundOBJ");
            soundOBJ.transform.position = Camera.main.transform.position;
        }

        AudioSource source = soundOBJ.AddComponent<AudioSource>();
        AudioClip audioClip;
        string resPath = "Sounds/" + name;
        name = resPath;
        if (i == 0)
        {
            if (!soundDict.ContainsKey(name))
                LoadRes(name);
            audioClip = soundDict[name];
        }
        else
        {
            name += MyRandom.Instance.NextInt(1, i + 1);
            if (!soundDict.ContainsKey(name))
                LoadRes(name);
            audioClip = soundDict[name];
        }

        source.clip = GameObject.Instantiate(audioClip);
        soundList.Add(source);
        source.volume = soundValue;
        source.loop = isLoop;
        source.Play();
        if (cooldown > 0f)
            lastPlayTimeByName[cooldownKey] = Time.time;
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
            if (bkMusic)
            {
                bkMusic.volume = bkValue;
            }
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

    private void LoadRes(string name)
    {
        soundDict[name] = Resources.Load<AudioClip>(name);
        Debug.Log("加载资源" + soundDict[name]);
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
        AudioClip audioClip;
        name = "Sounds/" + name;
        if (!soundDict.ContainsKey(name))
            LoadRes(name);
        audioClip = soundDict[name];
        bkMusic.clip = GameObject.Instantiate(audioClip);
        bkMusic.volume = bkValue;
        bkMusic.loop = true;
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
