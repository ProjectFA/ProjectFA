using UnityEngine;

public class ResourceManager
{
    /// <summary>
    /// 에셋을 로드합니다.
    /// </summary>
    /// <typeparam name="T">로드 할 에셋의 타입입니다.</typeparam>
    /// <param name="path">로드 할 에셋의 경로입니다.</param>
    /// <returns>로드 한 에셋을 반환합니다.</returns>
    public T Load<T>(string path) where T : Object
    {
        T asset = Resources.Load<T>(path);

        if (asset == null)
            Debug.Log($"에셋 로드에 실패했습니다: {path}");

        return asset;
    }

    /// <summary>
    /// 프리팹을 객체화 합니다.
    /// </summary>
    /// <param name="path">객체화 할 프리팹의 주소입니다.</param>
    /// <param name="parent">특정 부모 아래 자식으로 객체화 합니다. 입력하지 않을 경우 씬에 객체화 합니다.</param>
    /// <returns>객체화 된 오브젝트를 반환합니다.</returns>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab at path: {path}");
            return null;
        }

        GameObject go = GameObject.Instantiate(prefab, parent);

        // 이름에서 "Clone" 제거
        int idx = go.name.IndexOf("(Clone)");
        if (idx > 0)
            go.name = go.name.Substring(0, idx);

        return go;
    }

    /// <summary>
    /// 오브젝트를 
    /// </summary>
    /// <param name="go"></param>
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("Destroy()에 입력된 게임 오브젝트가 nullptr 입니다.");
            return;
        }

        GameObject.Destroy(go);
    }

}
