using UnityEngine;

public class HungerAndThirstController : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float hunger = 0.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float thirst = 0.0f;

    private void Update()
    {
        hunger += Time.deltaTime * 0.05f;
        thirst += Time.deltaTime * 0.04f;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hunger = 0.0f;
            thirst = 0.0f;
        }

    }

    public float GetHunger()
    {
        return hunger;
    }

    public float GetThirst()
    {
        return thirst;
    }


}
