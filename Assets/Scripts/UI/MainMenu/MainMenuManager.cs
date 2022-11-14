using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UI.Extra;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private float _swipeAnimationDuration;
    
    [Space(5)]
    
    [Header("Panels")]
    [SerializeField] Transform Canvas;
    
    [SerializeField] private RectTransform PanelsParent;

    [Space(5)] 
    
    [Header("Buttons")] 
    [SerializeField] Button OpenShopButton;
    [SerializeField] Button CloseShopButton;
    
    [SerializeField] Button OpenSettingsButton;
    [SerializeField] Button CloseSettingsButton;

    [SerializeField] Button PlayButton;
    [SerializeField] Button ExitButton;
    
    [Space(5)]
    
    [Header("Positions")]
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 shopPosition;
    [SerializeField] Vector2 settingsPosition;
    [Space]
    [SerializeField] float recoil;
    [SerializeField] private float recoilSpeed;

    
    
    private RectTransform _currentPanel;

    void Start()
    {
        InitializeButtons();
    }

    void InitializeButtons()
    {       
        var shopClickable = OpenShopButton.GetComponent<BasicClickableUI>();
        if(shopClickable != null)
        {
            shopClickable.AssignClickAction(() =>
            {
                OpenPanel(shopPosition);
            });
        }
        var closeShopClickable = CloseShopButton.GetComponent<BasicClickableUI>();
        if(closeShopClickable != null)
        {
            closeShopClickable.AssignClickAction(() =>
            {
                OpenPanel(startPosition);
            });
        }
        var settingsClickable = OpenSettingsButton.GetComponent<BasicClickableUI>();
        if(settingsClickable != null)
        {
            settingsClickable.AssignClickAction(() =>
            {
                OpenPanel(settingsPosition);
            });
        }
        var closeSettingsClickable = CloseSettingsButton.GetComponent<BasicClickableUI>();
        if(closeSettingsClickable != null)
        {
            closeSettingsClickable.AssignClickAction(() =>
            {
                OpenPanel(startPosition);
            });
        }
        var playClickable = PlayButton.GetComponent<BasicClickableUI>();
        if(playClickable != null)
        {
            playClickable.AssignClickAction(() =>
            {
                YesNoWindow.CreateYesNoWindow(Canvas, () =>
                {
                    SceneManager.LoadScene(1);
                }, "Are you sure?");
            });
        }
        var exitClickable = ExitButton.GetComponent<BasicClickableUI>();
        if(exitClickable != null)
        {
            exitClickable.AssignClickAction(() =>
            {
                YesNoWindow.CreateYesNoWindow(Canvas, () =>
                {
                    Application.Quit();
                }, "Are you sure you want to quit?");
            });
        }
    }

    void OpenPanel(Vector2 position)
    {
        if (PanelsParent == null)
            return;

        var oldPos = PanelsParent.anchoredPosition;

        DOTween.Sequence()
            .Append(PanelsParent.DOAnchorPos
            (position + new Vector2
                (position.x - oldPos.x > 0 ? recoil : -recoil, 0), _swipeAnimationDuration))
            .Append(PanelsParent.DOAnchorPos(position, recoilSpeed));

        _currentPanel = PanelsParent;
    }
}
