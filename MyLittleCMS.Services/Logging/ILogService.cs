using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Services.Logging
{
    public partial interface ILogService
    {
        void DeleteLog(Log log);
        void DeleteLogs(IList<Log> logs);
        void ClearLog();
        X.PagedList.IPagedList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", LogType? logType = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
        Log GetLogById(int logId);
        Log InsertLog(LogType logLevel, string shortMessage, string fullMessage = "", MembershipUser user = null);
        void Error(Exception ex);
        void Error(Exception ex,LogType logType,string shortMessage);
        void Error(Exception ex, LogType logType, string shortMessage,MembershipUser user);
    }
}

