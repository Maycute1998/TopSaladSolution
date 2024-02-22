namespace TopSaladSolution.DataAccess.Common.UnitOfWorkBase.Uow
{
    public class UnitOfWorkPoolOptions
    {
        public Dictionary<string, Type> RegisteredUoWs { get; set; } = new Dictionary<string, Type>();
    }
}
