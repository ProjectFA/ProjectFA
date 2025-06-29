using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    //public Dictionary<int, Item> Items { get; private set; } = new();

    //public event Action<int, Item> OnItemChanged;

    /// <summary>
    /// 모든 데이터 초기화 (JSON 로드)
    /// </summary>
    public void Init()
    {
        //Items = LoadJson<ItemData, int, Item>("ItemData").MakeDict();

        Debug.Log("데이터 매니저 초기화가 완료되었습니다.");
    }

    /// <summary>
    /// 아이템 데이터 수정 함수
    /// </summary>
    //public void SetItem(int key, Action<Item> updateAction)
    //{
    //    if (!Items.TryGetValue(key, out var item))
    //    {
    //        Debug.LogError($"아이템 ID {key}를 찾을 수 없습니다.");
    //        return;
    //    }

    //    updateAction.Invoke(item);
    //    OnItemChanged?.Invoke(key, item);
    //    Debug.Log($"아이템 ID {key}가 성공적으로 수정되었습니다.");
    //}

    /// <summary>
    /// 전체 데이터 저장 (JSON으로)
    /// </summary>
    public void SaveAll()
    {
        //SaveJson(new ItemData { items = Items.Values.ToList() }, "ItemData.json");

        Debug.Log("모든 데이터가 정상적으로 저장되었습니다.");
    }


    /// <summary>
    /// 아이템 데이터만 저장
    /// </summary>
    public void SaveItemData()
    {
        //SaveJson(new ItemData { items = Items.Values.ToList() }, "ItemData.json");
        Debug.Log("아이템 데이터가 저장되었습니다.");
    }


    #region 내부 JSON 처리

    private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset asset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        if (asset == null)
        {
            Debug.LogError($"JSON 파일을 찾을 수 없습니다: Data/{path}");
            return default;
        }

        Debug.Log($"JSON 파일을 성공적으로 불러왔습니다: {path}");
        return JsonUtility.FromJson<Loader>(asset.text);
    }

    private void SaveJson<T>(T data, string path) where T : class
    {
        if (data == null)
        {
            Debug.LogError($"저장할 데이터가 없습니다: {path}");
            return;
        }

        string json = JsonUtility.ToJson(data, true);

        try
        {
            File.WriteAllText($"Assets/Resources/Data/{path}", json);
            Debug.Log($"데이터가 저장되었습니다: Assets/Resources/Data/{path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"데이터 저장에 실패했습니다: {e.Message}");
        }
    }

    #endregion
}
