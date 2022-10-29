using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BasicClickableUI : MonoBehaviour
{
    private Button _button;

    public bool playClickAnim = true;

    [Header("Animation config")] 
    [SerializeField]private float shrinkedSize = 0.8f;
    [SerializeField]private float shrinkTime = 0.3f;
    [Space]
    [SerializeField] private float defaultSize = 1f;
    [SerializeField] private float growTime = 0.15f;

    void Awake()
    {        
        _button = GetComponent<Button>();
    }

    public void AssignClickAction(Action clickAction)
    {
        if (_button == null)
            return;
        
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            if (playClickAnim)
            {
                ClickAnimation(clickAction);
            }
            else
            {
                clickAction?.Invoke();
            }
        });
    }

    private void ClickAnimation(Action clickAction)
    {
        if (clickAction != null)
        {
            DOTween.Sequence()
                .Append(_button.transform.DOScale(shrinkedSize, shrinkTime))
                .Append(_button.transform.DOScale(defaultSize, growTime))
                .AppendCallback(clickAction.Invoke);
        }
    }
}
