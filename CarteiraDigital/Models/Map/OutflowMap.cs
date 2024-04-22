using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CarteiraDigital.Models.Map
{
    public class OutflowMap : ClassMapping<Outflow>
    {
        public OutflowMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int64);
                x.Column("Id");
            });
            Property(x => x.OutflowDate, x =>
            {
                x.Type(NHibernateUtil.DateTime);
                x.NotNullable(true);
            });
            Property(x => x.OutflowDescription, x =>
            {
                x.Length(700);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(x => x.OutflowAmount, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(true);
            });
            ManyToOne(x => x.Person, x =>
            {
                x.Column("PES_Id");
                x.NotNullable(true);
            });
            Table("Outflow");
        }
    }
}
