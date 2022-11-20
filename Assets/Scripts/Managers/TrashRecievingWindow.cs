using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashRecievingWindow : MonoBehaviour
{
    [SerializeField] Collider trashBox;

    private float initalDuration = 3;
    private float _currentDuration;
    private int level = 1;

    public TrashType trashType;

    private void Start()
    {
        _currentDuration = initalDuration - initalDuration * level / 10;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Trash") //Add a click check
        {
            if (col.gameObject.CompareTag("Trash"))
            {
                col.gameObject.SetActive(false);
                trashBox.isTrigger = false;
                StartCoroutine(TimerTrashBox());
                Debug.Log("Is trash");

            }
            else
            {
                Debug.Log("Error");
            }
        }
        else
        {
            Debug.Log("Error");
        }

    }
    IEnumerator TimerTrashBox()
    {
        ScoreManager.Instance.AddScore();
        yield return new WaitForSeconds(_currentDuration);
        Debug.Log("Box ready");
        trashBox.isTrigger = true;
    }
}