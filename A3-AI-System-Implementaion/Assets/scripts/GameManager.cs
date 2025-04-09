using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

//starts new game.
    private void Start()
    {
        player = FindObjectOfType<Player>();

        NewGame();
    }
// resets score, sets starting game speed, disables GameOver text and retry button while playing, spawns player and obstacle spawner, updates the highscore if it is higher then the current one.
    public void NewGame() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        enabled = true;

        player.gameObject.SetActive(true);
        //gameOverText.gameObject.SetActive(false);
        //retryButton.gameObject.SetActive(false);
    }
// functions for a game over, sets game speed to 0, deactivates player and obsctacle spawner, activates GameOver text and retry button, also updates highscore if applicable.
    public void GameOver() 
    {
        player.gameObject.SetActive(false);
       
       //gameOverText.gameObject.SetActive(true);
        //retryButton.gameObject.SetActive(true);
    }
//increases game speed over time, collects score based off speed and distance as well as turns score from a float to a intiger in order to appear on the score text.
    private void Update()
    {
       
    }
    
}
