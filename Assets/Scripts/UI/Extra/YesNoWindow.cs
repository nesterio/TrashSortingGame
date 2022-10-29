using System;
using UnityEngine;
using TMPro;

namespace UI.Extra
{
    public class YesNoWindow : MonoBehaviour
    {
        [SerializeField] protected BasicClickableUI yesButton;
        [SerializeField] protected BasicClickableUI noButton;
        [SerializeField] protected BasicClickableUI outsideButton;
        [Space(5)]
        [SerializeField] protected TMP_Text MessageText;

        protected Transform Parent
        {
            get => gameObject.transform;
            set => gameObject.transform.parent = value;
        }

        protected string _message;
        protected Action _yesAction;

        private void Start()
        {
            void CloseAction()
            {
                gameObject.SetActive(false);
            }

            if (outsideButton != null)
            {
                outsideButton.playClickAnim = false;
                outsideButton.AssignClickAction(CloseAction);
            }

            if (noButton != null)
            {
                noButton.AssignClickAction(CloseAction);
            }
        }

        private void Initialize(Transform parent, Action yesAction, string message)
        {
            Parent = parent;
            _message = message;
            _yesAction = yesAction;

            if (MessageText != null)
                MessageText.text = _message;
            
            if(yesButton != null)
                yesButton.AssignClickAction(yesAction);
            
            gameObject.SetActive(true);
        }

        public static void CreateYesNoWindow(Transform parent, Action yesAction, string message = "Are you sure?")
        {
            var windowObj = UI_Manager.Instance.FindObjByName("YesNoWindow");
            if (windowObj == null)
            {
                Debug.Log("Couldn't find prefab for YesNoWindow in UI_Manager");
                return;
            }
        
            YesNoWindow window = windowObj.GetComponent<YesNoWindow>();
            
            if (parent != null) window.transform.SetParent(parent, false);
            else return;
            
            window.Initialize(parent, yesAction, message);
        }
        
    }
}