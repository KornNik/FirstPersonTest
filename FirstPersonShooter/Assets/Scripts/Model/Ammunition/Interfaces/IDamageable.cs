namespace ExampleTemplate
{
    public interface IDamageable
    {
        void ReceiveDamage(float damage);
        void ReceiveDamageOverTime(float damage, float duration);
    }
}
