using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordsSceneController : MonoBehaviour
{
    // TODO: [CG, 2020.06.08] Не реализованы world-рекорды
    public RecordsPanelController recordsPanelController;

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
        recordsPanelController.ShowRecords(difficulty);
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
