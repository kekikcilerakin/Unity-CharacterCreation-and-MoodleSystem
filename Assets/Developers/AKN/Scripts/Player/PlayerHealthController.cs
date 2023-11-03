using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private float maxHealth = 100.0f;
    private float curHealth = 0.0f;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return curHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
    }
}
