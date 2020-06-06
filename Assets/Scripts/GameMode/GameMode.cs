using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int teamAmount = 2;

    public List<Team> teams;
    public List<Transform> spawnPoints;

    private void Start()
    {
        SetupGame();
    }

    /// <summary>
    /// Set up the teams for the game
    /// </summary>    
    public void SetupGame()
    {
        for (int teamID = 0; teamID < teamAmount; teamID++)
        {
            teams.Add(new Team());
        }        
    }

    /// <summary>
    /// Add score to specified team by ID
    /// </summary>
    /// <param name="teamID">the id of the team to add points to</param>
    /// <param name="count">the amount of points to add to the team score</param>
    /// <example>
    /// <code>
    ///     AddScore(1,1);
    /// </code>
    /// </example>    
    public void AddScore(int teamID, int count)
    {
        teams[teamID].score += count;
        Debug.Log("Captured Flag");
    }
}

[System.Serializable]
public class Team
{
    public int score;
    public string teamType;
}
