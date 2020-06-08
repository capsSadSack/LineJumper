using System;

public static class RecordInfoConverter
{
    public static string ToRecordString(this RecordInfo recordInfo)
    {
        string difficulty = EnumsProcessor.GetDescription(recordInfo.Difficulty);
        string date = recordInfo.Date.ToString("yyyy.MM.dd");

        return $"{ date };{ difficulty };{ recordInfo.PlayerName };{ recordInfo.Score }";
    }

    public static RecordInfo ToRecordInfo(this string recordInfoString)
    {
        var parts = recordInfoString.Split(';');

        return new RecordInfo()
        {
            Date = ToDateTime(parts[0]),
            Difficulty = EnumsProcessor.GetValueFromDescription<Difficulty>(parts[1]),
            PlayerName = parts[2],
            Score = int.Parse(parts[3])
        };
    }

    private static DateTime ToDateTime(string str)
    {
        var parts = str.Split('.');

        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        return new DateTime(year, month, day);
    }
}
