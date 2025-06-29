using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extention
{
    /// <summary>
    /// UI 이벤트를 바인딩 합니다.
    /// 만약 입력된 오브젝트에 UI_EventHandler 컴포넌트가 없다면 추가합니다.
    /// </summary>
    /// <param name="go">UI_EventHandler 컴포넌트가 있는 오브젝트. 만약 UI_EventHandler 컴포넌트가 없다면 추가합니다.</param>
    /// <param name="action">바인딩할 콜백 함수입니다.</param>
    /// <param name="type">해당 이벤트 호출 시 콜백을 호출할 이벤트 입니다.</param>
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Cilck)
    {
        UI_Base.BindEvent(go, action, type);
    }


    /// <summary>
    /// 컴포넌트를 가져옵니다. 만약 해당 컴포넌트가 없다면 추가한 후 반환합니다.
    /// </summary>
    /// <typeparam name="T">가져올 컴포넌트의 타입입니다.</typeparam>
    /// <param name="go">컴포넌트를 가져올 오브젝트입니다.</param>
    /// <returns>해당 타입의 컴포넌트를 반환합니다.</returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
}
