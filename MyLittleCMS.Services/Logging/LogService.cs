using MyLittleCMS.Services.Logging;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MyLittleCMS.Core.Domain.Entities;
using X.PagedList;
using MyLittleCMS.Core.Repository;
using MyLittleCMS.Core.Utilities;

namespace MyLittleCMS.Services
{
    public partial class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Log> _logRepository;

        public LogService(IUnitOfWork unitOfwork, IRepository<Log> logRepository)
        {
            _unitOfWork = unitOfwork;
            _logRepository = logRepository;
        }

        public void ClearLog()
        {
            var logs = _logRepository.Get().ToList();
            foreach (var logItem in logs)
                _logRepository.Delete(logItem);
        }

        public void DeleteLog(Log log)
        {
            _logRepository.Delete(log);
        }

        public void DeleteLogs(IList<Log> logs)
        {
            foreach (var logItem in logs)
                _logRepository.Delete(logItem);
        }

        private string GetShortMessageFromException(Exception ex)
        {
            if (ex.Message.Length >= 300)
            {
                return ex.Message.Substring(0, 300);
            }
            else
            {
                return ex.Message.Substring(0, ex.Message.Length);
            }
        }
        private string GetlongMessageFromException(Exception ex)
        {
            const int maxExceptionDepth = 4;
            if (ex == null)
            {
                return "";
            }
            var message = new StringBuilder(ex.Message);
            var inner = ex.InnerException;
            var depthCounter = 0;
            while (inner != null && depthCounter++ < maxExceptionDepth)
            {
                message.Append(" INNER EXCEPTION: ");
                message.Append(inner.Message);
                inner = inner.InnerException;
            }

            return message.ToString();
        }

        public void Error(Exception ex)
        {
            string shortMessage = GetShortMessageFromException(ex);
            string longMessage= GetlongMessageFromException(ex);
            InsertLog(LogType.SystemError, shortMessage, longMessage, null);
        }

        public void Error(Exception ex, LogType logType, string shortMessage)
        {
            string longMessage = GetlongMessageFromException(ex);
            InsertLog(logType, shortMessage, longMessage, null);
        }

        public void Error(Exception ex, LogType logType, string shortMessage, MembershipUser user)
        {
            string longMessage = GetlongMessageFromException(ex);
            InsertLog(logType, shortMessage, longMessage, user);
        }

        public IPagedList<Log> GetAllLogs(DateTime? fromUtc = default(DateTime?), DateTime? toUtc = default(DateTime?), string message = "",
            LogType? logType = default(LogType?), int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _logRepository.Table();
            if (fromUtc.HasValue)
                query = query.Where(l => fromUtc.Value <= l.CreatedOnUtc);
            if (toUtc.HasValue)
                query = query.Where(l => toUtc.Value >= l.CreatedOnUtc);
            if (logType.HasValue)
            {
                var logTypeId = (int)logType.Value;
                query = query.Where(l => logTypeId == l.LogTypeId);
            }
            if (!String.IsNullOrEmpty(message))
                query = query.Where(l => l.ShortMessage.Contains(message) || l.FullMessage.Contains(message));
            query = query.OrderByDescending(l => l.CreatedOnUtc);

            var log = new PagedList<Log>(query, pageIndex, pageSize);
            return log;
        }

        public Log GetLogById(int id)
        {
            return _logRepository.Get(id);
        }

        public Log InsertLog(LogType logType, string shortMessage, string fullMessage = "", MembershipUser user = null)
        {
            Log log = new Log
            {
                LogTypeId = (int)logType,
                FullMessage = fullMessage,
                ShortMessage = shortMessage,
            };
            if (user != null)
            {
                log.UserId = user.Id;
            }
            log.IpAddress = WebHelper.GetIpAddress();
            log.PageUrl = WebHelper.GetUrl();
            log.ReferrerUrl = WebHelper.GetUrlReferrer();
            log.CreatedOnUtc = DateTime.UtcNow;
            _logRepository.Add(log);
            return log;
        }

    }
}