using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public static DoNotDestroy Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy the new one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this as the permanent instance and protect it from scene loads
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}