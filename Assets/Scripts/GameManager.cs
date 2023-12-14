using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region VARIABLES

    public Text livesText;
    public Text scoreText;
    public Text gameOverText;
    public PacmanScript pacman;
    public GhostScript[] ghosts;
    public Transform pellets;
    private int gameScore;
    private int pacmanLives; 
    private int ghostWorthMultiplayer = 1;
    private bool lifeAdded = false;
    public int currentLevel;
    [SerializeField] private int fpsLimit;
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = fpsLimit;
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (pacmanLives <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetGameScore(0);
        SetPacmanLives(3);
        NextRound();
    }

    private void SetGameScore(int gameScore)
    {
        this.gameScore = gameScore;
        scoreText.text = gameScore.ToString().PadLeft(2, '0');
        if (gameScore >= 10000 && !lifeAdded)
        {
            SetPacmanLives(pacmanLives + 1);
            lifeAdded = true;
        }
    }

    private void SetPacmanLives(int pacmanLives)
    {
        this.pacmanLives = pacmanLives;
        livesText.text = 'x' + pacmanLives.ToString();
    }

    private void NextRound()
    {
        foreach (Transform pelletSmall in pellets)
        {
            pelletSmall.gameObject.SetActive(true);
            ResetStates();
        }
        currentLevel += 1;
        gameOverText.enabled = false;
    }

    private void ResetStates()
    {
        ResetWorth();
        foreach (var ghost in this.ghosts)
        {
            ghost.ResetGhostState();
        }
        
        
        pacman.ResetPacmanState();
    }

    private void GameOver()
    {
        foreach (var ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }
        
        pacman.gameObject.SetActive(false);

        gameOverText.enabled = true;
    }

    public void GhostDead(GhostScript ghost)
    {
        SetGameScore(gameScore + (ghost.worth * ghostWorthMultiplayer));
        ghostWorthMultiplayer++;
    }

    public void PacmanDead()
    {
        pacman.DeathAnimation();

        SetPacmanLives(pacmanLives - 1);

        if (pacmanLives > 0)
        {
            Invoke(nameof(ResetStates), 3f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletConsumed(PelletScript pellet)
    {
        pellet.gameObject.SetActive(false);
        SetGameScore(gameScore + pellet.worth);
        //Debug.Log(gameScore);

        if (AllPelletsEaten())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NextRound), 2f);
        }
    }

    public void PowerPelletConsumed(PowerPelletScript pellet)
    {
        foreach (var ghost in ghosts)
        {
            if (ghost.ghostHome.enabled == false)
            {
                ghost.ghostFrightened.EnableState(pellet.duration);
            }
        }
        
        PelletConsumed(pellet);
        //Debug.Log(gameScore);
        CancelInvoke(nameof(ResetWorth));
        Invoke(nameof(ResetWorth), pellet.duration);
    }

    private bool AllPelletsEaten()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void ResetWorth()
    {
        ghostWorthMultiplayer = 1;
    }
}
