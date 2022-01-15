namespace Game.Characters.Interfaces
{
    public interface IDrainMana
    {
        event System.Action onDrainManaEvent;

        void DrainMana();
    }

    public interface IResetMana
    {
        event System.Action onResetManaEvent;

        void ResetMana();
    }

    public interface IMana : IDrainMana, IResetMana
    {
        int maxMana { get; }
        int currentMana { get; }

        event System.Action<IMana, int> onUseManaEvent;
        event System.Action<IMana, int> onAddManaEvent;

        void UseMana(int mana);
        void AddMana(int mana);
    }
}