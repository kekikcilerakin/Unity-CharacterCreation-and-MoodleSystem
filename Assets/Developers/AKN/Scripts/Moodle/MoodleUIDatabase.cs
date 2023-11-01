using UnityEngine;

public class MoodleUIDatabase : MonoBehaviour
{
    public static MoodleUIDatabase Instance { get; private set; }

    [Header("Moodle Icons")]
    public Sprite HungerIcon;
    public Sprite ThirstIcon;

    [Header("Backgrounds")]
    public Sprite[] Backgrounds = new Sprite[4];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Sprite GetBackground(int level)
    {
        if (level >= 0 && level <= Backgrounds.Length)
        {
            return Backgrounds[level - 1];
        }

        return null;
    }
}
