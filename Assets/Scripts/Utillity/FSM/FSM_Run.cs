public partial class FSM
{
    public class StateRun<T> : CFSMState<T>
    {
        public override int State => (int)FSM_STATE.Run;

        private void Enter(T in_owner)
        {
            (in_owner as PawnBase).Enter_Run();
        }

        private void Update(T in_owner)
        {
            (in_owner as PawnBase).Update_Run();
        }

        private void Exit(T in_owner)
        {
            (in_owner as PawnBase).Exit_Run();
        }

        public StateRun()
        {
            m_action_enter = Enter;
            m_action_execute = Update;
            m_action_exit = Exit;
        }
    }
}