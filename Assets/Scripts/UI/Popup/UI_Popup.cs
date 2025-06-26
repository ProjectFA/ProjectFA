using System.Collections;
using UnityEngine;

public class UI_Popup : UI_Base
{
    protected CanvasGroup _canvasGroup;
    protected float _fadeDuration = 0.2f;

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject);
        _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        _canvasGroup.alpha = 0; // 시작 시 투명하게 설정
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// 이 UI를 닫습니다.
    /// </summary>
    public virtual void ClosePopupUI()
    {
        StartCoroutine(FadeOutAndClose());
    }

    protected IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / _fadeDuration);
            yield return null;
        }
        _canvasGroup.alpha = 1;
    }

    protected IEnumerator FadeOutAndClose()
    {
        float elapsedTime = 0;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / _fadeDuration);
            yield return null;
        }
        _canvasGroup.alpha = 0;
        Managers.UI.ClosePopupUI(this);
    }
}
