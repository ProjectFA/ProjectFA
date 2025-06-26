using UnityEngine;

public class UI_Scene : UI_Base
{
    Canvas _canvas;

    // Base�� Init()�� �� ȣ���ϱ�.
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

    /// <summary>
    /// SceneUI�� sortOrder�� ä��ϴ�.
    /// </summary>
    /// <param name="sortOrder"></param>
    public void SetSortOrder(int sortOrder)
    {
        if (_canvas == null)
            _canvas = Util.GetOrAddComponent<Canvas>(gameObject);

        _canvas.overrideSorting = true;
        _canvas.sortingOrder = sortOrder;
        Debug.Log($"{gameObject.name} - Sorting Order: {_canvas.sortingOrder}");
    }
}
