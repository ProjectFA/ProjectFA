using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
    /// <summary>
    /// UI���� �������� sorting order
    /// </summary>
    int _sortingOrder = 50;

    Stack<UI_Popup> _popupStack = new();

    /// <summary>
    /// Get: ��Ʈ UI ������Ʈ�� �����ɴϴ�.
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
    /// ������Ʈ�� Canvas�� �ִ� sortingOrder�� ä��ϴ�. 
    /// ���� ��� �˾� UI ���� ���� �� ����� �� �ֽ��ϴ�.
    /// ������Ʈ�� Canvas�� ���ٸ� ���������� Canvas�� �߰��մϴ�.
    /// </summary>
    /// <param name="go">Canvas�� �߰��ϰ� ������ ���� ������Ʈ�Դϴ�.</param>
    /// <param name="useSortingOrder">sorting order�� ������ �� �����մϴ�.</param>
    public void SetCanvas(GameObject go, bool useSortingOrder = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);

        // ��ũ�� ��ǥ�� ����
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        if (useSortingOrder)
        { 
            // �θ��� sort order ����
            canvas.overrideSorting = true;

            // _sortingOrder�� �����մϴ�.
            canvas.sortingOrder = _sortingOrder++;
        }
    }

    /// <summary>
    /// SubItem �������� ��üȭ �մϴ�.
    /// </summary>
    /// <typeparam name="T">�ҷ��� ���� ������ UI�� Ŭ���� Ÿ���Դϴ�.</typeparam>
    /// <param name="name">�ҷ��� ���� ������ UI�� �̸��Դϴ�. �Է����� ���� ��� Ŭ���� Ÿ�� �̸����� �����˴ϴ�.</param>
    /// <returns>��üȭ �� ������Ʈ�� ��ȯ�մϴ�.</returns>
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;


        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (go == null)
        {
            Debug.LogError($"MakeSubItem ����. ��üȭ�� �����߽��ϴ�.\n path: UI/SubItem/{name}");
            return null;
        }

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }

    /// <summary>
    /// SceneUI �������� ��üȭ �մϴ�.
    /// </summary>
    /// <typeparam name="T">�ҷ��� ��UI Ŭ���� Ÿ���Դϴ�.</typeparam>
    /// <param name="name">�ҷ��� ��UI�� �̸��Դϴ�. �Է����� ���� ��� Ŭ���� Ÿ�� �̸����� �����˴ϴ�.</param>
    /// <returns>��üȭ �� ������Ʈ�� ��ȯ�մϴ�.</returns>
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (go == null)
        {
            Debug.LogError($"ShowSceneUI ����. ��üȭ�� �����߽��ϴ�.\n path: UI/Scene/{name}");
            return null;
        }

        T sceneUI = Util.GetOrAddComponent<T>(go);
        go.transform.SetParent(UIRoot.transform);

        return sceneUI;
    }


    /// <summary>
    /// PopupUI �������� ��üȭ �մϴ�.
    /// </summary>
    /// <typeparam name="T">�ҷ��� �˾�UI Ŭ���� Ÿ���Դϴ�.</typeparam>
    /// <param name="name">�ҷ��� �˾�UI�� �̸��Դϴ�. �Է����� ���� ��� Ŭ���� Ÿ�� �̸����� �����˴ϴ�.</param>
    /// <returns>��üȭ �� ������Ʈ�� ��ȯ�մϴ�.</returns>
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
        {
            Debug.LogError($"ShowPopupUI ����. ��üȭ�� �����߽��ϴ�.\n path: UI/Popup/{name}");
            return null;
        }

        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(UIRoot.transform);

        return popup;
    }

    /// <summary>
    /// ������ ������ PopupUI�� �ݽ��ϴ�.
    ///  peek() != popup�� ��� �������� �ʽ��ϴ�.
    /// </summary>
    /// <param name="popup">Popup Stack���� ������ PopupUI�Դϴ�.</param>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0 || _popupStack.Peek() != popup)
            Debug.LogError($"ClosePopup Failed!: {popup.name}");

        ClosePopupUI();
    }

    /// <summary>
    /// Popup Stack���� ���� sort order�� ���� PopupUI�� �����մϴ�.
    /// </summary>
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
        {
            Debug.LogError("Popup Stack�� ��������� ClosePopupUI()�� ȣ���߽��ϴ�.");
            return;
        }

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _sortingOrder--;
    }

    /// <summary>
    /// ��� PopupUI�� �����մϴ�. PopupStack�� clear �մϴ�.
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
