namespace PrintCenter.Data
{
    public interface ITransaction
    {
        void Begin();

        void Commit();

        void Rollback();
    }
}