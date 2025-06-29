using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // AudioSource - 음원을 재생시킬 곳
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Max];

    // AudioClip - 음원
    Dictionary<string, AudioClip> _audioClips = new();

    // AudioListner - 귀(메인 카메라)
    

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for (int i = 0; i < soundNames.Length - 1; ++i)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
        }

        // 배경음악의 경우 루프 활성화
        _audioSources[(int)Define.Sound.Bgm].loop = true;
    }

    public void Clear()
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }

    /// <summary>
    /// AudioClip을 불러오고, 실행시킵니다.
    /// </summary>
    /// <param name="path">AudioClip 주소</param>
    /// <param name="type">재생시킬 사운드 타입</param>
    /// <param name="pitch"></param>
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        Play(GetOrAddAudioClip(path, type), type, pitch);
    }

    /// <summary>
    /// AudioClip을 실행시킵니다.
    /// </summary>
    /// <param name="audioClip">재생시킬 AudioClip</param>
    /// <param name="type">재생시킬 사운드 타입</param>
    /// <param name="pitch"></param>
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            Debug.LogError("SoundManager Play() 오류: 오디오클립이 null 입니다.");
            return;
        }

        if (Define.Sound.Bgm == type)
        {
            AudioSource audioSource = _audioSources[(int)type];
            // 기존 재생되던 배경 음악을 종료
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)type];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }

    }

    /// <summary>
    /// AudioClip을 불러옵니다.
    /// </summary>
    /// <param name="path">AudioClip 주소</param>
    /// <param name="type">불러올 사운드의 타입</param>
    /// <returns></returns>
    public AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (!path.Contains("Sounds/"))
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (Define.Sound.Bgm == type)
            audioClip = Managers.Resource.Load<AudioClip>(path);
        else
        {
            audioClip = GetOrAddAudioClip(path);
            if (!_audioClips.TryGetValue(path, out audioClip))
            {
                // 만약 기존 딕셔너리에 불러온 적이 없다면 새로 Load 해주어요.
                audioClip = Managers.Resource.Load<AudioClip>(path);
                // 딕셔너리에 불러온 오디오클립 추가
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip이 없습니다. path: {path}");
            return null;
        }

        return audioClip;
    }

}
