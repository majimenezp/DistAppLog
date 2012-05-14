// -----------------------------------------------------------------------
// <copyright file="MappingLog.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DistALServer.DAL.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Entities;
    using FluentNHibernate.Mapping;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MappingLog:ClassMap<Log>
    {
        public MappingLog()
        {
            Table("LogData");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.App).Length(100);
            Map(x => x.AppIdentity).Not.Nullable();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.Exception);
            Map(x => x.Level).Not.Nullable();
            Map(x => x.Message).Not.Nullable();
            Map(x => x.Module).Not.Nullable();

        }
    }
}
