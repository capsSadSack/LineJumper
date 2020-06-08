using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Assets.Scripts.Records.DataAccess
{
    public class PlayerPrefsRecordsAccess : IRecordsAccess
    {
        public IOrderedEnumerable<RecordInfo> GetRecords(Difficulty difficulty)
        {
            List<RecordInfo> output = new List<RecordInfo>();

            for (int place = 1; place <= 10; place++)
            {
                string key = GetRecordKey(difficulty, place);

                if (PlayerPrefs.HasKey(key))
                {
                    string recordString = PlayerPrefs.GetString(key);
                    RecordInfo recordInfo = recordString.ToRecordInfo();
                    output.Add(recordInfo);
                }
                else
                {
                    break;
                }
            }

            return output.OrderBy(x => x.Score);
        }

        public void InsertRecord(RecordInfo recordInfo)
        {
            var currentRecords = GetRecords(recordInfo.Difficulty).ToList();
            currentRecords.Add(recordInfo);
            var newRecords = currentRecords.OrderBy(x => x.Score);

            for (int i = 0; i < 10 || i < newRecords.Count(); i++)
            {
                int place = i + 1;
                string key = GetRecordKey(recordInfo.Difficulty, place);
                PlayerPrefs.SetString(key, recordInfo.ToRecordString());
            }

            PlayerPrefs.Save();
        }

        public bool IsRecord(RecordInfo recordInfo)
        {
            var currentRecords = GetRecords(recordInfo.Difficulty);
            return currentRecords.Last().Score < recordInfo.Score;
        }

        // place from 1 to 10!
        private string GetRecordKey(Difficulty difficulty, int place)
        {
            string diffStr = EnumsProcessor.GetDescription(difficulty);
            string placeStr = place.ToString("D2");
            return $"{ diffStr }_{ placeStr }";
        }
    }
}
