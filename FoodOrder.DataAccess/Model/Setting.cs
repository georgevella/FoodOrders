using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class Setting
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual string DataType { get; set; }
    }

    public class SettingMap : ClassMap<Setting>
    {
        public SettingMap()
        {
            Table("`tbSettings`");

            Id(x => x.Id).Column("`id`").GeneratedBy.HiLo("100");

            Map(x => x.Name).Column("`name`").Not.Nullable();
            Map(x => x.Value).Column("`value`").Not.Nullable().Length(1024);
            Map(x => x.DataType).Column("`dt`").Not.Nullable().Length(256);
        }
    }
}