using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecordsPanelController : MonoBehaviour
{
    private List<GameObject> recordItems = new List<GameObject>();


    public void ShowRecords(IEnumerable<RecordInfo> records)
    {
        ClearRecordItems();
        CreateRecords(records);
    }

    private void ClearRecordItems()
    {
        foreach (var recordItem in recordItems)
        {
            GameObject.Destroy(recordItem);
        }

        recordItems.Clear();
    }

    private void CreateRecords(IEnumerable<RecordInfo> records)
    {
        Vector2 offset = new Vector2(0, 850);
        int dy = -150;

        if (records != null && records.Count() > 0)
        {
            for (int place = 1; place <= records.Count(); place++)
            {
                GameObject recordItem;

                if (place <= records.Count())
                {
                    recordItem = CreateRecordItem(place, records.ElementAt(place - 1));
                }
                else
                {
                    RecordInfo empty = new RecordInfo()
                    {
                        PlayerName = "...",
                        Score = 0
                    };

                    recordItem = CreateRecordItem(place, empty);
                }

                recordItem.transform.localScale = new Vector3(1, 1, 1);
                recordItem.transform.localPosition = offset + (place - 1) * new Vector2(0, dy);
                recordItems.Add(recordItem);
            }
        }
    }

    private GameObject CreateRecordItem(int place, RecordInfo recordInfo)
    {
        var source = Resources.Load("Prefabs/RecordItem");
        GameObject objSource = (GameObject)Instantiate(source);
        objSource.transform.SetParent(gameObject.transform);

        Text placeText = objSource.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        Text nameText = objSource.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        Text scoreText = objSource.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>();

        placeText.text = $"{ place }";
        nameText.text = recordInfo.PlayerName;
        scoreText.text = $"{ recordInfo.Score }";

        return objSource;
    }
}
