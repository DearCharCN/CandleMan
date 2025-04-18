using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.BehaviourTrees
{

    [System.Obsolete("Use Cooldown or Limiter")]
    [Name("Filter")]
    [Category("Decorators")]
    [Description("Filters the access of its child either a specific number of times, or every specific amount of time.")]
    [ParadoxNotion.Design.Icon("Filter")]
    public class Filter : BTDecorator
    {

        public enum FilterMode
        {
            LimitNumberOfTimes = 0,
            CoolDown = 1
        }

        public enum Policy
        {
            SuccessOrFailure,
            SuccessOnly,
            FailureOnly
        }

        [Tooltip("The mode to use.")]
        public FilterMode filterMode = FilterMode.CoolDown;
        [ShowIf("filterMode", 0)]
        [Name("Max Times"), Tooltip("The max ammount of times to allow the child to execute until the tree is completely restarted.")]
        public BBParameter<int> maxCount = 1;
        [ShowIf("filterMode", 0)]
        [Name("Increase Count When"), Tooltip("Only increase count if the selected status is returned from the child.")]
        public Policy policy = Policy.SuccessOrFailure;
        [ShowIf("filterMode", 1), Tooltip("The time to disallow execution for.")]
        public BBParameter<float> coolDownTime = 5f;
        [Name("Optional When Filtered"), Tooltip("If enabled, the Filter Decorator will return an Optional status when it is filtered. Otherwise it will return Failure.")]
        public bool inactiveWhenLimited = true;

        private int executedCount;
        private float currentTime;

        public override void OnGraphStoped() {
            executedCount = 0;
            currentTime = 0;
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( decoratedConnection == null ) {
                return Status.Optional;
            }

            switch ( filterMode ) {
                case FilterMode.CoolDown:

                    if ( currentTime > 0 ) {
                        return inactiveWhenLimited ? Status.Optional : Status.Failure;
                    }

                    status = decoratedConnection.Execute(agent, blackboard);
                    if ( status == Status.Success || status == Status.Failure ) {
                        StartCoroutine(Cooldown());
                    }
                    break;

                case FilterMode.LimitNumberOfTimes:

                    if ( executedCount >= maxCount.value ) {
                        return inactiveWhenLimited ? Status.Optional : Status.Failure;
                    }

                    status = decoratedConnection.Execute(agent, blackboard);
                    if
                    (
                        ( status == Status.Success && policy == Policy.SuccessOnly ) ||
                        ( status == Status.Failure && policy == Policy.FailureOnly ) ||
                        ( ( status == Status.Success || status == Status.Failure ) && policy == Policy.SuccessOrFailure )
                    ) {
                        executedCount += 1;
                    }
                    break;
            }

            return status;
        }


        IEnumerator Cooldown() {
            currentTime = coolDownTime.value;
            while ( currentTime > 0 ) {
                yield return null;
                currentTime -= Time.deltaTime;
            }
        }


        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        protected override void OnNodeGUI() {

            if ( filterMode == FilterMode.CoolDown ) {
                GUILayout.Space(25);
                var pRect = new Rect(5, GUILayoutUtility.GetLastRect().y, rect.width - 10, 20);
                UnityEditor.EditorGUI.ProgressBar(pRect, currentTime / coolDownTime.value, currentTime > 0 ? "Cooling..." : "Cooled");
            } else
            if ( filterMode == FilterMode.LimitNumberOfTimes ) {
                GUILayout.Label(executedCount + " / " + maxCount.value + " Accessed Times");
            }
        }

#endif
    }
}