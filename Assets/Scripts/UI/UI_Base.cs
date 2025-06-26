using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public abstract class UI_Base : MonoBehaviour
{

    // Ÿ�� ���� ������Ʈ�� ���� ������ �ִ� ��ųʸ�
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new();

    public abstract void Init();

    /// <summary>
    /// ���ϴ� Ÿ���� UI�� ��ųʸ��� ���ε� ��Ų��.
    /// </summary>
    /// <typeparam name="T">������ ������Ʈ�� Ÿ��</typeparam>
    /// <param name="type">���ε� �ϰ��� �ϴ� Enum Ÿ��. Enum�� ��Ҵ� ������Ʈ�� �̸��� ���ƾ� �Ѵ�. 
    /// typeof() �� ����� Enum�� ��ȯ���Ѿ� �Ѵ�. Enum ���̹�: ���� ��� ���� ������Ʈ Ÿ���� ���ε� �Ϸ��� GameObjects �� ���� �����Ѵ�.</param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; ++i)
        {
            // �ֻ��� �θ���� ��������� Ž��
            objects[i] = typeof(T) == typeof(GameObject) ?
                Util.FindChildObject(gameObject, names[i], true) : Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.LogError($"���ε� ���� �߽��ϴ�: {names[i]}, {type}");
        }
    }

    /// <summary>
    /// ��ųʸ����� ������Ʈ�� �����´�.
    /// </summary>
    /// <typeparam name="T">������ ������Ʈ�� Ÿ��</typeparam>
    /// <param name="idx">**Enum�� ����ϴ� ���� ��õ** ������ UI�� Enum�� ����� �����Ѵ�.</param>
    /// <returns>�ùٸ� ������ ������Ʈ�� �ִٸ� �� ������Ʈ�� ��ȯ�Ѵ�. ���� �� null�� ��ȯ�Ѵ�.</returns>
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] obj = null;
        if (!_objects.TryGetValue(typeof(T), out obj))
            return null;

        return obj[idx] as T;
    }

    /// <summary>
    /// ��ųʸ����� GameObject�� �����´�.
    /// </summary>
    /// <param name="idx">**Enum�� ����ϴ� ���� ��õ** ������ UI�� Enum�� ����� �����Ѵ�.</param>
    /// <returns></returns>
    protected GameObject GetObject(int idx)
    {
        return Get<GameObject>(idx);
    }

    /// <summary>
    /// ��ųʸ����� TMPro.TMP_Text�� �����´�.
    /// </summary>
    /// <param name="idx">**Enum�� ����ϴ� ���� ��õ** ������ UI�� Enum�� ����� �����Ѵ�.</param>
    /// <returns></returns>
    protected TMPro.TMP_Text GetText(int idx)
    {
        return Get<TMPro.TMP_Text>(idx);
    }

    /// <summary>
    /// ��ųʸ����� Button�� �����´�.
    /// </summary>
    /// <param name="idx">**Enum�� ����ϴ� ���� ��õ** ������ UI�� Enum�� ����� �����Ѵ�.</param>
    /// <returns></returns>
    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    /// <summary>
    /// ��ųʸ����� Image �����´�.
    /// </summary>
    /// <param name="idx">**Enum�� ����ϴ� ���� ��õ** ������ UI�� Image�� ����� �����Ѵ�.</param>
    /// <returns></returns>
    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }

    /// <summary>
    /// UI �̺�Ʈ�� ���ε� �մϴ�.
    /// ���� �Էµ� ������Ʈ�� UI_EventHandler ������Ʈ�� ���ٸ� �߰��մϴ�.
    /// </summary>
    /// <param name="go">UI_EventHandler ������Ʈ�� �ִ� ������Ʈ. ���� UI_EventHandler ������Ʈ�� ���ٸ� �߰��մϴ�.</param>
    /// <param name="action">���ε��� �ݹ� �Լ��Դϴ�.</param>
    /// <param name="type">�ش� �̺�Ʈ ȣ�� �� �ݹ��� ȣ���� �̺�Ʈ �Դϴ�.</param>
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Cilck)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        { 
            case Define.UIEvent.Cilck:
                evt.BindClickEvent(action);
                break;

            case Define.UIEvent.BeginDrag:
                evt.BindBeginDragEvent(action);
                break;

            case Define.UIEvent.Drag:
                evt.BindDragEvent(action);
                break;

            case Define.UIEvent.EndDrag:
                evt.BindEndDragEvent(action);
                break;
            case Define.UIEvent.Drop:
                evt.BindDropEvent(action);
                break;
        }


    }

}
