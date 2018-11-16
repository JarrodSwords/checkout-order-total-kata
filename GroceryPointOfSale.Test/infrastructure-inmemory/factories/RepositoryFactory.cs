namespace GroceryPointOfSale.Test
{
    public abstract class RepositoryFactory<T> where T : new()
    {
        public virtual T CreateEmptyRepository()
        {
            return new T();
        }

        public virtual T CreateSeededRepository()
        {
            var repository = CreateEmptyRepository();
            Seed(ref repository);
            return repository;
        }

        protected abstract void Seed(ref T repository);
    }
}