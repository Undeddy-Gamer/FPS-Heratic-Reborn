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

    public void SetupGame()
    {
        for (int teamID = 0; teamID < teamAmount; teamID++)
        {
            teams.Add(new Team());
        }        
    }

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

}
