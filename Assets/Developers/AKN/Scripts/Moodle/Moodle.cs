using System;
using UnityEngine;

public enum MoodleType
{
    None,
    Hunger,
    Thirst,
}

public class Moodle : MonoBehaviour
{
    [SerializeField] private MoodleType type;
    [SerializeField] private int level;

    public event EventHandler OnLevelChanged;

    public static string GetName(MoodleType moodleType, int moodleLevel)
    {
        if (moodleLevel == 0)
        {
            return "Invalid Moodle Level";
        }

        string moodleName = Translator.Instance.GetMoodleName(moodleType, moodleLevel);
        return string.IsNullOrEmpty(moodleName) ? "Unknown Moodle Type" : moodleName;
    }

    public static string GetDescription(MoodleType moodleType, int moodleLevel)
    {
        if (moodleLevel == 0)
        {
            return "Invalid Moodle Level";
        }

        string moodleDescription = Translator.Instance.GetMoodleDescription(moodleType, moodleLevel);
        return string.IsNullOrEmpty(moodleDescription) ? "Unknown Moodle Type" : moodleDescription;
    }

    public static Sprite GetIcon(MoodleType moodleType)
    {
        if (moodleType == MoodleType.Hunger)
        {
            return MoodleUIDatabase.Instance.HungerIcon;
        }

        if (moodleType == MoodleType.Thirst)
        {
            return MoodleUIDatabase.Instance.ThirstIcon;
        }

        return null;
    }

    public static Sprite GetBackground(int moodleLevel)
    {
        if (moodleLevel <= 0 || moodleLevel > 4)
        {
            return null;
        }

        return MoodleUIDatabase.Instance.GetBackground(moodleLevel);
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int newLevel)
    {
        if (level == newLevel) return;

        newLevel = Mathf.Clamp(newLevel, 0, 4);
        level = newLevel;

        OnLevelChanged?.Invoke(this, EventArgs.Empty);
    }

    public MoodleType GetMoodleType()
    {
        return type;
    }

    public void SetMoodleType(MoodleType moodleType)
    {
        type = moodleType;
    }

    private void Update()
    {
        //if player health <= 0 return

        if (type == MoodleType.Hunger)
        {
            UpdateMoodleLevel(PlayerController.Instance.HungerAndThirstController.GetHunger(), 0.70f, 0.45f, 0.25f, 0.15f);
        }

        if (type == MoodleType.Thirst)
        {
            UpdateMoodleLevel(PlayerController.Instance.HungerAndThirstController.GetThirst(), 0.84f, 0.70f, 0.25f, 0.12f);
        }

        ApplyEffect();
    }

    private void UpdateMoodleLevel(float currentValue, float level4Threshold, float level3Threshold, float level2Threshold, float level1Threshold)
    {
        if (currentValue > level4Threshold) SetLevel(4);
        else if (currentValue > level3Threshold) SetLevel(3);
        else if (currentValue > level2Threshold) SetLevel(2);
        else if (currentValue > level1Threshold) SetLevel(1);
        else if (currentValue > 0.00f) SetLevel(0);
    }

    private void ApplyEffect()
    {
        if (type == MoodleType.Hunger)
        {
            switch (level)
            {
                case 1:
                    PlayerController.Instance.PlayerMovement.ReduceMovementSpeed(0.1f);
                    break;
                case 2:
                    PlayerController.Instance.PlayerMovement.ReduceMovementSpeed(0.3f);
                    break;
                case 3:
                    PlayerController.Instance.PlayerMovement.ReduceMovementSpeed(0.5f);
                    break;
                case 4:
                    PlayerController.Instance.PlayerMovement.ReduceMovementSpeed(0.8f);
                    break;
                default:
                    PlayerController.Instance.PlayerMovement.ReduceMovementSpeed(0.0f);
                    break;
            }
        }
    }
}