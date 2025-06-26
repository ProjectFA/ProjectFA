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
    /// Ű �̺�Ʈ�� ���ε� �մϴ�.
    /// </summary>
    /// <param name="key">�Է� �� �ݹ��� ȣ���� Ű �Դϴ�.</param>
    /// <param name="action">���ε��� �ݹ� �Լ��Դϴ�.</param>
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
    /// ���ε� �� Ű �̺�Ʈ�� �����մϴ�.
    /// </summary>
    /// <param name="key">�Է� �� �ݹ��� ȣ���� Ű �Դϴ�.</param>
    /// <param name="action">����ε��� �ݹ� �Լ��Դϴ�.</param>
    public void UnbindKeyEvent(Key key, Action action)  
    {
        if (_keyAction.ContainsKey(key))
            _keyAction[key] -= action;
    }

    /// <summary>
    /// ���콺 �̺�Ʈ�� ���ε� �մϴ�.
    /// </summary>
    /// <param name="action">���ε��� �ݹ� �Լ��Դϴ�.</param>
    public void BindMouseEvent(Action<Define.MouseEvent> action) { UnbindMouseEvent(action); _mouseAction += action; }

    /// <summary>
    /// ���ε� �� ���콺 �̺�Ʈ�� �����մϴ�.
    /// </summary>
    /// <param name="action">����ε��� �ݹ� �Լ��Դϴ�.</param>
    public void UnbindMouseEvent(Action<Define.MouseEvent> action) { _mouseAction -= action; }

    public void OnUpdate()
    {
        // ��� Ű �Է¿� ���� Invoke
        foreach (var action in _keyAction)
        {
            if (Keyboard.current[action.Key].wasPressedThisFrame)
                action.Value.Invoke();
        }

        // UI�� ���콺 ���� ���� ��� Ŭ�� ����
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_mouseAction != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                _mouseAction.Invoke(Define.MouseEvent.Click);  // ó�� ���� ����
            else if (Mouse.current.leftButton.isPressed)
                _mouseAction.Invoke(Define.MouseEvent.Pressed);  // ��� �����ִ� ����
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
                _mouseAction.Invoke(Define.MouseEvent.Release);  // �� ����
        }
    }

    public void Clear()
    {
        _keyAction.Clear();
        _mouseAction = null;
    }
}
