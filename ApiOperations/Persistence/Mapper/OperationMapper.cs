using Domain;
using DapperExtensions.Mapper;

namespace Persistence.Mapper
{
    public class OperationMapper : ClassMapper<Operation>
    {
        public OperationMapper()
        {
            Table("Operation");
            Map(op => op.Id).Key(KeyType.Identity);
            Map(op => op.Type).Column("Type");
            Map(op => op.FirstArgument).Column("FirstArgument");
            Map(op => op.SecondArgument).Column("SecondArgument");
            AutoMap();
        }
    }
}
