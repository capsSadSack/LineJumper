using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    private const string webURL = "http://dreamlo.com/lb/";

    private readonly Dictionary<Difficulty, Codes> siteCodes = new Dictionary<Difficulty, Codes>()
    {
        { Difficulty.Easy,   new Codes("5ee2210c377dce0a149f2e75", "LSJi_XdPkE2pPJV6Nvfruw3CYGcNbAU0mY1Z0BPEVOFg") },
        { Difficulty.Medium, new Codes("", "") },
        { Difficulty.Hard,   new Codes("", "") }
    };

    private RecordInfo[] highScores;


    public void AddNewHighScore(RecordInfo record)
    {
        StartCoroutine(UploadNewHighScore(record));
    }

    private IEnumerator UploadNewHighScore(RecordInfo record)
    {
        WWW www = new WWW(
            webURL + 
            siteCodes[record.Difficulty].PrivateCode + 
            "/add/" +
            WWW.EscapeURL(record.PlayerName) + "/" + record.Score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public IEnumerable<RecordInfo> DownloadHighScores(Difficulty difficulty)
    {
        StartCoroutine(DownloadHighScoresFromDb(difficulty));
        return highScores;
    }

    private IEnumerator DownloadHighScoresFromDb(Difficulty difficulty)
    {
        WWW www = new WWW(
            webURL + 
            siteCodes[difficulty].PublicCode +
            "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text, difficulty);
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }

    private void FormatHighScores(string textStream, Difficulty difficulty)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highScores = new RecordInfo[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split('|');
            int score = int.Parse(entryInfo[1]);

            RecordInfo highScore = new RecordInfo()
            {
                Difficulty = difficulty,
                PlayerName = entryInfo[0],
                Score = score
            };

            highScores[i] = highScore;
        }
    }

    private struct Codes
    {
        public string PublicCode { get; set; }
        public string PrivateCode { get; set; }

        public Codes(string publicCode, string privateCode)
        {
            PublicCode = publicCode;
            PrivateCode = privateCode;
        }
    }
}


