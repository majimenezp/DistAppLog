// -----------------------------------------------------------------------
// <copyright file="MappingApplication.cs" company="Microsoft">
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
    public class MappingApplication:ClassMap<Application>
    {

        public MappingApplication()
        {
            Table("Applications");
            Not.LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.AppName).Length(100);
        }
    }
}
