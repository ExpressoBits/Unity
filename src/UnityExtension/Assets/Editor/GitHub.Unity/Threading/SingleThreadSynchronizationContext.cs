using GitHub.Unity;
using System;
using System.Threading;
using UnityEditor;

namespace GitHub.Unity
{
    class MainThreadSynchronizationContext : SynchronizationContext, IMainThreadSynchronizationContext
    {
        private static readonly ILogging logger = Logging.GetLogger<MainThreadSynchronizationContext>();

        public void Schedule(Action action)
        {
            //logger.Debug("Scheduling action");

            Guard.ArgumentNotNull(action, "action");
            Post(_ => action.SafeInvoke(), null);
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            if (d == null)
                return;

            EditorApplication.delayCall += () => d(state);
        }
    }
}