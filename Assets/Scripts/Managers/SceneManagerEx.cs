using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    /// <summary>
    /// 현재 씬을 반환하는 프로퍼티 입니다.
    /// </summary>
    public BaseScene CurrentScene { get { return Object.FindAnyObjectByType<BaseScene>(); } }

    /// <summary>
    /// 현재 씬을 초기화 하고 새 씬을 불러옵니다.
    /// </summary>
    /// <param name="type">불러올 씬의 enum type</param>
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        // 현재 씬의 정보 초기화
        SceneManager.LoadScene(GetSceneName(type));
    }

    private string GetSceneName(Define.Scene type)
    {
        return System.Enum.GetName(typeof(Define.Scene), type);
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
