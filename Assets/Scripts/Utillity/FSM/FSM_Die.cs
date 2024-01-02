public partial class FSM
{
    public class StateDie<T> : CFSMState<T>
    {
        public override int State => (int)FSM_STATE.Die;

        private void Enter(T in_owner)
        {
            (in_owner as PawnBase).Enter_Die();
        }

        private void Update(T in_owner)
        {
            (in_owner as PawnBase).Update_Die();
        }

        private void Exit(T in_owner) 
        {
            (in_owner as PawnBase).Exit_Die();
        }

        public StateDie()
        {
            m_action_enter = Enter;
            m_action_execute = Update;
            m_action_exit = Exit;
        }
    }
}