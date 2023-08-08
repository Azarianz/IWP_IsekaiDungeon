using System;
using UnityEngine;

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; set; }

    public Flock playerTeam;
    public Flock[] enemies;

    public UI_DungeonSetup setupUI;
    public UI_DungeonGame gameUI;

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.PlayerSetup);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.PlayerSetup:
                gameUI.ShowUI(false);
                setupUI.ShowUI(true);
                PlayerSetup();
                break;
            case GameState.GameStart:
                gameUI.ShowUI(true);
                setupUI.ShowUI(false);
                GameUpdate();
                break;
            case GameState.Paused:
                Paused();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.PlayerSetup:
                PlayerSetup();
                break;
            case GameState.GameStart:
                GameUpdate();
                break;
            case GameState.Paused:
                Paused();
                break;
            case GameState.Win:
                GameWin();
                break;
            case GameState.Lose:
                GameLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(State), State, null);
        }
    }

    public void PlayerSetup() {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        
        //ChangeState(GameState.GameStart);
    }

    public void GameStart()
    {
        ChangeState(GameState.GameStart);
    }

    public void GameUpdate()
    {
        //Debug.Log("Game Update");

        if (playerTeam.IsFlockDead())
        {
            ChangeState(GameState.Lose);
        }
        else if (enemies[enemies.Length - 1].IsFlockDead())
        {
            ChangeState(GameState.Win);
        }

        if (Input.GetKey(KeyCode.P))
        {
            ChangeState(GameState.Paused);
        }
    }

    public void GameWin()
    {

    }

    public void GameLose()
    {
        
    }

    public void Paused()
    {
        if (Input.GetKey(KeyCode.P))
        {
            ChangeState(GameState.GameStart);
        }
    }
}

[Serializable]
public enum GameState {
    PlayerSetup = 0,
    GameStart = 1,
    Paused = 2,
    Win = 3,
    Lose = 4,
}