namespace JZ.CORE
{
    public abstract class State
    {
        public virtual void StartState() { }
        public virtual void EndState(State nextState) { }
        public virtual void StateUpdate() { }
        protected virtual void StateChangeCheck() { }

        //Returns true if state is allowed to change
        protected abstract bool StateChangePreCheck();
    }
}