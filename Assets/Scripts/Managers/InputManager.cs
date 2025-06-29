using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager
{
    private Dictionary<Key, Action> _keyAction = new();
    private Action<Define.MouseEvent> _mouseAction = null;

    /// <summary>
    /// 키 이벤트를 바인딩 합니다.
    /// </summary>
    /// <param name="key">입력 시 콜백을 호출할 키 입니다.</param>
    /// <param name="action">바인딩할 콜백 함수입니다.</param>
    public void BindKeyEvent(Key key, Action action)
    { 
        if (_keyAction.ContainsKey(key))
        {
            UnbindKeyEvent(key, action);
            _keyAction[key] += action;
        }
        else
            _keyAction[key] = action;
    }

    /// <summary>
    /// 바인딩 된 키 이벤트를 제거합니다.
    /// </summary>
    /// <param name="key">입력 시 콜백을 호출할 키 입니다.</param>
    /// <param name="action">언바인딩할 콜백 함수입니다.</param>
    public void UnbindKeyEvent(Key key, Action action)  
    {
        if (_keyAction.ContainsKey(key))
            _keyAction[key] -= action;
    }

    /// <summary>
    /// 마우스 이벤트를 바인딩 합니다.
    /// </summary>
    /// <param name="action">바인딩할 콜백 함수입니다.</param>
    public void BindMouseEvent(Action<Define.MouseEvent> action) { UnbindMouseEvent(action); _mouseAction += action; }

    /// <summary>
    /// 바인딩 된 마우스 이벤트를 제거합니다.
    /// </summary>
    /// <param name="action">언바인딩할 콜백 함수입니다.</param>
    public void UnbindMouseEvent(Action<Define.MouseEvent> action) { _mouseAction -= action; }

    public void OnUpdate()
    {
        // 모든 키 입력에 대해 Invoke
        foreach (var action in _keyAction)
        {
            if (Keyboard.current[action.Key].wasPressedThisFrame)
                action.Value.Invoke();
        }

        // UI가 마우스 위에 있을 경우 클릭 무시
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_mouseAction != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                _mouseAction.Invoke(Define.MouseEvent.Click);  // 처음 눌린 순간
            else if (Mouse.current.leftButton.isPressed)
                _mouseAction.Invoke(Define.MouseEvent.Pressed);  // 계속 눌려있는 상태
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
                _mouseAction.Invoke(Define.MouseEvent.Release);  // 뗀 순간
        }
    }

    public void Clear()
    {
        _keyAction.Clear();
        _mouseAction = null;
    }
}
