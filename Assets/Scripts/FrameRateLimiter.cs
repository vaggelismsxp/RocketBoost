using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 120; // Set your desired FPS
    }

    
}