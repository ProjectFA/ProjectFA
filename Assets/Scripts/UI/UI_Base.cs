using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public abstract class UI_Base : MonoBehaviour
{

    // 타입 별로 오브젝트를 전부 가지고 있는 딕셔너리
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new();

    public abstract void Init();

    /// <summary>
    /// 원하는 타입의 UI를 딕셔너리에 바인딩 시킨다.
    /// </summary>
    /// <typeparam name="T">가져올 오브젝트의 타입</typeparam>
    /// <param name="type">바인딩 하고자 하는 Enum 타입. Enum의 요소는 오브젝트의 이름과 같아야 한다. 
    /// typeof() 를 사용해 Enum을 변환시켜야 한다. Enum 네이밍: 예를 들어 게임 오브젝트 타입을 바인딩 하려면 GameObjects 와 같이 정의한다.</param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; ++i)
        {
            // 최상위 부모부터 재귀적으로 탐색
            objects[i] = typeof(T) == typeof(GameObject) ?
                Util.FindChildObject(gameObject, names[i], true) : Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.LogError($"바인딩 실패 했습니다: {names[i]}, {type}");
        }
    }

    /// <summary>
    /// 딕셔너리에서 오브젝트를 가져온다.
    /// </summary>
    /// <typeparam name="T">가져올 오브젝트의 타입</typeparam>
    /// <param name="idx">**Enum을 사용하는 것을 추천** 가져올 UI를 Enum을 사용해 지정한다.</param>
    /// <returns>올바른 조건의 오브젝트가 있다면 그 오브젝트를 반환한다. 실패 시 null을 반환한다.</returns>
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] obj = null;
        if (!_objects.TryGetValue(typeof(T), out obj))
            return null;

        return obj[idx] as T;
    }

    /// <summary>
    /// 딕셔너리에서 GameObject를 가져온다.
    /// </summary>
    /// <param name="idx">**Enum을 사용하는 것을 추천** 가져올 UI를 Enum을 사용해 지정한다.</param>
    /// <returns></returns>
    protected GameObject GetObject(int idx)
    {
        return Get<GameObject>(idx);
    }

    /// <summary>
    /// 딕셔너리에서 TMPro.TMP_Text를 가져온다.
    /// </summary>
    /// <param name="idx">**Enum을 사용하는 것을 추천** 가져올 UI를 Enum을 사용해 지정한다.</param>
    /// <returns></returns>
    protected TMPro.TMP_Text GetText(int idx)
    {
        return Get<TMPro.TMP_Text>(idx);
    }

    /// <summary>
    /// 딕셔너리에서 Button를 가져온다.
    /// </summary>
    /// <param name="idx">**Enum을 사용하는 것을 추천** 가져올 UI를 Enum을 사용해 지정한다.</param>
    /// <returns></returns>
    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    /// <summary>
    /// 딕셔너리에서 Image 가져온다.
    /// </summary>
    /// <param name="idx">**Enum을 사용하는 것을 추천** 가져올 UI를 Image를 사용해 지정한다.</param>
    /// <returns></returns>
    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }

    /// <summary>
    /// UI 이벤트를 바인딩 합니다.
    /// 만약 입력된 오브젝트에 UI_EventHandler 컴포넌트가 없다면 추가합니다.
    /// </summary>
    /// <param name="go">UI_EventHandler 컴포넌트가 있는 오브젝트. 만약 UI_EventHandler 컴포넌트가 없다면 추가합니다.</param>
    /// <param name="action">바인딩할 콜백 함수입니다.</param>
    /// <param name="type">해당 이벤트 호출 시 콜백을 호출할 이벤트 입니다.</param>
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
