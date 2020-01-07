using System;

using KACToolbarWrapper;

namespace KerbalAlarmClock
{
    public partial class KerbalAlarmClock
    {
        internal IButton btnToolbarKAC = null;
        internal Boolean BlizzyToolbarIsAvailable = false;

        /// <summary>
        /// Check to see if the Toolbar is available
        /// </summary>
        /// <returns>True if the Toolbar.ToolbarManager class is loaded in an existing assembly</returns>
        internal Boolean HookToolbar()
        {
            //Is the Dll in memory
            Boolean blnReturn = ToolbarManager.ToolbarAvailable;
            Log.detail("Blizzy's Toolbar Loaded:{0}", blnReturn);
            return blnReturn;
        }


        /// <summary>
        /// initialises a Toolbar Button for this mod
        /// </summary>
        /// <returns>The ToolbarButtonWrapper that was created</returns>
        internal IButton InitToolbarButton()
        {
            IButton btnReturn = null;
            try
            {
                Log.detail("Initialising the Toolbar Icon");
                btnReturn = ToolbarManager.Instance.add("KerbalAlarmClock", "btnToolbarIcon");
                btnReturn.TexturePath = KACResources.GetIconTexturePath(KACUtils.PathToolbarIcons, "KACIcon-Norm");
                btnReturn.ToolTip = "Kerbal Alarm Clock";
                btnReturn.OnClick += (e) =>
                {
                    WindowVisibleByActiveScene = !WindowVisibleByActiveScene;
                    settings.Save();
                };
            }
            catch (Exception ex)
            {
                DestroyToolbarButton(btnReturn);
                Log.error(ex, "Error Initialising Toolbar Button");
            }
            return btnReturn;
        }

        /// <summary>
        /// Destroys theToolbarButtonWrapper object
        /// </summary>
        /// <param name="btnToDestroy">Object to Destroy</param>
        internal void DestroyToolbarButton(IButton btnToDestroy)
        {
            if (btnToDestroy != null)
            {
                Log.detail("Destroying Toolbar Button");
                btnToDestroy.Destroy();
            }
            btnToDestroy = null;
        }

    }
}
