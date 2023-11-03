using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
 
    public HungerAndThirstController HungerAndThirstController { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public PlayerSpriteManager PlayerSpriteManager { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

    public PlayerHealthController PlayerHealthController { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        HungerAndThirstController = GetComponent<HungerAndThirstController>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerSpriteManager = GetComponent<PlayerSpriteManager>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealthController = GetComponent<PlayerHealthController>();
    }
}
