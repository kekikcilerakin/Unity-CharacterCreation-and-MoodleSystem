using UnityEngine;

public class MoodleDisplayUI : MonoBehaviour
{
    [SerializeField] private GameObject moodlePrefab;

    public GameObject CreateMoodleObject()
    {
        GameObject moodleObject = Instantiate(moodlePrefab, transform);
        return moodleObject;
    }
}
