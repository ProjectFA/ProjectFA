using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
    /// <summary>
    /// UI에게 지정해줄 sorting order
    /// </summary>
    int _sortingOrder = 50;

    Stack<UI_Popup> _popupStack = new();

    /// <summary>
    /// Get: 루트 UI 오브젝트를 가져옵니다.
    /// </summary>
    public GameObject UIRoot
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }


    /// <summary>
    /// 오브젝트의 Canvas에 있는 sortingOrder를 채웁니다. 
    /// 예를 들어 팝업 UI 등이 켜질 때 사용할 수 있습니다.
    /// 오브젝트에 Canvas가 없다면 내부적으로 Canvas를 추가합니다.
    /// </summary>
    /// <param name="go">Canvas를 추가하고 가져올 게임 오브젝트입니다.</param>
    /// <param name="useSortingOrder">sorting order를 지정할 지 선택합니다.</param>
    public void SetCanvas(GameObject go, bool useSortingOrder = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);

        // 스크린 좌표로 렌더
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        if (useSortingOrder)
        { 
            // 부모의 sort order 무시
            canvas.overrideSorting = true;

            // _sortingOrder를 지정합니다.
            canvas.sortingOrder = _sortingOrder++;
        }
    }

    /// <summary>
    /// SubItem 프리팹을 객체화 합니다.
    /// </summary>
    /// <typeparam name="T">불러올 서브 아이템 UI의 클래스 타입입니다.</typeparam>
    /// <param name="name">불러올 서브 아이템 UI의 이름입니다. 입력하지 않을 경우 클래스 타입 이름으로 설정됩니다.</param>
    /// <returns>객체화 한 오브젝트를 반환합니다.</returns>
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;


        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (go == null)
        {
            Debug.LogError($"MakeSubItem 실패. 객체화에 실패했습니다.\n path: UI/SubItem/{name}");
            return null;
        }

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }

    /// <summary>
    /// SceneUI 프리팹을 객체화 합니다.
    /// </summary>
    /// <typeparam name="T">불러올 씬UI 클래스 타입입니다.</typeparam>
    /// <param name="name">불러올 씬UI의 이름입니다. 입력하지 않을 경우 클래스 타입 이름으로 설정됩니다.</param>
    /// <returns>객체화 한 오브젝트를 반환합니다.</returns>
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (go == null)
        {
            Debug.LogError($"ShowSceneUI 실패. 객체화에 실패했습니다.\n path: UI/Scene/{name}");
            return null;
        }

        T sceneUI = Util.GetOrAddComponent<T>(go);
        go.transform.SetParent(UIRoot.transform);

        return sceneUI;
    }


    /// <summary>
    /// PopupUI 프리팹을 객체화 합니다.
    /// </summary>
    /// <typeparam name="T">불러올 팝업UI 클래스 타입입니다.</typeparam>
    /// <param name="name">불러올 팝업UI의 이름입니다. 입력하지 않을 경우 클래스 타입 이름으로 설정됩니다.</param>
    /// <returns>객체화 한 오브젝트를 반환합니다.</returns>
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
        {
            Debug.LogError($"ShowPopupUI 실패. 객체화에 실패했습니다.\n path: UI/Popup/{name}");
            return null;
        }

        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(UIRoot.transform);

        return popup;
    }

    /// <summary>
    /// 레벨에 생성된 PopupUI를 닫습니다.
    ///  peek() != popup일 경우 제거하지 않습니다.
    /// </summary>
    /// <param name="popup">Popup Stack에서 제거할 PopupUI입니다.</param>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0 || _popupStack.Peek() != popup)
            Debug.LogError($"ClosePopup Failed!: {popup.name}");

        ClosePopupUI();
    }

    /// <summary>
    /// Popup Stack에서 가장 sort order가 높은 PopupUI를 제거합니다.
    /// </summary>
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
        {
            Debug.LogError("Popup Stack이 비어있지만 ClosePopupUI()를 호출했습니다.");
            return;
        }

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _sortingOrder--;
    }

    /// <summary>
    /// 모든 PopupUI를 제거합니다. PopupStack을 clear 합니다.
    /// </summary>
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
    }

}
