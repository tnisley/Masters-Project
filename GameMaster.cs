using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    [SerializeField]
    GameObject pauseMenu;
    public Player player;


    public static int totalScore; // score from previous levels.
    public int levelScore;  // total score including this level. 
    public int gemsRemaining;
    public int currentHealth;

    private const int GEM_BONUS = 5000;

    public GameObject gemContainer; // Parent object of all gems


    private void Awake()
    {
        GameEvents.OnRestartLevel.AddListener(ReloadScene);
        GameEvents.OnGetCollectible.AddListener(GetCollectible);
        InputHandler.OnPauseButton.AddListener(Pause);
        InputHandler.OnUnpauseButton.AddListener(Unpause);
        GameEvents.OnPlayerExitLevel.AddListener(CompleteLevel);
        GameEvents.OnQuitGame.AddListener(Quit);

    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeLevel();

        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    // Handles the player getting a collectible. Updates score 
    // then destroys the collectible object.
    public void GetCollectible(CollectibleData itemData)
    {
        levelScore += itemData.pointValue;

        switch (itemData.itemType)
        {
            case CollectibleData.Type.GEM:
                gemsRemaining -= 1;
                if (gemsRemaining == 0)
                {
                    Debug.Log("Got all gems");
                    levelScore += GEM_BONUS;
                    GameEvents.OnAllGemsCollected.Invoke();
                }
                GameEvents.OnGemsUpdate.Invoke(gemsRemaining);
                break;
        }

        GameEvents.OnScoreUpdate.Invoke(levelScore);
    }

    // Call when player goes through the level exit. Loads next level.
    public void CompleteLevel()
    {
        totalScore = levelScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Reloads the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected void InitializeLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level01")
            totalScore = 0;

        // Reset score
        levelScore = totalScore;
        GameEvents.OnScoreUpdate.Invoke(levelScore);

        // Reset Gems Remaining
        gemsRemaining = gemContainer.transform.childCount;
        GameEvents.OnGemsUpdate.Invoke(gemsRemaining);
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    private void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        GameEvents.OnRestartLevel.RemoveListener(ReloadScene);
        GameEvents.OnGetCollectible.RemoveListener(GetCollectible);
        InputHandler.OnPauseButton.RemoveListener(Pause);
        InputHandler.OnUnpauseButton.RemoveListener(Unpause);
        GameEvents.OnPlayerExitLevel.RemoveListener(CompleteLevel);
        GameEvents.OnQuitGame.RemoveListener(Quit);


    }
}