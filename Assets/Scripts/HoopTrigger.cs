using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public ParticleSystem successEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Instantiate(successEffect, transform.position, Quaternion.identity);
            FindObjectOfType<ScoringSystem>().AddScore(10);
        }
    }
}
