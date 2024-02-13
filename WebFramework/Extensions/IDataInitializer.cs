using Common;
using Core.Application.Extensions;

namespace Services.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
