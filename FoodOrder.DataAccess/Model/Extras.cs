using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class Extras
    {
        public virtual int Id { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual float Price { get; set; }

        public virtual Store Store { get; set; }
    }

    public class ExtrasMap : ClassMap<Extras>
    {
        public ExtrasMap()
        {
            Id(x => x.Id).Column("`id`");

            Map(x => x.DisplayName).Column("`displayName`").Not.Nullable();
            Map(x => x.Price).Column("`price`").Not.Nullable();

            References(x => x.Store).Column("`storeid`");

            Table("`tbExtras`");
        }
    }
}