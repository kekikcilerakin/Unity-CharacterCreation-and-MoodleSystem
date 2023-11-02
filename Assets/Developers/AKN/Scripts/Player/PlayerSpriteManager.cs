using UnityEngine;

public enum FacingDirection
{
    Up,
    Right,
    Down,
    Left
}

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer headSpriteRenderer;
    [SerializeField] private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private SpriteRenderer hairSpriteRenderer;
    [SerializeField] private SpriteRenderer beardSpriteRenderer;

    private Sprite headSprite;
    private Sprite bodySprite;
    private Sprite hairSprite;
    private Sprite beardSprite;

    private string currentHeadSpriteName;
    private string currentBodySpriteName;
    private string currentHairSpriteName;
    private string currentBeardSpriteName;

    private FacingDirection lastDirection;
    private Vector2 currentMoveDirection;

    private void Start()
    {
        lastDirection = FacingDirection.Down;
    }

    private void Update()
    {
        Vector2 moveDirection = PlayerController.Instance.PlayerInput.GetMovementInput();

        if (moveDirection != Vector2.zero)
        {
            currentMoveDirection = moveDirection;
            lastDirection = GetFacingDirection(currentMoveDirection);
        }

        HandleMouseDirection();

        UpdateSprites();

    }

    private void HandleMouseDirection()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z-component to zero

            // Calculate the direction from the player to the mouse
            Vector2 directionToMouse = (Vector2)(mousePosition - transform.position);

            // Update the lastDirection based on the mouse direction
            lastDirection = GetFacingDirection(directionToMouse);
        }
    }

    private void UpdateSprites()
    {
        if (bodySprite == null) return;

        string newHeadSpriteName = $"{headSprite.name.Replace("_Down", "")}_{lastDirection}";
        string newBodySpriteName = $"{bodySprite.name.Replace("_Down", "")}_{lastDirection}";
        string newHairSpriteName = $"{hairSprite.name.Replace("_Down", "")}_{lastDirection}";
        string newBeardSpriteName = $"{beardSprite.name.Replace("_Down", "")}_{lastDirection}";

        if (newHeadSpriteName != currentHeadSpriteName ||
            newBodySpriteName != currentBodySpriteName ||
            newHairSpriteName != currentHairSpriteName ||
            newBeardSpriteName != currentBeardSpriteName)
        {
            // Only update when there's a change in the sprite names
            currentHeadSpriteName = newHeadSpriteName;
            currentBodySpriteName = newBodySpriteName;
            currentHairSpriteName = newHairSpriteName;
            currentBeardSpriteName = newBeardSpriteName;

            headSpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Heads/" + currentHeadSpriteName);
            bodySpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Bodies/" + currentBodySpriteName);
            hairSpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Hairs/" + currentHairSpriteName);
            beardSpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Beards/" + currentBeardSpriteName);

            if (lastDirection == FacingDirection.Left)
            {
                string rightHeadSpriteName = $"{headSprite.name.Replace("_Down", "")}_Right";
                string rightBodySpriteName = $"{bodySprite.name.Replace("_Down", "")}_Right";
                string rightHairSpriteName = $"{hairSprite.name.Replace("_Down", "")}_Right";
                string rightBeardSpriteName = $"{beardSprite.name.Replace("_Down", "")}_Right";

                headSpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Heads/" + rightHeadSpriteName);
                bodySpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Bodies/" + rightBodySpriteName);
                hairSpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Hairs/" + rightHairSpriteName);
                beardSpriteRenderer.sprite = Resources.Load<Sprite>("CharacterCreation/Beards/" + rightBeardSpriteName);

                headSpriteRenderer.flipX = true;
                bodySpriteRenderer.flipX = true;
                hairSpriteRenderer.flipX = true;
                beardSpriteRenderer.flipX = true;
            }
            else
            {
                headSpriteRenderer.flipX = false;
                bodySpriteRenderer.flipX = false;
                hairSpriteRenderer.flipX = false;
                beardSpriteRenderer.flipX = false;
            }
        }
    }

    public FacingDirection GetFacingDirection(Vector2 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle <= 45)
            return FacingDirection.Right;
        else if (angle > 45 && angle <= 135)
            return FacingDirection.Up;
        else if (angle > 135 || angle <= -135)
            return FacingDirection.Left;
        else
            return FacingDirection.Down;
    }

    public void SetHeadSprite(Sprite sprite, Color32 color)
    {
        headSprite = sprite;
        headSpriteRenderer.sprite = sprite;
        headSpriteRenderer.color = color;
    }
    public void SetBodySprite(Sprite sprite, Color32 color)
    {
        bodySprite = sprite;
        bodySpriteRenderer.sprite = sprite;
        bodySpriteRenderer.color = color;
    }
    public void SetHairSprite(Sprite sprite, Color32 color)
    {
        hairSprite = sprite;
        hairSpriteRenderer.sprite = sprite;
        hairSpriteRenderer.color = color;
    }
    public void SetBeardSprite(Sprite sprite, Color32 color)
    {
        beardSprite = sprite;
        beardSpriteRenderer.sprite = sprite;
        beardSpriteRenderer.color = color;
    }
}
