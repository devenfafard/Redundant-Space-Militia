using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Observer
{
    private UIController uiController = null;
    private EnemyController[] enemies = null;
    private OpenGate secondGate = null;
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
        // TODO - update health + stamina UI

        // Dev workaround to demo level 2
        if (Input.GetKeyDown(KeyCode.X))
        {
            uiController.OnNotify(NotificationType.SECOND_CHECKPOINT_DONE);
            secondGate.SetCompleteKills(true);
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
                uiController.OnNotify(NotificationType.PLAYER_DEAD);
                break;

            case NotificationType.FIRST_CHECKPOINT_DONE:
                uiController.OnNotify(NotificationType.FIRST_CHECKPOINT_DONE);
                break;

            case NotificationType.ENEMY_DEAD:
                enemyKillCount++;
                print("ENEMIES KILLED : " + enemyKillCount);

                if(enemyKillCount == 4)
                {
                    uiController.OnNotify(NotificationType.SECOND_CHECKPOINT_DONE);
                    secondGate.SetCompleteKills(true);
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
                print(enemies.Length);
                foreach(EnemyController enemy in enemies)
                {
                    print(enemy.name);
                    enemy.AddObserver(this);
                }
                break;
            case 2:
                uiController.OnNotify(NotificationType.LEVEL2_START);
                break;
        }
    }

    private int FetchCurrentSceneIndex()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.buildIndex;
    }
}
