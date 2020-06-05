using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Observer
{
    private UIController uiController = null;
    private PlayerController player = null;
    private PowerBase1 firstPowerBase = null;
    private PowerBase2 secondPowerBase = null;
    private TerminalBase1 firstTerminal = null;
    private TerminalBase2 secondTerminal = null;
    private EnemyController[] enemies = null;
    private SpaceShip ship = null;
    private OpenGate secondGate = null;
    private GameMusic gameMusic = null;
    private EndZone endZone = null;
    private int enemyKillCount = 0;

    #region SINGLETON IMPLEMENTATION
    private static GameManager _instance = null;

    // Global accessor so other scripts can be fed information as relevant.
    public static GameManager Instance
    {
        get
        {
            // If an instance can't be found...
            if (_instance == null)
            {
                // ... search in the scene to be sure it really is null.
                _instance = GameObject.FindObjectOfType<GameManager>();

                // If the instance is STILL null...
                if (_instance == null)
                {
                    // ... create a new GameManager instance and pass that along.
                    GameObject newGameManager = new GameObject("_GameManager");
                    _instance = newGameManager.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    #region GETTERS
    public int GetBuildIndex()
    {
        return FetchCurrentSceneIndex();
    }
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;

        uiController = GameObject.FindObjectOfType<UIController>();
        uiController.AddObserver(this);
    }

    private void Update()
    {
        if(FetchCurrentSceneIndex() != 0)
        {
            uiController.DisplayHealthStats(player.GetHealth());
            uiController.DisplayStaminaStats(player.GetStamina());
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            uiController.OnNotify(NotificationType.SECOND_CHECKPOINT_DONE);
            secondGate.SetCompleteKills(true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(4);
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            enemyKillCount = 18;
            print("you won");
            uiController.OnNotify(NotificationType.WIN_GAME);
        }
    }

    public override void OnNotify(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.START_GAME:
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                break;

            case NotificationType.QUIT_GAME:
                Application.Quit();
                break;

            case NotificationType.PLAYER_DEAD:
                uiController.OnNotify(NotificationType.GAME_OVER);
                gameMusic.OnNotify(NotificationType.GAME_OVER);
                break;

            case NotificationType.FIRST_CHECKPOINT_DONE:
                uiController.OnNotify(NotificationType.FIRST_CHECKPOINT_DONE);
                break;

            case NotificationType.POWERBASE_1_DONE:
                uiController.OnNotify(NotificationType.POWERBASE_1_DONE);
                firstTerminal.OnNotify(NotificationType.POWERBASE_1_DONE);
                break;

            case NotificationType.POWERBASE_2_DONE:
                uiController.OnNotify(NotificationType.POWERBASE_2_DONE);
                secondTerminal.OnNotify(NotificationType.POWERBASE_2_DONE);
                break;

            case NotificationType.TERMINAL_1_DONE:
                uiController.OnNotify(NotificationType.TERMINAL_1_DONE);
                ship.OnNotify(NotificationType.TERMINAL_1_DONE);
                break;

            case NotificationType.TERMINAL_2_DONE:
                uiController.OnNotify(NotificationType.TERMINAL_2_DONE);
                ship.OnNotify(NotificationType.TERMINAL_2_DONE);
                break;

            case NotificationType.LEVEL1_COMPLETE:
                uiController.OnNotify(NotificationType.LEVEL1_COMPLETE);
                gameMusic.OnNotify(NotificationType.LEVEL1_COMPLETE);
                SceneManager.LoadScene(2, LoadSceneMode.Single);
                break;

            case NotificationType.LEVEL_2_BEGIN_COMPLETED:
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                break;

            case NotificationType.LEVEL2_COMPLETE:
                print("FUCK EVERYTHING");
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                break;

            case NotificationType.ENEMY_DEAD:
                enemyKillCount++;

                if(enemyKillCount == 4 && GetBuildIndex() == 1)
                {
                    uiController.OnNotify(NotificationType.SECOND_CHECKPOINT_DONE);
                    gameMusic.OnNotify(NotificationType.SECOND_CHECKPOINT_DONE);
                    secondGate.SetCompleteKills(true);
                }
                if (enemyKillCount == 18 && GetBuildIndex() == 4)
                {
                    print("you won");
                    uiController.OnNotify(NotificationType.WIN_GAME);
                }
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1:
                uiController.OnNotify(NotificationType.LEVEL1_START);
                Debug.Log("[Scene 1 Loaded]");
                ChickenCoup firstPuzzle = GameObject.FindObjectOfType<ChickenCoup>();
                firstPuzzle.AddObserver(this);
                secondGate = GameObject.FindObjectOfType<OpenGate>();
                enemies = GameObject.FindObjectsOfType<EnemyController>();
                player = GameObject.FindObjectOfType<PlayerController>();
                player.AddObserver(this);
                firstPowerBase = GameObject.FindObjectOfType<PowerBase1>();
                secondPowerBase = GameObject.FindObjectOfType<PowerBase2>();
                firstTerminal = GameObject.FindObjectOfType<TerminalBase1>();
                secondTerminal = GameObject.FindObjectOfType<TerminalBase2>();
                ship = GameObject.FindObjectOfType<SpaceShip>();
                gameMusic = GameObject.FindObjectOfType<GameMusic>();
                firstPowerBase.AddObserver(this);
                secondPowerBase.AddObserver(this);
                firstTerminal.AddObserver(this);
                secondTerminal.AddObserver(this);
                ship.AddObserver(this);
                gameMusic.AddObserver(this);
                print(enemies.Length);
                gameMusic.OnNotify(NotificationType.LEVEL1_START);
                foreach(EnemyController enemy in enemies)
                {
                    print(enemy.name);
                    enemy.AddObserver(this);
                }
                break;
            case 2:
                uiController.OnNotify(NotificationType.LEVEL2_START);
                Debug.Log("[Scene 2 Loaded]");
                player = GameObject.FindObjectOfType<PlayerController>();
                player.AddObserver(this);
                endZone = GameObject.FindObjectOfType<EndZone>();
                endZone.AddObserver(this);
                break;
            case 3:
                uiController.OnNotify(NotificationType.PART_2_START);
                Debug.Log("[Scene 3 Loaded]");
                player = GameObject.FindObjectOfType<PlayerController>();
                player.AddObserver(this);
                EndZone waterLevelEndZone = GameObject.FindObjectOfType<EndZone>();
                waterLevelEndZone.AddObserver(this);
                break;
            case 4:
                uiController.OnNotify(NotificationType.PART_3_START);
                Debug.Log("[Scene 4 Loaded]");
                player.AddObserver(this);
                enemies = GameObject.FindObjectsOfType<EnemyController>();
                foreach (EnemyController enemy in enemies)
                {
                    print(enemy.name);
                    enemy.AddObserver(this);
                }
                enemyKillCount = 0;
                break;
        }
    }

    private int FetchCurrentSceneIndex()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.buildIndex;
    }
}
