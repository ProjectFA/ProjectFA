using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // AudioSource - ������ �����ų ��
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Max];

    // AudioClip - ����
    Dictionary<string, AudioClip> _audioClips = new();

    // AudioListner - ��(���� ī�޶�)
    

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

        // ��������� ��� ���� Ȱ��ȭ
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
    /// AudioClip�� �ҷ�����, �����ŵ�ϴ�.
    /// </summary>
    /// <param name="path">AudioClip �ּ�</param>
    /// <param name="type">�����ų ���� Ÿ��</param>
    /// <param name="pitch"></param>
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        Play(GetOrAddAudioClip(path, type), type, pitch);
    }

    /// <summary>
    /// AudioClip�� �����ŵ�ϴ�.
    /// </summary>
    /// <param name="audioClip">�����ų AudioClip</param>
    /// <param name="type">�����ų ���� Ÿ��</param>
    /// <param name="pitch"></param>
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            Debug.LogError("SoundManager Play() ����: �����Ŭ���� null �Դϴ�.");
            return;
        }

        if (Define.Sound.Bgm == type)
        {
            AudioSource audioSource = _audioSources[(int)type];
            // ���� ����Ǵ� ��� ������ ����
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
    /// AudioClip�� �ҷ��ɴϴ�.
    /// </summary>
    /// <param name="path">AudioClip �ּ�</param>
    /// <param name="type">�ҷ��� ������ Ÿ��</param>
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
                // ���� ���� ��ųʸ��� �ҷ��� ���� ���ٸ� ���� Load ���־��.
                audioClip = Managers.Resource.Load<AudioClip>(path);
                // ��ųʸ��� �ҷ��� �����Ŭ�� �߰�
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip�� �����ϴ�. path: {path}");
            return null;
        }

        return audioClip;
    }

}
