using UnityEngine;
using UnityEngine.EventSystems;

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
        UpdatePlayerSpriteWithKeyboard(moveDirection);

        // Check if the right mouse button is being held down

        if (Input.GetMouseButton(1))
        {
            UpdatePlayerSpriteWithMouse();
        }
    }

    public void UpdatePlayerSpriteWithKeyboard(Vector2 moveDirection)
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

    private int GetDirectionFromMouse(Vector3 mousePosition)
    {
        Vector2 playerToMouse = (mousePosition - transform.position).normalized;

        if (Mathf.Abs(playerToMouse.x) > Mathf.Abs(playerToMouse.y))
        {
            if (playerToMouse.x > 0)
            {
                return 1; // Right
            }
            else
            {
                return 3; // Left
            }
        }
        else
        {
            if (playerToMouse.y > 0)
            {
                return 0; // Up
            }
            else
            {
                return 2; // Down
            }
        }
    }

    private void UpdatePlayerSpriteWithMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction as an integer
        int direction = GetDirectionFromMouse(mousePosition);

        // Update the sprite based on the direction
        switch (direction)
        {
            case 0:
                spriteRenderer.sprite = upSprite;
                lastMoveDirection = Vector2.up;
                break;
            case 1:
                spriteRenderer.sprite = rightSprite;
                lastMoveDirection = Vector2.right;
                break;
            case 2:
                spriteRenderer.sprite = downSprite;
                lastMoveDirection = Vector2.down;
                break;
            case 3:
                spriteRenderer.sprite = leftSprite;
                lastMoveDirection = Vector2.left;
                break;
            default:
                spriteRenderer.sprite = downSprite; // Default to down sprite
                break;
        }
    }

}
