using System.Collections;
using Managers;
using Types;
using UnityEngine;

public class TrashRecievingWindow : MonoBehaviour
{
    private readonly int difficulty = 1;
    private float initalDuration = 3;
    private float CurrentDuration => initalDuration - initalDuration * difficulty / 10;
    
    public TrashType trashType;
    private bool _isAvailable = true;

    private void OnTriggerEnter(Collider col)
    {
        if(_isAvailable == false)
            return;
        
        if (col.gameObject == MainManager.HoldedObject 
            && MainManager.HoldedObject.CompareTag("Trash"))
            InputManager.Instance.ReleaseEvent += ProcessObject;
        else
            Break();
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject == MainManager.HoldedObject 
            && MainManager.HoldedObject.CompareTag("Trash"))
            InputManager.Instance.ReleaseEvent -= ProcessObject;
    }

    private void ProcessObject()
    {
        if(MainManager.HoldedObject == null)
            return;
        
        var trash = MainManager.HoldedObject.GetComponent<Trash>();

        if (trash.trashType == trashType)
            AcceptTrash();
        else
            Break();
    }

    private void AcceptTrash()
    {
        _isAvailable = false;
            
        MainManager.HoldedObject.SetActive(false);
        ScoreManager.Instance.AddScore();
        
        StartCoroutine(TrashProcessingTimer());
    }

    private void Break()
    {
        // TODO
        _isAvailable = false;
        StartCoroutine(TrashProcessingTimer());
    }

    IEnumerator TrashProcessingTimer()
    {
        yield return new WaitForSeconds(CurrentDuration);
        _isAvailable = true;
    }
}