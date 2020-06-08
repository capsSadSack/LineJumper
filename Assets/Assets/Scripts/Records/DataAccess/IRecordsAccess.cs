using System.Collections.Generic;
using System.Linq;

namespace Assets.Assets.Scripts.Records
{
    public interface IRecordsAccess
    {
        IOrderedEnumerable<RecordInfo> GetRecords(Difficulty difficulty);

        void InsertRecord(RecordInfo recordInfo);

        bool IsRecord(RecordInfo recordInfo);
    }
}
