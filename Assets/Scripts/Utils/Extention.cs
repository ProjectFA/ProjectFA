using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extention
{
    /// <summary>
    /// UI �̺�Ʈ�� ���ε� �մϴ�.
    /// ���� �Էµ� ������Ʈ�� UI_EventHandler ������Ʈ�� ���ٸ� �߰��մϴ�.
    /// </summary>
    /// <param name="go">UI_EventHandler ������Ʈ�� �ִ� ������Ʈ. ���� UI_EventHandler ������Ʈ�� ���ٸ� �߰��մϴ�.</param>
    /// <param name="action">���ε��� �ݹ� �Լ��Դϴ�.</param>
    /// <param name="type">�ش� �̺�Ʈ ȣ�� �� �ݹ��� ȣ���� �̺�Ʈ �Դϴ�.</param>
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Cilck)
    {
        UI_Base.BindEvent(go, action, type);
    }


    /// <summary>
    /// ������Ʈ�� �����ɴϴ�. ���� �ش� ������Ʈ�� ���ٸ� �߰��� �� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T">������ ������Ʈ�� Ÿ���Դϴ�.</typeparam>
    /// <param name="go">������Ʈ�� ������ ������Ʈ�Դϴ�.</param>
    /// <returns>�ش� Ÿ���� ������Ʈ�� ��ȯ�մϴ�.</returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
}
