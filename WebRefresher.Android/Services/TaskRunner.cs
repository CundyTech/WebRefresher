using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App.Job;
using Xamarin.Forms;
using OperationCanceledException = Android.OS.OperationCanceledException;

namespace WebRefresher.Droid.Services
{
    /// <summary>
    /// https://forums.xamarin.com/discussion/134197/any-xamarin-android-c-sample-for-jobintentservice
    /// </summary>
    [Service(Permission = "android.permission.BIND_JOB_SERVICE", Exported = true)]
    public class AppService : Android.Support.V4.App.JobIntentService
    {
        private static string Tag = typeof(AppService) + ": ";
        private CancellationTokenSource _cts;
        private static int MY_JOB_ID = 1000;

        public static void EnqueueWork(Context context, Intent work)
        {
            Java.Lang.Class cls = Java.Lang.Class.FromType(typeof(AppService));
            try
            {
                EnqueueWork(context, cls, MY_JOB_ID, work);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnHandleWork(Intent intent)
        {
            _cts = new CancellationTokenSource(30 * 1000);

            Task.Run(async () =>
            {
                try
                {
                    // do your work here
                }
                catch (Exception e)
                {
                    throw e;
                }
            });
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }

            base.OnDestroy();
        }
    }
}
