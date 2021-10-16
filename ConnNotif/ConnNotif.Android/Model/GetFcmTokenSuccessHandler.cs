using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnNotif.Droid.Model
{
    /// <summary>
    /// Wraps the native Android interface <see cref="IOnCompleteListener"/>,
    /// which enables interaction with Promise-style async callbacks.
    /// </summary>
    public class GetFcmTokenSuccessHandler : Java.Lang.Object, IOnCompleteListener
    {
        private readonly Action<string> _handleFcmTokenCallback;

        public GetFcmTokenSuccessHandler(Action<string> handleFcmTokenCallback)
        {
            _handleFcmTokenCallback = handleFcmTokenCallback;
        }

        /// <summary>
        /// Method invoked when the associated <see cref="Task"/> completes.
        /// </summary>
        /// <param name="result">The task that completed.</param>
        public void OnComplete(Task result)
        {
            if (result.IsComplete && result.IsSuccessful)
            {
                var token = result.Result.ToString();

                _handleFcmTokenCallback?.Invoke(token);
            }
            else if (result.IsComplete && result.Exception != null)
            {
                // Add error handling here
            }
        }

        #region Interface requiresments; irrelevant to implementation
        public JniManagedPeerStates JniManagedPeerState => JniManagedPeerStates.None;

        public void SetJniIdentityHashCode(int value) { }
        public void SetJniManagedPeerState(JniManagedPeerStates value) { }
        public void SetPeerReference(JniObjectReference reference) { }
        #endregion
    }
}