
namespace ExampleTemplate
{
    interface ISaveSystem<T>
    {
        void Save(T serializableObject);
        T Load();
    }
}
