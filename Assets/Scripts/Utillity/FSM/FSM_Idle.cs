public partial class FSM
{
    public class StateIdle<T> : CFSMState<T>
    {
        public override int State => (int)FSM_STATE.Idle;

        private void Enter(T in_owner)
        {
            (in_owner as PawnBase).Enter_Idle();
        }

        private void Update(T in_owner)
        {
            (in_owner as PawnBase).Update_Idle();
        }

        private void Exit(T in_owner) 
        {
            (in_owner as PawnBase).Exit_Idle();
        }

        public StateIdle()
        {
            m_action_enter = Enter;
            m_action_execute = Update;
            m_action_exit = Exit;
        }
    }
}