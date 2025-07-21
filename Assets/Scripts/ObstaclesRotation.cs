using UnityEngine;

public class ObstaclesRotation : MonoBehaviour
{
    [SerializeField] float xAngle;
    [SerializeField] float yAngle;
    [SerializeField] float zAngle;

    
    void Update()
    {
        transform.Rotate(xAngle, yAngle, zAngle);
        
    }
}
