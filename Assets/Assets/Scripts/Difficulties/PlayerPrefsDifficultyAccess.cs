using UnityEngine;

public class PlayerPrefsDifficultyAccess
{
    private readonly string key = "difficulty";
    private readonly Difficulty defaultDifficulty = Difficulty.Easy;


    public Difficulty GetDifficulty()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string difficulty = PlayerPrefs.GetString(key);
            return EnumsProcessor.GetValueFromDescription<Difficulty>(difficulty);
        }
        else
        {
            return defaultDifficulty;
        }
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        string difficultyString = EnumsProcessor.GetDescription(difficulty);
        PlayerPrefs.SetString(key, difficultyString);
        PlayerPrefs.Save();
    }
}
