using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.BehaviourTrees
{

    [Name("Repeat")]
    [Category("Decorators")]
    [Description("Repeats the child either x times, or until it returns the specified status, or forever.")]
    [ParadoxNotion.Design.Icon("Repeat")]
    public class Repeater : BTDecorator
    {

        public enum RepeaterMode
        {
            RepeatTimes = 0,
            RepeatUntil = 1,
            RepeatForever = 2
        }

        public RepeaterMode repeaterMode = RepeaterMode.RepeatTimes;
        [ShowIf("repeaterMode", 0)]
        public BBParameter<int> repeatTimes = 1;
        [ShowIf("repeaterMode", 1)]
        public BooleanStatus repeatUntilStatus = BooleanStatus.Success;

        private int currentIteration = 1;

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( decoratedConnection == null ) {
                return Status.Optional;
            }

            if ( decoratedConnection.status != Status.Running ) {
                decoratedConnection.Reset();
            }

            status = decoratedConnection.Execute(agent, blackboard);

            switch ( status ) {
                case Status.Resting:
                    return Status.Running;
                case Status.Running:
                    return Status.Running;
            }

            switch ( repeaterMode ) {
                case RepeaterMode.RepeatTimes:

                    if ( currentIteration >= repeatTimes.value ) {
                        return status;
                    }

                    currentIteration++;
                    break;

                case RepeaterMode.RepeatUntil:

                    if ( (int)status == (int)repeatUntilStatus ) {
                        return status;
                    }
                    break;
            }

            return Status.Running;
        }

        protected override void OnReset() {
            currentIteration = 1;
        }


        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        protected override void OnNodeGUI() {

            if ( repeaterMode == RepeaterMode.RepeatTimes ) {
                GUILayout.Label(repeatTimes + " Times");
                if ( Application.isPlaying )
                    GUILayout.Label("Iteration: " + currentIteration.ToString());

            } else if ( repeaterMode == RepeaterMode.RepeatUntil ) {

                GUILayout.Label("Until " + repeatUntilStatus);

            } else {

                GUILayout.Label("Repeat Forever");
            }
        }

#endif
    }
}