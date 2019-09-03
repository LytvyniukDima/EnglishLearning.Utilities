namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
