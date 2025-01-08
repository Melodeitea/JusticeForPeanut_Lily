using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { LargerHoop, DoublePoints }
    public PowerUpType type;
    public float duration = 10f;

    private bool isActive = false;
    private float timer = 0f;

    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                DeactivatePowerUp();
            }
        }
    }

    public void ActivatePowerUp()
    {
        isActive = true;
        switch (type)
        {
            case PowerUpType.LargerHoop:
                GameObject hoop = GameObject.Find("Hoop");
                hoop.transform.localScale *= 1.5f;
                break;

            case PowerUpType.DoublePoints:
                FindObjectOfType<ScoringSystem>().multiplier *= 2;
                break;
        }
    }

    public void DeactivatePowerUp()
    {
        isActive = false;
        if (type == PowerUpType.LargerHoop)
        {
            GameObject hoop = GameObject.Find("Hoop");
            hoop.transform.localScale /= 1.5f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePowerUp();
            Destroy(gameObject); // Remove the power-up object
        }
    }
}
