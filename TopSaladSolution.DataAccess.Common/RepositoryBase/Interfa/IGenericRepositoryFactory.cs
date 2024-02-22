namespace TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
