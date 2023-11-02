using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CharacterCreation : MonoBehaviour
{
    [Header("Male")]
    [SerializeField] private Sprite maleHead;
    [SerializeField] private Sprite maleBody;

    [Header("Female")]
    [SerializeField] private Sprite femaleHead;
    [SerializeField] private Sprite femaleBody;

    [SerializeField] private Sprite maleGenderIcon;
    [SerializeField] private Sprite femaleGenderIcon;

    [Header("Hair & Beard")]
    [SerializeField] private Sprite[] hairs;
    [SerializeField] private Sprite[] beards;

    [Header("Colors")]
    private Color32[] hairColors;
    private Color32[] skinColors;

    [Header("UI Elements")]
    [SerializeField] private Image headImage;
    [SerializeField] private Image bodyImage;
    [SerializeField] private Image hairImage;
    [SerializeField] private Image beardImage;
    [SerializeField] private Image genderIcon;

    [SerializeField] private TMP_Dropdown hairTypeDropdown;
    [SerializeField] private TMP_Dropdown hairColorDropdown;
    [SerializeField] private TMP_Dropdown beardColorDropdown;
    [SerializeField] private TMP_Dropdown beardTypeDropdown;
    [SerializeField] private TMP_Dropdown skinColorDropdown;

    private bool isMale = true;

    private void Start()
    {
        PredefineSkinColors();
        PredefineHairColors();

        ResetVisuals();
        PopulateDropdowns();
    }

    private void PredefineSkinColors()
    {
        Color32[] predefinedSkinColors = new Color32[]
        {
        new Color32(255, 209, 171, 255),
        new Color32(223, 162, 111, 255),
        new Color32(179, 114, 61, 255),
        new Color32(133, 73, 23, 255),
        new Color32(82, 37, 0, 255),
        new Color32(59, 45, 52, 255),
        };
        skinColors = predefinedSkinColors;
    }

    private void PredefineHairColors()
    {
        Color32[] predefinedHairColors = new Color32[]
        {
        new Color32(250, 240, 190, 255), // Blonde
        new Color32(0, 0, 0, 255), // Black
        new Color32(255, 0, 0, 255), // Red
        new Color32(0, 255, 0, 255), // Green
        new Color32(0, 0, 255, 255), // Blue
        };
        hairColors = predefinedHairColors;
    }

    public void SwitchGender()
    {
        isMale = !isMale;

        if (isMale)
        {

            headImage.sprite = maleHead;
            bodyImage.sprite = maleBody;
        }
        else
        {
            headImage.sprite = femaleHead;
            bodyImage.sprite = femaleBody;

            ResetBeard();
        }
    }
    
    private void ResetBeard()
    {
        beardImage.sprite = beards[0];
        beardImage.color = hairColors[0];
        beardTypeDropdown.value = 0;
        beardColorDropdown.value = 0;
    }

    public void RandomizeVisuals()
    {
        if (isMale)
        {
            RandomizeBeard();
        }

        RandomizeHair();
        RandomizeSkinColor();
    }

    private void RandomizeBeard()
    {
        int randomBeardTypeIndex = Random.Range(0, beards.Length);
        beardImage.sprite = beards[randomBeardTypeIndex];
        beardTypeDropdown.value = randomBeardTypeIndex;

        int randomBeardColorIndex = Random.Range(0, hairColors.Length);
        beardImage.color = hairColors[randomBeardColorIndex];
        beardColorDropdown.value = randomBeardColorIndex;
    }

    private void RandomizeHair()
    {
        int randomHairTypeIndex = Random.Range(0, hairs.Length);
        hairImage.sprite = hairs[randomHairTypeIndex];
        hairTypeDropdown.value = randomHairTypeIndex;

        int randomHairColorIndex = Random.Range(0, hairColors.Length);
        hairImage.color = hairColors[randomHairColorIndex];
        hairColorDropdown.value = randomHairColorIndex;
    }

    private void RandomizeSkinColor()
    {
        int randomSkinColorIndex = Random.Range(0, skinColors.Length);
        headImage.color = skinColors[randomSkinColorIndex];
        bodyImage.color = skinColors[randomSkinColorIndex];
        skinColorDropdown.value = randomSkinColorIndex;
    }

    private void ResetVisuals()
    {
        if (isMale)
        {
            headImage.sprite = maleHead;
            bodyImage.sprite = maleBody;
            genderIcon.sprite = maleGenderIcon;
        }
        else
        {
            headImage.sprite = femaleHead;
            bodyImage.sprite = femaleBody;
            genderIcon.sprite = femaleGenderIcon;
        }

        hairImage.sprite = hairs[0];
        beardImage.sprite = beards[0];

        headImage.color = skinColors[0];
        bodyImage.color = skinColors[0];

        hairImage.color = hairColors[0];
        beardImage.color = hairColors[0];
    }

    public void PopulateDropdowns()
    {
        PopulateDropdownOptions(hairTypeDropdown, hairs);
        PopulateDropdownOptions(beardTypeDropdown, beards);
        PopulateDropdownWithColors(skinColorDropdown, skinColors, null);
        PopulateDropdownWithColors(hairColorDropdown, hairColors, hairImage);
        PopulateDropdownWithColors(beardColorDropdown, hairColors, beardImage);
    }

    public void OnSkinColorDropdownValueChanged()
    {
        headImage.color = skinColors[skinColorDropdown.value];
        bodyImage.color = skinColors[skinColorDropdown.value];
    }

    public void OnHairTypeDropdownValueChanged() => hairImage.sprite = hairs[hairTypeDropdown.value];

    public void OnHairColorDropdownValueChanged() => hairImage.color = hairColors[hairColorDropdown.value];

    public void OnBeardTypeDropdownValueChanged() => beardImage.sprite = beards[beardTypeDropdown.value];

    public void OnBeardColorDropdownValueChanged() => beardImage.color = hairColors[beardColorDropdown.value];

    private void PopulateDropdownOptions(TMP_Dropdown dropdown, Sprite[] sprites)
    {
        dropdown.ClearOptions();

        List<string> dropdownOptions = new List<string>();

        foreach (Sprite sprite in sprites)
        {
            string spriteName = sprite.name;
            spriteName = spriteName.Replace("Hair_", "").Replace("Beard_", "").Replace("_Down", "");
            dropdownOptions.Add(spriteName);
        }

        dropdown.AddOptions(dropdownOptions);
        dropdown.value = 0;
    }

    private void PopulateDropdownWithColors(TMP_Dropdown dropdown, Color32[] colors, Image targetImage)
    {
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        foreach (Color32 color in colors)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = ""; // Leave the text empty
            option.image = CreateColoredImage(color);

            dropdownOptions.Add(option);
        }

        dropdown.AddOptions(dropdownOptions);

        if (targetImage != null)
        {
            targetImage.color = colors[0];
        }
    }

    private Sprite CreateColoredImage(Color32 color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();

        Rect rect = new Rect(0, 0, 1, 1);

        return Sprite.Create(texture, rect, Vector2.one * 0.5f);
    }

    public void ConfirmCharacter()
    {
        PlayerSpriteManager spriteManager = PlayerController.Instance.PlayerSpriteManager;

        spriteManager.SetHeadSprite(headImage.sprite, headImage.color);
        spriteManager.SetBodySprite(bodyImage.sprite, bodyImage.color);
        spriteManager.SetHairSprite(hairImage.sprite, hairImage.color);
        spriteManager.SetBeardSprite(beardImage.sprite, beardImage.color);

        GetComponent<RectTransform>().localScale = Vector2.zero;
    }

}
