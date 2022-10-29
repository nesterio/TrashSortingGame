using UnityEngine;
using Scripts.Clicking;
public class MainManager : MonoBehaviour
{
    private ClickDetector ClickDetector;
    
    void Start()
    {
        ClickDetector = new ClickDetector();
    }
}
