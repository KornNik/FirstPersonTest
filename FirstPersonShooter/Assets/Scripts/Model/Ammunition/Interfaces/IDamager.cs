namespace ExampleTemplate
{
    public interface IDamager
    {
        void InflictDamage(IDamageable victim);
        void AddDamage(float bonusDamage);
    }
}
