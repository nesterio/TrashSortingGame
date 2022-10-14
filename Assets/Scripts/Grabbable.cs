using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class Grabbable : MonoBehaviour
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

    // Update is called once per frame
    protected void Update()
    {
        if(_isInitialized == false)
            return;
        
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            if (_holdingSmth == true)
                TryDetectItem();
            else
                _releaseDelegate?.Invoke();
        } 
        
    }

    private void TryDetectItem() // TODO: https://www.dropbox.com/scl/fi/3fxr9jfh0xvewqip266mq/.paper?dl=0&noframe=0&rlkey=v03pklhqcpltj02hmrc3gfkzo
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null 
                && raycastHit.transform.gameObject.CompareTag("Trash")) // TODO: add tag Trash
            {
                if (raycastHit.transform.gameObject == gameObject)
                    _liftDelegate?.Invoke();
            }
        }
    }

}
