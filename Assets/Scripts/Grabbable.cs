using UnityEngine;

public class Grabbable : MonoBehaviour //TODO: Improve performance by checking if someone is subscribed to a variable before reading input
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isInitialized;

    private bool _holdingSmth;
    
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
        FillDelegates();
        
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        
        //TODO: Move this to separate PlayerController class
        if (_rigidbody == null)
        { 
            _rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        //
    }

    protected virtual void FillDelegates()
    {
        _liftDelegate += () =>
        {
            if (_rigidbody.useGravity)
                _rigidbody.useGravity = false;
            
            if (!_holdingSmth)
                _holdingSmth = true;
        };

        _releaseDelegate += () =>
        {
            if (!_rigidbody.useGravity)
                _rigidbody.useGravity = true;
            
            if (_holdingSmth)
                _holdingSmth = false;
        };
    }
    

}
