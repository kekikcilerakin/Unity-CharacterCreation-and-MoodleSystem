using System;
using UnityEngine;
using UnityEngine.UI;

public class MoodleUI : MonoBehaviour
{
    public Image moodleBackground;
    public Image moodleIcon;

    private Moodle moodle;

    public void Start()
    {
        moodle = GetComponent<Moodle>();
        moodle.OnLevelChanged += Moodle_OnLevelChanged;

        HideMoodle();
    }

    private void Moodle_OnLevelChanged(object sender, EventArgs e)
    {
        int moodleLevel = moodle.GetLevel();

        if (moodleLevel == 0)
        {
            HideMoodle();
        }
        else
        {
            ShowMoodle();

            Debug.Log(moodleLevel);
            moodleBackground.sprite = MoodleUIDatabase.Instance.GetBackground(moodleLevel);
            moodleIcon.sprite = Moodle.GetIcon(moodle.GetMoodleType());
        }
    }


    private void ShowMoodle()
    {
        GetComponent<LayoutElement>().preferredHeight = 64;
    }

    private void HideMoodle()
    {

        GetComponent<LayoutElement>().preferredHeight = 0;
    }
}
