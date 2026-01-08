using UnityEngine;

public class SimpleSingleton : MonoBehaviour
{
    public static SimpleSingleton Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    } 
    
    
}
