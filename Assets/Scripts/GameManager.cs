using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ensure that this script is executed before all other scripts
[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    // Implement a singleton pattern
    // Singleton means that there can only be one GameManager instance at a time
    public static GameManager Instance { get; private set; }

    // Declaration of other variables and references
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public AnimatedSprite animator;
    public float gameSpeed { get; private set; }

    // UI Elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;

    // References to the Player and Spawner objects
    private Player player;
    private Spawner spawner;

    // Stores the score of the current run
    private float score;

    // Called first of all (when the scripts are being loaded)
    private void Awake()
    {
        // Ensures that only instance of the singleton is present
        if (Instance != null) {
            // Some other instance is present, so destroy the current instance
            DestroyImmediate(gameObject);
        } else {
            // No other instance is present, so set Instance to the current instance
            Instance = this;
        }
    }

    // Called when the scene ends
    private void OnDestroy()
    {
        // Destroy the instance
        if (Instance == this) {
            Instance = null;
        }
    }

    // Called once after the script is enabled
    private void Start()
    {
        // Obtain references to the Player and Spawner objects
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        // Start a new game
        NewGame();
    }

    // Custom method to start a new game
    public void NewGame()
    {
        // Array of obstacles present in the game currently
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        // Destroy all currently existing obstacles
        foreach (var obstacle in obstacles) 
        {
            Destroy(obstacle.gameObject);
        }

        // Initialize score and gameSpeed
        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        // Activate the Player and Spawner because the game has started
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        // Deactive the game over text and retry button as the game is going on
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        // Update the high score after a run has started
        UpdateHiscore();
    }

    // Custom method to end a run after dying
    public void GameOver()
    {
        // Stop the game
        gameSpeed = 0f;
        enabled = false;

        // If the player was crouching while dying, make them stop crouching when a new game is started
        animator.isCrouching = false;

        // Deactivate the player and spawner
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        // Activate the game over text and retry button
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        // Update the high score after a run has ended
        UpdateHiscore();
    }

    private void Update()
    {
        // Increase gameSpeed with time
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        
        // Update current score
        score += gameSpeed * Time.deltaTime;

        // Convert the current score to a string for displaying
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHiscore()
    {
        // Get the current high score from PlayerPrefs
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        // Update the high score if the current score is greater than the current high score
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        // Convert the highscore to a string for displaying it
        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

}
