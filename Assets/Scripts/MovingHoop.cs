using UnityEngine;

public class MovingHoop : MonoBehaviour
{
    public float speed = 2f;
    public float range = 3f;

    private float startPosition;

    void Start()
    {
        startPosition = transform.position.x;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, range * 2) - range;
        transform.position = new Vector3(startPosition + movement, transform.position.y, transform.position.z);
    }
}
