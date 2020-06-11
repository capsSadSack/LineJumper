using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordsSceneController : MonoBehaviour
{
    public RecordsPanelController recordsPanelController;
    public HighScores worldRecordsAccess;

    private PlayerPrefsRecordsAccess recordsAccess = new PlayerPrefsRecordsAccess();

    private RecordsScale recordsScale = RecordsScale.Local;
    private Difficulty difficulty = Difficulty.Easy;

    public void ProcessLocalButtonClick()
    {
        recordsScale = RecordsScale.Local;
        UpdateRecords();
    }

    public void ProcessWorldButtonClick()
    {
        recordsScale = RecordsScale.World;
        UpdateRecords();
    }

    public void ProcessEasyButtonClick()
    {
        difficulty = Difficulty.Easy;
        UpdateRecords();
    }

    public void ProcessMediumButtonClick()
    {
        difficulty = Difficulty.Medium;
        UpdateRecords();
    }

    public void ProcessHardButtonClick()
    {
        difficulty = Difficulty.Hard;
        UpdateRecords();
    }

    public void UpdateRecords()
    {
        if (recordsScale == RecordsScale.Local)
        {
            var records = recordsAccess.GetRecords(difficulty);
            recordsPanelController.ShowRecords(records);
        }
        else
        {
            var records = worldRecordsAccess.DownloadHighScores(difficulty);
            recordsPanelController.ShowRecords(records);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private enum RecordsScale
    {
        Local,
        World
    }
}
