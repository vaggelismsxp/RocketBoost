using UnityEngine;

public class ObstacleUpAndDown : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    private float direction=1;
    private float yLimit = 30;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * direction * Time.fixedDeltaTime);
        if (transform.position.y >= yLimit || transform.position.y <= -yLimit)
        {
            direction *= -1;
        }
    }
}
