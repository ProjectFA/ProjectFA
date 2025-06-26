using UnityEngine;

public class ResourceManager
{
    /// <summary>
    /// ������ �ε��մϴ�.
    /// </summary>
    /// <typeparam name="T">�ε� �� ������ Ÿ���Դϴ�.</typeparam>
    /// <param name="path">�ε� �� ������ ����Դϴ�.</param>
    /// <returns>�ε� �� ������ ��ȯ�մϴ�.</returns>
    public T Load<T>(string path) where T : Object
    {
        T asset = Resources.Load<T>(path);

        if (asset == null)
            Debug.Log($"���� �ε忡 �����߽��ϴ�: {path}");

        return asset;
    }

    /// <summary>
    /// �������� ��üȭ �մϴ�.
    /// </summary>
    /// <param name="path">��üȭ �� �������� �ּ��Դϴ�.</param>
    /// <param name="parent">Ư�� �θ� �Ʒ� �ڽ����� ��üȭ �մϴ�. �Է����� ���� ��� ���� ��üȭ �մϴ�.</param>
    /// <returns>��üȭ �� ������Ʈ�� ��ȯ�մϴ�.</returns>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab at path: {path}");
            return null;
        }

        GameObject go = GameObject.Instantiate(prefab, parent);

        // �̸����� "Clone" ����
        int idx = go.name.IndexOf("(Clone)");
        if (idx > 0)
            go.name = go.name.Substring(0, idx);

        return go;
    }

    /// <summary>
    /// ������Ʈ�� 
    /// </summary>
    /// <param name="go"></param>
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("Destroy()�� �Էµ� ���� ������Ʈ�� nullptr �Դϴ�.");
            return;
        }

        GameObject.Destroy(go);
    }

}
