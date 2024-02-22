namespace TopSaladSolution.DataAccess.Common.UnitOfWorkBase.Interfa
{
    public interface IUnitOfWorkPool
    {
        IEnumerable<string> RegisteredUoWKeys { get; }

        IUnitOfWork Get(string key);

        IEnumerable<IUnitOfWork> GetAll();
    }
}
