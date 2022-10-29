using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Transform startPanel;
    [SerializeField] Transform shopPanel;
    [SerializeField] Transform settingsPanel;
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject exitPanel;

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
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 shopPosition;
    [SerializeField] Vector3 settingsPosition;
    [Space]
    [SerializeField] float recoil;
    [SerializeField] private float recoilSpeed;

    
    
    private Transform _currentPanel;

    void Start()
    {
        _currentPanel = startPanel;
        InitializeButtons();
    }

    void InitializeButtons()
    {       
        var shopClickable = OpenShopButton.GetComponent<BasicClickableUI>();
        if(shopClickable != null)
        {
            shopClickable.AssignClickAction(() =>
            {
                OpenPanel(shopPanel, shopPosition);
            });
        }
        var closeShopClickable = CloseShopButton.GetComponent<BasicClickableUI>();
        if(closeShopClickable != null)
        {
            closeShopClickable.AssignClickAction(() =>
            {
                OpenPanel(startPanel, shopPosition);
            });
        }
        var settingsClickable = OpenSettingsButton.GetComponent<BasicClickableUI>();
        if(settingsClickable != null)
        {
            settingsClickable.AssignClickAction(() =>
            {
                OpenPanel(settingsPanel, settingsPosition);
            });
        }
        var closeSettingsClickable = CloseSettingsButton.GetComponent<BasicClickableUI>();
        if(closeSettingsClickable != null)
        {
            closeSettingsClickable.AssignClickAction(() =>
            {
                OpenPanel(startPanel, settingsPosition);
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

    void OpenPanel(Transform newPanel, Vector3 posForOldPanel)
    {
        if (newPanel == null)
            return;

        DOTween.Sequence()
            .Append(newPanel.DOMove
            (startPosition + new Vector3
                (posForOldPanel.x > 0 ? -recoil : recoil, 0, 0), _swipeAnimationDuration))
            .Append( newPanel.DOMove(startPosition, recoilSpeed));
        
        _currentPanel.DOMove(posForOldPanel, _swipeAnimationDuration);

        _currentPanel = newPanel;
    }
}
