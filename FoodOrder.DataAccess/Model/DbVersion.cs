using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class DbVersion
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual DateTime TimeStamp { get; set; }
    }

    public class DbVersionMap : ClassMap<DbVersion>
    {
        public DbVersionMap()
        {
            Table("`tbVersion`");

            Id(x => x.Id).Column("`version`");

            Map(x => x.TimeStamp).Column("`timestamp`");
        }
    }
}

