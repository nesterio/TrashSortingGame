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
    private float speed = 0.3f;
    
    //// Delegates ////
    private delegate void LiftDelegate();
    private LiftDelegate _liftDelegate;
    //
    private delegate void ReleaseDelegate();
    private ReleaseDelegate _releaseDelegate;
    //
    
    private void Awake()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        
        //TODO: Move this to separate PlayerController class
        if (_rigidbody == null)
        { 
            _rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        //
    }
    
    public void Lift()
    {
        if (_rigidbody.useGravity)
            _rigidbody.useGravity = false;

        _inputManager.MousePosEvent += Move;
        _inputManager.ReleaseEvent += Release;
            
        if (!_holdingSmth)
            _holdingSmth = true;
    }

    public void Release()
    {
        if (!_rigidbody.useGravity)
            _rigidbody.useGravity = true;
        
        _inputManager.MousePosEvent -= Move;
        _inputManager.ReleaseEvent -= Release;
            
        if (_holdingSmth)
            _holdingSmth = false;
    }

    void Move(Vector3 vector3)
    {
        _rigidbody.transform.DOMove(new Vector3(vector3.x, vector3.y, grabHeight), speed);
    }

}
