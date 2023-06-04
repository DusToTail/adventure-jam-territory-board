namespace Gameplay
{
    public class PlayerState
    {
        public string Name { get; private set; }
        public delegate void OnEnterPlayerState(PlayerState state);
        public delegate void OnExitPlayerState(PlayerState state);
        public event OnEnterPlayerState onEnterPlayerState;
        public event OnExitPlayerState onExitPlayerState;

        public PlayerState(string name)
        {
            Name = name;
        }

        public void OnEnter()
        {
            onEnterPlayerState?.Invoke(this);
        }
        public void OnExit()
        {
            onExitPlayerState?.Invoke(this);
        }
    }
}
