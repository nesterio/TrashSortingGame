using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashRecievingWindow : MonoBehaviour
{
    [SerializeField] Collider trashBox;

    private float initalDuration = 3;
    private float currentDuration;
    private int level = 1;

    public TrashType trashType;

    private void Start()
    {
        currentDuration = initalDuration - initalDuration * level / 10;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Trash") //Add a click check
        {
            if (col.gameObject.tag == "Trash")
            {
                Destroy(col.gameObject);
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
        yield return new WaitForSeconds(currentDuration);
        Debug.Log("Box ready");
        trashBox.isTrigger = true;
    }
}