/* Part of KSPPluginFramework
Version 1.2

Forum Thread:https://forum.kerbalspaceprogram.com/topic/60381-ksp-plugin-framework-plugin-examples-and-structure/
Author: TriggerAu, 2014
License: The MIT License (MIT)
*/
using System;

using Log = KerbalAlarmClock.Log;
using Data = KSPe.IO.Data<KerbalAlarmClock.KerbalAlarmClock>;

namespace KSPPluginFramework
{ 
    public abstract class ConfigNodeStorage : IPersistenceLoad, IPersistenceSave
    {
        #region Constructors
        /// <summary>
        /// Class Constructor
        /// </summary>
        public ConfigNodeStorage() {
			this.configNode = Data.ConfigNode.For(this.GetType().Name);
		}
        #endregion

		private Data.ConfigNode configNode;

        #region Interface Methods
        /// <summary>
        /// Wrapper for our overridable functions
        /// </summary>
        void IPersistenceLoad.PersistenceLoad()
        {
            OnDecodeFromConfigNode();
        }
        /// <summary>
        /// Wrapper for our overridable functions
        /// </summary>
        void IPersistenceSave.PersistenceSave()
        {
            OnEncodeToConfigNode();
        }

        /// <summary>
        /// This overridable function executes whenever the object is loaded from a config node structure. Use this for complex classes that need decoding from simple Data.ConfigNode values
        /// </summary>
        public virtual void OnDecodeFromConfigNode() { }
        /// <summary>
        /// This overridable function executes whenever the object is encoded to a config node structure. Use this for complex classes that need encoding into simple Data.ConfigNode values
        /// </summary>
        public virtual void OnEncodeToConfigNode() { }
        #endregion

        /// <summary>
        /// Test whether the configured FilePath exists
        /// </summary>
        /// <returns>True if its there</returns>
        public Boolean FileExists
        {
            get
            {
                return this.configNode.IsLoadable;
            }
        }
    
        /// <summary>
        /// Loads the object from the Data.ConfigNode structure in the previously supplied file
        /// </summary>
        /// <returns>Succes of Load</returns>
        public Boolean Load()
        {
			Log.dbg("Loading Data.ConfigNode");

			Boolean blnReturn = false;
            try
            {
                if (FileExists)
                {
                    //Load the file into a config node
                    this.configNode.Load();
					//plug it in to the object
					ConfigNode.LoadObjectFromConfig(this, this.configNode.Node);
                    blnReturn = true;
                }
                else
                {
                    Log.warn("File could not be found to load({0})", this.configNode.Path);
                    blnReturn = false;
                }
            }
            catch (Exception ex)
            {
                Log.error(ex, "Failed to Load Data.ConfigNode from file({0})", this.configNode.Path);
				string emergencyFilename =  String.Format("{0}.err-{1}", this.configNode.Path, string.Format("ddMMyyyy-HHmmss", DateTime.Now));
				Log.warn("Storing old config - {0}", emergencyFilename);
                System.IO.File.Copy(this.configNode.Path, emergencyFilename, true);
                blnReturn = false;
            }
            return blnReturn;
        }

        /// <summary>
        /// Saves the object to a Data.ConfigNode structure in the previously supplied file
        /// </summary>
        /// <returns>Succes of Save</returns>
        public Boolean Save()
        {
            Log.dbg("Saving Data.ConfigNode");

            Boolean blnReturn = false;
            try
            {
                //Encode the current object
                ConfigNode cnToSave = this.AsConfigNode;

				//Wrap it in a node with a name of the class
				ConfigNode cnSaveWrapper = new ConfigNode(this.GetType().Name);
				cnSaveWrapper.AddNode(cnToSave);

				//Save it to the file
				this.configNode.Save(cnSaveWrapper);
                blnReturn = true;
            }
            catch (Exception ex)
            {
                Log.error(ex, "Failed to Save ConfigNode to file({0})", this.configNode.Path);
                blnReturn = false;
            }
            return blnReturn;
        }

        /// <summary>
        /// Returns the current object as a Data.ConfigNode
        /// </summary>
        public ConfigNode AsConfigNode
        {
            get
            {
                try
                {
                    //Create a new Empty Node with the class name
                    ConfigNode cnTemp = new ConfigNode(this.GetType().Name);
                    //Load the current object in there
                    cnTemp = ConfigNode.CreateConfigFromObject(this, cnTemp);
                    return cnTemp;
                }
                catch (Exception ex)
                {
                    Log.error(ex, "Failed to generate Data.ConfigNode");
                    //Logging and return value?                    
                    return new ConfigNode(this.GetType().Name);
                }
            }
        }


        #region Assembly/Class Information
        /// <summary>
        /// Name of the Assembly that is running this MonoBehaviour
        /// </summary>
        internal static String _AssemblyName
        { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; } }

        /// <summary>
        /// Full Path of the executing Assembly
        /// </summary>
        internal static String _AssemblyLocation
        { get { return System.Reflection.Assembly.GetExecutingAssembly().Location; } }

        /// <summary>
        /// Folder containing the executing Assembly
        /// </summary>
        internal static String _AssemblyFolder
        { get { return System.IO.Path.GetDirectoryName(_AssemblyLocation); } }

        #endregion  

    }
}