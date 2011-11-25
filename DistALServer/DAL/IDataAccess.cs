using System;
using DistALServer.DAL.Entities;
namespace DistALServer.DAL
{
    public interface IDataAccess
    {
        void InsertDebugMessage(DistALMessages.DebugMessage message);
        void InsertErrorMessage(DistALMessages.ErrorMessage message);
        void InsertFatalMessage(DistALMessages.FatalErrorMessage message);
        void InsertHitMessage(DistALMessages.HitMessage message);
        void InsertInfoMessage(DistALMessages.InfoMessage message);
        void InsertWarningMessage(DistALMessages.WarningMessage message);
        Log[] GetLog(int PageNumber, int ItemsPerPage, out int TotalPages);
        Log[] GetLog();
        
        long CheckAppId(string appname);
    }
}
