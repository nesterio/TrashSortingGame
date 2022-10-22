using UnityEngine;

namespace Scripts.Clicking
{
    public class ClickDetector
    {
        readonly InputManager _inputManager = InputManager.Instance;

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
            Debug.DrawLine ( ray.origin, ray.origin + ray.direction * 100, Color.red );
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                Debug.Log(raycastHit.transform.gameObject.name);
                
                if (raycastHit.transform != null 
                    && raycastHit.transform.gameObject.CompareTag("Trash"))
                {
                    var hitTrash = raycastHit.transform.gameObject;
                    hitTrash.GetComponent<Grabbable>().Lift();
                }
                else
                {
                    // TODO: https://www.dropbox.com/scl/fi/3fxr9jfh0xvewqip266mq/.paper?dl=0&noframe=0&rlkey=v03pklhqcpltj02hmrc3gfkzo
                }
            }
        }
    }
}