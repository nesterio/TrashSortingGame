using UnityEngine;

namespace Scripts.Clicking
{
    public class ClickDetector
    {
        InputManager _inputManager = InputManager.Instance;

        public ClickDetector()
        {
            if(_inputManager != null)
                _inputManager.ClickEvent += OnClick;
        }

        private void OnClick()
        {
            TryDetectItem();
        }
    
        private void TryDetectItem() 
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null 
                    && raycastHit.transform.gameObject.CompareTag("Trash")) // TODO: add tag Trash
                {
                    var hitTrash = raycastHit.transform.gameObject;
                }
                else
                {
                    // TODO: https://www.dropbox.com/scl/fi/3fxr9jfh0xvewqip266mq/.paper?dl=0&noframe=0&rlkey=v03pklhqcpltj02hmrc3gfkzo
                }
            }
        }
    }
}