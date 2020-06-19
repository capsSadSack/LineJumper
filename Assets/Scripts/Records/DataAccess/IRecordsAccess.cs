using System.Linq;

public interface IRecordsAccess
{
    IOrderedEnumerable<RecordInfo> GetRecords(Difficulty difficulty);

    void InsertRecord(RecordInfo recordInfo);

    bool IsRecord(RecordInfo recordInfo);
}
