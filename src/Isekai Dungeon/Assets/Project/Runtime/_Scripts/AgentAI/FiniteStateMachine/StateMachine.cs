using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.STATES;

namespace FSM
{
    public class StateMachine
    {
        private IState _currentState;
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();  //transition coming from any state as they dont have FROM state

        private static List<Transition> EmptyTransitions = new List<Transition>(0);  

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                setState(transition.TO);

            _currentState.DoState();
        }

        public void setState(IState state)
        {
            if (_currentState == state)
                return;

            _currentState?.OnExitState();   //Check if there is a previous state, then run their exit code
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if(_currentTransitions == null)
            {
                _currentTransitions = EmptyTransitions; //still iterate if null, save memory less lookup
            }

            _currentState.OnEnterState();
        }

        public void AddTransition(IState f, IState t, Func<bool>condition)
        {
            if(_transitions.TryGetValue(f.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[f.GetType()] = transitions;
            }

            transitions.Add(new Transition(t, condition));
        }

        public void AddAnyTransition(IState state, Func<bool> condition)
        {
            _anyTransitions.Add(new Transition(state, condition));
        }

        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState TO { get; }

            public Transition(IState t, Func<bool> condition)
            {
                TO = t;
                Condition = condition;
            }
        }

        private Transition GetTransition()
        {
            foreach(var transition in _anyTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            foreach (var transition in _currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            return null;
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }


    }

}
