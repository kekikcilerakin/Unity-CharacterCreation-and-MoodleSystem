using UnityEngine;

public class HungerAndThirstController : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float hungerLevel = 0.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float thirstLevel = 0.0f;

    private void Update()
    {
        hungerLevel += Time.deltaTime * 0.05f;
        thirstLevel += Time.deltaTime * 0.04f;
    }

    public float GetHungerLevel()
    {
        return hungerLevel;
    }

    public float GetThirstLevel()
    {
        return thirstLevel;
    }
}
