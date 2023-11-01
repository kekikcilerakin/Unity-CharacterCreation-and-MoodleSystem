using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

[Serializable]
public class TranslationData
{
    public Dictionary<string, string> translations = new Dictionary<string, string>();
}

public class Translator : MonoBehaviour
{
    public static Translator Instance { get; private set; }
    public string selectedLanguage;
    private TranslationData translationData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        LoadLanguage("EN");
    }

    public void LoadLanguage(string language)
    {
        selectedLanguage = language;

        string jsonFilePath = "Localization/UI_" + language;

        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);

        if (jsonTextAsset != null)
        {
            translationData = JsonConvert.DeserializeObject<TranslationData>(jsonTextAsset.text);
        }
        else
        {
            Debug.LogError("Failed to load JSON file: " + jsonFilePath);
        }
    }

    public string GetMoodleName(MoodleType moodleType, int level)
    {
        string key = $"moodle_{moodleType.ToString().ToLower()}_level_{level}";
        return GetTranslation(key);
    }

    public string GetMoodleDescription(MoodleType moodleType, int level)
    {
        string key = $"moodle_{moodleType.ToString().ToLower()}_desc_level_{level}";
        return GetTranslation(key);
    }

    private string GetTranslation(string key)
    {
        if (translationData != null && translationData.translations.ContainsKey(key))
        {
            return translationData.translations[key];
        }
        else
        {
            return "Translation Not Found";
        }
    }
}