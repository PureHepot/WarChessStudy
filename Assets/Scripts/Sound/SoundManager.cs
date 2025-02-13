using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;//����bgm����Ƶ���

    private Dictionary<string, AudioClip> clips;//��Ƶ�����ֵ�

    private bool isStop;//����

    public bool IsStop
    {
        get
        {
            return isStop;
        }
        set
        {
            isStop  = value;
            if (isStop == true)
            {
                bgmSource.Pause();
            }
            else
            {
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume;//bgm������С

    public float BgmVolume
    {
        get
        {
            return bgmVolume;
        }
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }

    private float effectVolume;//��Ч��С

    public float EffectVolume
    {
        get
        {
            return effectVolume;
        }
        set
        {
            effectVolume = value;
        }
    }


    public SoundManager()
    {
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        clips = new Dictionary<string, AudioClip>();

        IsStop = false;
        bgmVolume = 1f;
        effectVolume = 1f;
    }

    public void PlayBGM(string res)
    {
        if (isStop)
        {
            return;
        }
        if (clips.ContainsKey(res) == false)
        {
            //������Ƶ
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip);
        }
        bgmSource.clip = clips[res];
        bgmSource.Play();
    }
}
