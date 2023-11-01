using System.Collections.Generic;
using UnityEngine;

public class MoodleManager : MonoBehaviour
{
    public static MoodleManager Instance { get; private set; }

    [SerializeField] private List<Moodle> moodles = new List<Moodle>();
    [SerializeField] private MoodleDisplayUI moodleDisplayUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateInitialMoodles();
    }

    private void GenerateInitialMoodles()
    {
        AddMoodle(MoodleType.Hunger);
        AddMoodle(MoodleType.Thirst);
    }

    public void AddMoodle(MoodleType type)
    {
        GameObject moodleObject = moodleDisplayUI.CreateMoodleObject();
        Moodle moodleComponent = moodleObject.GetComponent<Moodle>();

        moodleComponent.SetMoodleType(type);
        moodleComponent.name = $"Moodle ({type})";

        moodles.Add(moodleComponent);
    }
}
