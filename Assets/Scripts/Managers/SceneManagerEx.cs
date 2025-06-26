using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    /// <summary>
    /// ���� ���� ��ȯ�ϴ� ������Ƽ �Դϴ�.
    /// </summary>
    public BaseScene CurrentScene { get { return Object.FindAnyObjectByType<BaseScene>(); } }

    /// <summary>
    /// ���� ���� �ʱ�ȭ �ϰ� �� ���� �ҷ��ɴϴ�.
    /// </summary>
    /// <param name="type">�ҷ��� ���� enum type</param>
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        // ���� ���� ���� �ʱ�ȭ
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
