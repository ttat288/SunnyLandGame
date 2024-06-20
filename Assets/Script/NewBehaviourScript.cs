using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject scoreEntryPrefab;
    public Transform scoresContainer;
    public List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>();

    private void Start()
    {
        // Test data
        highscoreEntries = new List<HighscoreEntry>();
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player1", playerScore = 100 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player2", playerScore = 60 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player3", playerScore = 80 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player4", playerScore = 50 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player5", playerScore = 30 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player6", playerScore = 10 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player7", playerScore = 30 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player8", playerScore = 40 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player9", playerScore = 50 });
        highscoreEntries.Add(new HighscoreEntry { playerName = "Player10", playerScore = 60 });
        highscoreEntries.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));
        // Call update UI function
        UpdateHighscoreUI();
    }

    private void UpdateHighscoreUI()
    {
        // Clear existing entries
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new entries
        foreach (HighscoreEntry entry in highscoreEntries)
        {
            // Instantiate one Prefab for each entry
            GameObject newEntry = Instantiate(scoreEntryPrefab, scoresContainer);

            // Set text of the new entry
            Text[] texts = newEntry.GetComponentsInChildren<Text>();
            texts[0].text = entry.playerName;
            texts[1].text = entry.playerScore.ToString();
        }
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public string playerName;
    public int playerScore;
}
