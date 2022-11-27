using UnityEngine;
using UnityEngine.Audio;

namespace Particle
{
    public class ClickMaterialType
    {
        #region clicking
        readonly  InputManager _inputManager = InputManager.Instance;

        public ClickMaterialType()
        {
            if (_inputManager != null)
            {
                _inputManager.ClickEvent += OnClick;
            }
        }

        private void OnClick()
        {
            CliCkUnclickable();
        }
        #endregion
    
        private Color _darkColor;
        private Color _lightColor;
        private AudioMixer _sound;
        private int _numberParticle;
        private void Start()
        {
            var allParticles = Resources.LoadAll<ClickMaterial>("");
            foreach (var particle in allParticles)
            {
                _darkColor = particle.DarkColor;
                _lightColor = particle.LightColor;
                _sound = particle.Sound;
                _numberParticle = particle.NumberParticle;
            }
        }

        private void CliCkUnclickable()
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine ( ray.origin, ray.origin + ray.direction * 100, Color.red );

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform.gameObject.CompareTag("Clickable"))
                {
                    Debug.Log("Good");
                }
            }
        }
    }
}
