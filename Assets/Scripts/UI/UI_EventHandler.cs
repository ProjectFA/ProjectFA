using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerClickHandler
{
    private Action<PointerEventData> _onClickEvent;
    private Action<PointerEventData> _onBeginDragEvent;
    private Action<PointerEventData> _onDragEvent;
    private Action<PointerEventData> _onEndDragEvent;
    private Action<PointerEventData> _onDropEvent;

    public void BindClickEvent(Action<PointerEventData> action) { UnbindClickEvent(action); _onClickEvent += action; }
    public void UnbindClickEvent(Action<PointerEventData> action) { _onClickEvent -= action; }

    public void BindBeginDragEvent(Action<PointerEventData> action) { UnbindBeginDragEvent(action); _onBeginDragEvent += action; }
    public void UnbindBeginDragEvent(Action<PointerEventData> action) { _onBeginDragEvent -= action; }

    public void BindDragEvent(Action<PointerEventData> action) { UnbindDragEvent(action); _onDragEvent += action; }
    public void UnbindDragEvent(Action<PointerEventData> action) { _onDragEvent -= action; }

    public void BindEndDragEvent(Action<PointerEventData> action) { UnbindEndDragEvent(action); _onEndDragEvent += action; }
    public void UnbindEndDragEvent(Action<PointerEventData> action) { _onEndDragEvent -= action; }

    public void BindDropEvent(Action<PointerEventData> action) { UnbindDropEvent(action); _onDropEvent += action; }
    public void UnbindDropEvent(Action<PointerEventData> action) { _onDropEvent -= action; }

    // EventHandler

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickEvent?.Invoke(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _onBeginDragEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _onDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _onEndDragEvent?.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        _onDropEvent?.Invoke(eventData);
    }

    // EventHandler end
}
