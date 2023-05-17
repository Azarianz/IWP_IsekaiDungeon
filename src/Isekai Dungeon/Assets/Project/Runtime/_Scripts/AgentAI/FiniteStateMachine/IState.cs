
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.STATES
{
    public interface IState
    {
        void OnEnterState();
        void OnExitState();
        void DoState();
    }

}

