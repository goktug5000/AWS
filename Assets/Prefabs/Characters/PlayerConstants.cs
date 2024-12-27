using UnityEngine;

public class PlayerConstants : MonoBehaviour
{
    public static GameObject currentPlayer;

    public GameObject playerObj;
    public GameObject playerCanvas;
    public GameObject playerDots;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public GunBasics gunBasics;

    public RectTransform HPRectUi;
    public RectTransform HPRectOnPlayer;

    [SerializeField] private bool isCurrentUser;

    private void Awake()
    {
        // TODO: burda baðlandýðýn id ile kontrol edip singleton yap
        if (isCurrentUser)
        {
            PlayerConstants.currentPlayer = playerObj;
            playerHealth.HPRect = HPRectUi;
            Destroy(playerCanvas);
        }
        else
        {
            Destroy(playerMovement);
            Destroy(gunBasics);
            Destroy(playerDots);

            playerHealth.HPRect = HPRectOnPlayer;
        }
    }
}
