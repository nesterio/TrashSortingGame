using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;

    private bool _isInitialized;

    protected readonly InputManager _inputManager = InputManager.Instance;
    
    protected readonly float _speed = 7.5f;

    private void Awake() => Initialize();

    private void Initialize()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        
        if (_rigidbody == null)
            _rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
    }
    
    public virtual void Lift()
    {
        _inputManager.MousePosEvent += Move;
        _inputManager.ReleaseEvent += Release;
    }

    public virtual void Release()
    {
        _inputManager.MousePosEvent -= Move;
        _inputManager.ReleaseEvent -= Release;
    }

    void Move(Vector3 vector3) => _rigidbody.velocity = (vector3 - transform.position) * _speed;

}
