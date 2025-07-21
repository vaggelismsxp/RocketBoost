using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed ;

    private float  zLimit = 12f ;
    private int direction=1;

    private void Update()
    {
        
        
        transform.Translate(Vector3.forward * moveSpeed * direction *  Time.deltaTime);

        if (transform.position.z >= zLimit || transform.position.z < -zLimit)
        {
            Debug.Log("z :" + transform.position.z);
            moveSpeed *= -1;
        }

    }






}
