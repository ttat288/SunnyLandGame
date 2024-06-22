using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Script.singleton;
using Assets.Script;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject scoreEntryPrefab;
    public Transform scoresContainer;
    private static List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>();
    private MongoDBConnection mongoDBConnection;
    private User user;

    private async void Start()
    {
        user = User.Instance;
        mongoDBConnection = new MongoDBConnection();

        var topRanks = await mongoDBConnection.GetTopRank(user.Id);
        highscoreEntries.AddRange(topRanks);
        highscoreEntries.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));

        // Call update UI function
        await UpdateHighscoreUI();
    }

    private async Task UpdateHighscoreUI()
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
            texts[0].text = await mongoDBConnection.getNameById(entry.playerId);
            texts[1].text = entry.playerScore.ToString();
        }
    }
}
