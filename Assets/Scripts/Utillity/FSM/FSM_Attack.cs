public partial class FSM
{
    public class StateAttack<T> : CFSMState<T>
    {
        public override int State => (int)FSM_STATE.Attack;

        private void Enter(T in_owner)
        {
            (in_owner as PawnBase).Enter_Attack();
        }

        private void Update(T in_owner)
        {            
            (in_owner as PawnBase).Update_Attack();
        }

        private void Exit(T in_owner)
        {
            (in_owner as PawnBase).Exit_Attack();
        }

        public StateAttack()
        {
            m_action_enter = Enter;
            m_action_execute = Update;
            m_action_exit = Exit;
        }
    }
}