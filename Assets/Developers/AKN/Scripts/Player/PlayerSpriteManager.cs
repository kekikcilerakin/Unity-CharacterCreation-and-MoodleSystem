using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;

    private Vector2 lastMoveDirection;

    private void Update()
    {
        Vector2 moveDirection = PlayerController.Instance.PlayerInput.GetMovementInput();
        UpdatePlayerSprite(moveDirection);
    }

    public void UpdatePlayerSprite(Vector2 moveDirection)
    {
        Vector2 newDirection = GetDirectionFromInput(moveDirection);
        spriteRenderer.sprite = GetSpriteFromDirection(newDirection);
        lastMoveDirection = newDirection;
    }

    private Vector2 GetDirectionFromInput(Vector2 input)
    {
        float absHorizontal = Mathf.Abs(input.x);
        float absVertical = Mathf.Abs(input.y);

        if (absHorizontal > absVertical)
        {
            return new Vector2(Mathf.Sign(input.x), 0);
        }
        else if (absVertical > absHorizontal)
        {
            return new Vector2(0, Mathf.Sign(input.y));
        }
        return lastMoveDirection;
    }

    private Sprite GetSpriteFromDirection(Vector2 direction)
    {
        if (direction == Vector2.right) return rightSprite;
        if (direction == Vector2.left) return leftSprite;
        if (direction == Vector2.up) return upSprite;
        if (direction == Vector2.down) return downSprite;

        return downSprite; // Default to down sprite if no match
    }
}
