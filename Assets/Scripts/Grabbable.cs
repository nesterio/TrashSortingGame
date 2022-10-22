using DG.Tweening;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isInitialized;

    private bool _holdingSmth;

    private InputManager _inputManager = InputManager.Instance;

    float grabHeight = -1.7f;
    private float speed = 1f;

    private Tween _moveTween;

    private void Awake()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        
        //TODO: ?? Move this to separate PlayerController class ??
        if (_rigidbody == null)
        { 
            _rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        //
    }
    
    public void Lift()
    {
        _inputManager.MousePosEvent += Move;
        _inputManager.ReleaseEvent += Release;
            
        if (!_holdingSmth)
            _holdingSmth = true;
    }

    private void Release()
    {
        _inputManager.MousePosEvent -= Move;
        _inputManager.ReleaseEvent -= Release;

        _moveTween.Kill(); // NOT WORKING NEEDS FIX
            
        if (_holdingSmth)
            _holdingSmth = false;
    }

    void Move(Vector3 vector3)
    {
        _moveTween = _rigidbody.transform.DOMove(new Vector3(vector3.x, vector3.y, grabHeight), speed);
    }
    
    

}
