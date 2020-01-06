using System;

using UnityEngine;
using KSP.UI;
using KSP.UI.Screens;
using KSPPluginFramework;

namespace KerbalAlarmClock
{
    public partial class KerbalAlarmClock
    {
        /// <summary>
        /// Sets up the App Button - no longer called by the event as that only happens on StartMenu->SpaceCenter now
        /// </summary>
        void OnGUIAppLauncherReady()
        {
            Log.dbg("AppLauncherReady");
            if (ApplicationLauncher.Ready)
            {
                if (settings.ButtonStyleChosen == Settings.ButtonStyleEnum.Launcher )
                {
                    if (btnAppLauncher == null)
                    {
                        btnAppLauncher = InitAppLauncherButton();
                        //if (WindowVisibleByActiveScene)
                        //{
                        //    LogFormatted("Setting Button True");
                        //    btnAppLauncher.SetTrue();
                        //}
                    }
                }
            }
            else { Log.detail("App Launcher-Not Actually Ready"); }
        }

        void OnGameSceneLoadRequestedForAppLauncher(GameScenes SceneToLoad)
        {
            Log.dbg("GameSceneLoadRequest");
            DestroyAppLauncherButton();
        }
        internal ApplicationLauncherButton btnAppLauncher = null;

        internal ApplicationLauncherButton InitAppLauncherButton()
        {
            ApplicationLauncherButton retButton = null;

            ApplicationLauncherButton[] lstButtons = KerbalAlarmClock.FindObjectsOfType<ApplicationLauncherButton>();
            //LogFormatted("AppLauncher: Creating Button-BEFORE", lstButtons.Length);
            try
            {
                retButton = ApplicationLauncher.Instance.AddModApplication(
                    onAppLaunchToggleOn, onAppLaunchToggleOff,
                    onAppLaunchHoverOn, onAppLaunchHoverOff,
                    null, null,
                    ApplicationLauncher.AppScenes.ALWAYS,
                    (Texture)KACResources.toolbariconNorm);

                //AppLauncherButtonMutuallyExclusive(settings.AppLauncherMutuallyExclusive);

                //appButton = ApplicationLauncher.Instance.AddApplication(
                //    onAppLaunchToggleOn, onAppLaunchToggleOff,
                //    onAppLaunchHoverOn, onAppLaunchHoverOff,
                //    null, null,
                //    (Texture)Resources.texAppLaunchIcon);
                //appButton.VisibleInScenes = ApplicationLauncher.AppScenes.FLIGHT;

            }
            catch (Exception ex)
            {
                Log.error(ex, "AppLauncher: Failed to set up App Launcher Button");
                retButton = null;
            }
            lstButtons = KerbalAlarmClock.FindObjectsOfType<ApplicationLauncherButton>();
            //LogFormatted("AppLauncher: Creating Button-AFTER", lstButtons.Length);

            return retButton;
        }


        internal void DestroyAppLauncherButton()
        {
            //LogFormatted("AppLauncher: Destroying Button-BEFORE NULL CHECK");
            if (btnAppLauncher != null)
            {
                ApplicationLauncherButton[] lstButtons = KerbalAlarmClock.FindObjectsOfType<ApplicationLauncherButton>();
                Log.detail("AppLauncher: Destroying Button-Button Count:{0}", lstButtons.Length);
                ApplicationLauncher.Instance.RemoveModApplication(btnAppLauncher);
                btnAppLauncher = null;
            }
            //LogFormatted("AppLauncher: Destroying Button-AFTER NULL CHECK");
        }


        internal Boolean AppLauncherToBeSetTrue = false;
        internal DateTime AppLauncherToBeSetTrueAttemptDate;
        internal void SetAppButtonToTrue()
        {
            if (!ApplicationLauncher.Ready)
            {
                Log.dbg("not ready yet");
                AppLauncherToBeSetTrueAttemptDate = DateTime.Now;
                return;
            }
            ApplicationLauncherButton ButtonToToggle = btnAppLauncher;

            if (ButtonToToggle == null)
            {
                Log.dbg("Button Is Null");
                AppLauncherToBeSetTrueAttemptDate = DateTime.Now;
                return;
            }


            if (ButtonToToggle.toggleButton.CurrentState != UIRadioButton.State.True)
            {
                if (AppLauncherToBeSetTrueAttemptDate.AddSeconds(settings.AppLauncherSetTrueTimeOut) < DateTime.Now)
                {
                    AppLauncherToBeSetTrue = false;
                    Log.warn("AppLauncher: Unable to set the AppButton to true - tried for {0} secs", settings.AppLauncherSetTrueTimeOut);
                }
                else
                {
                    Log.detail("Setting App Button True");
                    ButtonToToggle.SetTrue(true);
                }
            }
            else
            {
                AppLauncherToBeSetTrue = false;
            }
        }

        void onAppLaunchToggleOn() {
            Log.dbg("TOn");

            WindowVisibleByActiveScene = true;
            settings.Save();
            Log.dbg("{0}",WindowVisibleByActiveScene);
        }
        void onAppLaunchToggleOff() {
            Log.dbg("TOff");

            WindowVisibleByActiveScene = false;
            settings.Save();
            Log.dbg("{0}", WindowVisibleByActiveScene);
        }
        void onAppLaunchHoverOn() {
            Log.dbg("HovOn");
            //MouseOverAppLauncherBtn = true;
        }
        void onAppLaunchHoverOff() {
            Log.dbg("HovOff");
            //MouseOverAppLauncherBtn = false; 
        }

        //Boolean MouseOverAppLauncherBtn = false;
    }
}
