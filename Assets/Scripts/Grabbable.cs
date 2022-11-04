using DG.Tweening;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isInitialized;

    private bool _holdingSmth;

    private readonly InputManager _inputManager = InputManager.Instance;
    
    private readonly float _speed = 7.5f;

    private Tween _moveTween;

    private void Awake()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        
        if (_rigidbody == null)
        { 
            _rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
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

        if (_holdingSmth)
            _holdingSmth = false;
    }

    void Move(Vector3 vector3)
    {
        _rigidbody.velocity = (vector3 - transform.position) * _speed;
    }
    
    

}
