using TerritoryBoard.Mechanics;
using TerritoryBoard.TurnBasedSystem;
using System.Collections.Generic;
using System;

namespace Gameplay
{
    public abstract class Player : TurnBasedActor, ITeamMember
    {
        public readonly Dictionary<string, PlayerState> States = new Dictionary<string, PlayerState>()
        {
            { "WaitingOthers", new PlayerState("WaitingOthers")},
            { "SelectingBoard", new PlayerState("SelectingBoard")},
            { "SelectingAction", new PlayerState("SelectingAction")},
            { "CreatingStructure", new PlayerState("CreatingStructure")},
            { "CreatingUnit", new PlayerState("CreatingUnit")},
            { "MovingUnit", new PlayerState("MovingUnit")},
            { "DeletingStructure", new PlayerState("DeletingStructure")},
            { "DeletingUnit", new PlayerState("DeletingUnit")}
        };

        public ITeam Team
        {
            get
            {
                return _team;
            }
            set
            {
                if(_team != value)
                {
                    _team = value;
                    onTeamChanged?.Invoke(this, value);
                }
            }
        }
        public delegate void OnTeamChanged(ITeamMember member, ITeam team);
        public event OnTeamChanged onTeamChanged;
        private ITeam _team;

        public PlayerState PlayerState
        {
            get
            {
                return _playerState;
            }
            set
            {
                bool changed = _playerState != value;
                if(changed)
                {
                    if (_playerState != null)
                    {
                        _playerState.OnExit();
                        if(value != null)
                        {
                            value.OnEnter();
                        }
                        _playerState = value;
                    }
                    else
                    {
                        value.OnEnter();
                        _playerState = value;
                    }
                }
                
            }
        }
        private PlayerState _playerState;

        protected Player(string id, TurnController turnController) : base(id, turnController)
        {
            _playerState = States["WaitingForOthers"];
        }

        public abstract void Initialize();

        public PlayerState GetPlayerState(string stateName)
        {
            if(States.TryGetValue(stateName, out PlayerState playerState))
            {
                return playerState;
            }
            throw new ArgumentException($"Player ({id}): failed to get PlayerState({stateName})!");
        }
    }
}
