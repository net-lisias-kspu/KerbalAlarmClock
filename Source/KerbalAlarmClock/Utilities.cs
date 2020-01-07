using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Asset = KSPe.IO.Asset<KerbalAlarmClock.KerbalAlarmClock>;

namespace KerbalAlarmClock
{
    internal static class KACUtils
    {
		internal const string PathData = "Data";
		internal const string PathTextures = "Textures";
		internal const string PathToolbarIcons = "ToolbarIcons";
		internal const string PathSounds = "Sounds";

		// TODO: Get rid of this stunt. Try to use S.A.V.E. as a library?
		private static readonly string SavePath = string.Format("{0}saves/{1}", KSPUtil.ApplicationRootPath.Replace("\\", "/"), HighLogic.SaveFolder);

		internal static Boolean BackupSaves()
        {
            if (!KerbalAlarmClock.settings.BackupSaves)
            {
                return true;
            }

            Boolean blnReturn=false;
            Log.dbg("Backing up saves");

            if (!System.IO.Directory.Exists(SavePath))
            {
                Log.dbg("Saves Path not found: {0}");
            }
            else
            {
                if (!System.IO.File.Exists(String.Format("{0}/persistent.sfs", SavePath)))
                {
                    Log.dbg("Persistent.sfs file not found: {0}/persistent.sfs", SavePath);
                }
                else
                {
                    try
                    {
                        System.IO.File.Copy(String.Format("{0}/persistent.sfs", SavePath),
                                            String.Format("{0}/zKACBACKUP{1:yyyyMMddHHmmss}-persistent.sfs", SavePath, DateTime.Now),
                                            true);
                        Log.dbg("Backed Up Persistent.sfs as: {0}/zKACBACKUP{1:yyyyMMddHHmmss}-persistent.sfs", SavePath, DateTime.Now);
                        
                        //Now go for the quicksave
                        if (System.IO.File.Exists(String.Format("{0}/quicksave.sfs", SavePath)))
                        {
                            System.IO.File.Copy(String.Format("{0}/quicksave.sfs", SavePath),
                                                String.Format("{0}/zKACBACKUP{1:yyyyMMddHHmmss}-quicksave.sfs", SavePath, DateTime.Now),
                                                true);
                            Log.dbg("Backed Up quicksave.sfs as: {0}/zKACBACKUP{1:yyyyMMddHHmmss}-quicksave.sfs", SavePath, DateTime.Now);
                        }                        
                        blnReturn = true;

                        PurgeOldBackups();

                    }
                    catch (Exception ex)
                    {
                        Log.dbg("Unable to backup: {0}/persistent.sfs\r\n\t{1}", SavePath,ex.Message);
                    }
                }
            }

            return blnReturn;
        }

        private static void PurgeOldBackups()
        {
            PurgeOldBackups("persistent.sfs");
            PurgeOldBackups("quicksave.sfs");
        }

        private static void PurgeOldBackups(String OriginalName)
        {
            //Now delete any old ones greater than the list to keep
            List<System.IO.FileInfo> SaveBackups = new System.IO.DirectoryInfo(SavePath).GetFiles(string.Format("KACBACKUP*{0}",OriginalName)).ToList<System.IO.FileInfo>();
            SaveBackups.AddRange(new System.IO.DirectoryInfo(SavePath).GetFiles(string.Format("zKACBACKUP*{0}", OriginalName)).ToList<System.IO.FileInfo>());

            Log.dbg("{0} KACBackup...{1} Saves found", SaveBackups.Count,OriginalName);

            List<System.IO.FileInfo> SaveBackupsToDelete = SaveBackups.OrderByDescending(fi => fi.CreationTime).Skip(KerbalAlarmClock.settings.BackupSavesToKeep).ToList<System.IO.FileInfo>();
            Log.dbg("{0} KACBackup...{1} Saves to purge", SaveBackupsToDelete.Count, OriginalName);
            for (int i = SaveBackupsToDelete.Count - 1; i >= 0; i--)
            {
                Log.dbg("\tDeleting {0}", SaveBackupsToDelete[i].Name);

                //bin the loadmeta if it exists too
                string loadmetaFile = SaveBackupsToDelete[i].DirectoryName + "/" + System.IO.Path.GetFileNameWithoutExtension(SaveBackupsToDelete[i].FullName) + ".loadmeta";
                if (System.IO.File.Exists(loadmetaFile))
                {
                    System.IO.File.Delete(loadmetaFile);
                }

                SaveBackupsToDelete[i].Delete();
            }
        }

        //generic function
        internal static String PipeSepVariables(params object[] vars)
        {
            return SepVariables("|", vars);
        }

        internal static String CommaSepVariables(params object[] vars)
        {
            return SepVariables(",", vars);
        }

        internal static String SepVariables(String separator, params object[] vars)
        {
            String strReturn = "";
            foreach (object tmpVar in vars)
            {
                if (strReturn != "") strReturn += separator;
                if (tmpVar == null)
                    strReturn += "";
                else
                {
                    strReturn = EncodeVarStrings(strReturn);
                    strReturn += tmpVar.ToString();
                }
            }
            return strReturn;
        }

        internal static String EncodeVarStrings(String Input)
        {
            String strReturn = Input;
            //encode \r\t\n
            strReturn = strReturn.Replace("\r\n", "\\r\\n");
            strReturn = strReturn.Replace("\r", "\\r");
            strReturn = strReturn.Replace("\n", "\\n");
            strReturn = strReturn.Replace("\t", "\\t");
            return strReturn;
        }

        internal static String DecodeVarStrings(String Input)
        {
            String strReturn = Input;
            //encode \r\t\n
            strReturn = strReturn.Replace("\\r\\n", "\r\n");
            strReturn = strReturn.Replace("\\r", "\r\n");
            strReturn = strReturn.Replace("\\n", "\n");
            strReturn = strReturn.Replace("\\t", "\t");
            return strReturn;
        }

        /// <summary>
        /// Loads a texture from the file system directly
        /// </summary>
        /// <param name="tex">Unity Texture to Load</param>
        /// <param name="FileName">Image file name</param>
        /// <param name="FolderPath">Optional folder path of image</param>
        /// <returns></returns>
        public static Boolean LoadImageFromFile(ref Texture2D tex, String FileName, String FolderPath)
        {
			try
			{
				tex = Asset.Texture2D.LoadFromFile(FolderPath, FileName);
				return true;
			} catch (Exception e)
			{
				Log.error(e, "While loading image {0}/{1}", FolderPath, FileName);
				return false;
			}
        }

        #region "offset building"
        internal static RectOffset SetWindowRectOffset(RectOffset tmpRectOffset, int intValue)
        {
            tmpRectOffset.left = intValue;
            //tmpRectOffset.top = Top;
            tmpRectOffset.right = intValue;
            tmpRectOffset.bottom = intValue;
            return tmpRectOffset;
        }

        internal static RectOffset SetRectOffset(RectOffset tmpRectOffset, int intValue)
        {
            return SetRectOffset(tmpRectOffset, intValue, intValue, intValue, intValue);
        }

        internal static RectOffset SetRectOffset(RectOffset tmpRectOffset, int Left, int Right, int Top, int Bottom)
        {
            tmpRectOffset.left = Left;
            tmpRectOffset.top = Top;
            tmpRectOffset.right = Right;
            tmpRectOffset.bottom = Bottom;
            return tmpRectOffset;
        }
        #endregion

        #region "Math Stuff"
        internal static double Clamp(double x, double min, double max)
        {
            return Math.Min(Math.Max(x, min), max);
        }

        //keeps angles in the range -180 to 180
        internal static double clampDegrees(double angle)
        {
            angle = angle + ((int)(2 + Math.Abs(angle) / 360)) * 360.0; //should be positive
            angle = angle % 360.0;
            if (angle > 180.0) return angle - 360.0;
            else return angle;
        }

        //keeps angles in the range 0 to 360
        internal static double clampDegrees360(double angle)
        {
            angle = angle % 360.0;
            if (angle < 0) return angle + 360.0;
            else return angle;
        }
        #endregion

        #region "Orbital Math"
        /// <summary>
        /// Calculates the current phase angle between <paramref name="origin"/> and <paramref name="destination"/>.
        /// </summary>
        /// <returns>The phase angle.</returns>
        /// <param name="origin">Origin.</param>
        /// <param name="destination">Destination.</param>
        public static double CurrentPhaseAngle(Orbit origin, Orbit destination)
        {
            Vector3d normal = origin.GetOrbitNormal().normalized;
            Vector3d projected = Vector3d.Exclude(normal, destination.pos);
            double result = Vector3d.Angle(origin.pos, projected);
            if (Vector3d.Dot(Vector3d.Cross(origin.pos, projected), normal) < 0)
            {
                return 360.0 - result;
            }
            else
            {
                return result;
            }
        }

        //returns false if there is no AN/DN on the flight plan
        //public static Boolean CalcTimeToANorDN(Vessel vessel, ANDNNodeType typeOfNode, out Double timeToNode)
        //{
        //    Boolean blnReturn = false;
        //    timeToNode = 0;
        //    try
        //    {
        //        //work out the target type, and get the target orbit
        //        //if (FlightGlobals.fetch.VesselTarget != null)
        //        if (KACWorkerGameState.CurrentVesselTarget is Vessel || KACWorkerGameState.CurrentVesselTarget is CelestialBody)
        //        {
        //            Orbit oTarget = KACWorkerGameState.CurrentVesselTarget.GetOrbit();
        //            Vector3d vectVesselPos = vessel.orbit.getRelativePositionAtUT(KACWorkerGameState.CurrentTime.UT);

        //            blnReturn = CalcTimeToANorDN(vectVesselPos, vessel.orbit, oTarget, typeOfNode, out timeToNode);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return blnReturn;
        //}

        //#region "AN/DN Code - predominantly the functions from the Kerbal Engineer Redux by cybutek under Creative commons BY-NC-SA - https://creativecommons.org/licenses/by-nc-sa/3.0/deed.en_GB"
        //public static Boolean CalcTimeToANorDN(Vector3d position, Orbit origin, Orbit target, ANDNNodeType typeOfNode, out Double timeToNode)
        //{
        //    timeToNode = 0d;
        //    Boolean blnReturn = false;
        //    try
        //    {
        //        double AngleToANDN;
        //        if (typeOfNode == ANDNNodeType.Ascending)
        //            AngleToANDN = CalcAngleToAscendingNode(position, origin, target);
        //        else
        //            AngleToANDN = CalcAngleToDescendingNode(position, origin, target);
        //        timeToNode = CalcTimeToNode(origin, AngleToANDN);
        //        blnReturn = true;
        //    }
        //    catch (Exception)
        //    {
        //        //
        //    }
        //    return blnReturn;
        //}

        //public enum ANDNNodeType
        //{
        //    Ascending,
        //    Descending
        //}


        //public static double CalcAngleToAscendingNode(Vector3d position, Orbit origin, Orbit target)
        //{
        //    double angleToNode = 0d;

        //    if (origin.inclination < 90)
        //    {
        //        angleToNode = CalcPhaseAngle(position, GetAscendingNode(origin, target));
        //    }
        //    else
        //    {
        //        angleToNode = 360 - CalcPhaseAngle(position, GetAscendingNode(origin, target));
        //    }

        //    return angleToNode;
        //}

        //public static double CalcAngleToDescendingNode(Vector3d position, Orbit origin, Orbit target)
        //{
        //    double angleToNode = 0d;

        //    if (origin.inclination < 90)
        //    {
        //        angleToNode = CalcPhaseAngle(position, GetDescendingNode(origin, target));
        //    }
        //    else
        //    {
        //        angleToNode = 360 - CalcPhaseAngle(position, GetDescendingNode(origin, target));
        //    }

        //    return angleToNode;
        //}

        //public static Vector3d GetAscendingNode(Orbit origin, Orbit target)
        //{
        //    //get the vector at 90 degrees to the two orbits normal so we see the cross over AN
        //    return Vector3d.Cross(target.GetOrbitNormal(), origin.GetOrbitNormal());
        //}
        //public static Vector3d GetDescendingNode(Orbit origin, Orbit target)
        //{
        //    //get the vector at 90 degrees to the two orbits normal so we see the cross over AN
        //    return Vector3d.Cross(origin.GetOrbitNormal(), target.GetOrbitNormal());
        //}

        //public static double CalcPhaseAngle(Vector3d origin, Vector3d target)
        //{
        //    //angle between the two vectors
        //    double phaseAngle = Vector3d.Angle(target, origin);
        //    if (Vector3d.Angle(Quaternion.AngleAxis(90, Vector3d.forward) * origin, target) > 90)
        //    {
        //        phaseAngle = 360 - phaseAngle;
        //    }
        //    return (phaseAngle + 360) % 360;
        //}
        //public static double CalcTimeToNode(Orbit origin, double angleToNode)
        //{
        //    return (origin.period / 360d) * angleToNode;
        //}
        //#endregion

        #region "timeOfClosestApproach Code - "
        internal static Vector3d getAbsolutePositionAtUT(Orbit orbit, double UT)
        {
            Vector3d pos = orbit.getRelativePositionAtUT(UT);
            pos += orbit.referenceBody.position;
            return pos;
        }

        internal static double timeOfClosestApproach(Orbit oOrig, Orbit oTgt, double timeStart, out double closestdistance)
        {
            return timeOfClosestApproach(oOrig, oTgt, timeStart, oOrig.period, 20, out closestdistance);
        }

        internal static double timeOfClosestApproach(Orbit oOrig, Orbit oTgt, double timeStart, int orbit, out double closestdistance)
        {
            double period = oOrig.period;
            double start = timeStart + (orbit - 1) * period;
            double len = period;
            if (oOrig.eccentricity >= 1) {
                start = timeStart;
                len = oOrig.timeToPe + oTgt.period;
                //UnityEngine.Debug.Log($"timeOfClosestApproach {start} {len} {ta} {E} {M} {Planetarium.GetUniversalTime ()}");
            }
            //return timeOfClosestApproach(a, b, time + ((orbit - 1) * a.period), (orbit * a.period), 20, out closestdistance);
            return timeOfClosestApproach(oOrig, oTgt, start, len, 20, out closestdistance);
        }

        internal static double timeOfClosestApproach(Orbit oOrig, Orbit oTgt, double timeStart, double periodtoscan, double numDivisions, out double closestdistance)
        {
            double closestApproachTime = timeStart;
            double closestApproachDistance = Double.MaxValue;
            double minTime = timeStart;
            double maxTime = timeStart + periodtoscan;

            //work out iterations for precision - we only really need to within a second - so how many iterations do we actually need
            //Each iteration gets us 1/10th of the period to scan

            for (int iter = 0; iter < 8; iter++)
            {
                double dt = (maxTime - minTime) / numDivisions;
                for (int i = 0; i < numDivisions; i++)
                {
                    double t = minTime + i * dt;
                    double distance = (getAbsolutePositionAtUT(oOrig, t) - getAbsolutePositionAtUT(oTgt, t)).magnitude;
                    if (distance < closestApproachDistance)
                    {
                        closestApproachDistance = distance;
                        closestApproachTime = t;
                    }
                }
                minTime = KACUtils.Clamp(closestApproachTime - dt, timeStart, timeStart + periodtoscan);
                maxTime = KACUtils.Clamp(closestApproachTime + dt, timeStart, timeStart + periodtoscan);
            }

            closestdistance = closestApproachDistance;
            return closestApproachTime;
        }

        internal static double timeOfTargetDistance(Orbit oOrig, Orbit oTgt, double timeStart, int orbit, out double closestdistance, double targetDistance)
        {
            //return timeOfClosestApproach(a, b, time + ((orbit - 1) * a.period), (orbit * a.period), 20, out closestdistance);
            return timeOfTargetDistance(oOrig, oTgt, timeStart + ((orbit - 1) * oOrig.period), oOrig.period, 20, out closestdistance,targetDistance);
        }

        internal static double timeOfTargetDistance(Orbit oOrig, Orbit oTgt, double timeStart, double periodtoscan, double numDivisions, out double closestdistance, double targetDistance)
        {
            double closestApproachTime = timeStart;
            double closestApproachDistance = Double.MaxValue;
            double minTime = timeStart;
            double maxTime = timeStart + periodtoscan;

            //work out iterations for precision - we only really need to within a second - so how many iterations do we actually need
            //Each iteration gets us 1/10th of the period to scan

            for (int iter = 0; iter < 8; iter++)
            {
                double dt = (maxTime - minTime) / numDivisions;
                for (int i = 0; i < numDivisions; i++)
                {
                    double t = minTime + i * dt;
                    double distance = (getAbsolutePositionAtUT(oOrig, t) - getAbsolutePositionAtUT(oTgt, t)).magnitude;
                    if (Math.Abs(distance -targetDistance) < closestApproachDistance)
                    {
                        closestApproachDistance = Math.Abs(distance - targetDistance);
                        closestApproachTime = t;
                    }
                }
                minTime = KACUtils.Clamp(closestApproachTime - dt, timeStart, timeStart + periodtoscan);
                maxTime = KACUtils.Clamp(closestApproachTime + dt, timeStart, timeStart + periodtoscan);
            }

            closestdistance = closestApproachDistance+targetDistance;
            return closestApproachTime;
        }


        internal static double timeOfTargetAltitude(Orbit oOrig, double timeStart, out double closestdistance, double targetDistance)
        {
            //return timeOfClosestApproach(a, b, time + ((orbit - 1) * a.period), (orbit * a.period), 20, out closestdistance);
            return timeOfTargetAltitude(oOrig,  timeStart , 20, out closestdistance, targetDistance);
        }

        //enum OrbitAltitudeDirection
        //{
        //    Ascending=1,
        //    Descending=2
        //}

        internal static double timeOfTargetAltitude(Orbit oOrig, double timeStart, double numDivisions, out double closestdistance, double targetAltitude)
        {
            double closestApproachTime = timeStart;
            double closestApproachAltitude = Double.MaxValue;
            double minTime = timeStart;

            //work out the end of the period to scan based on whether we are above or below the target altitude
            double UTofEndOrbitAltDirection;
            double startAltitude = oOrig.getRelativePositionAtUT(timeStart).magnitude-oOrig.referenceBody.Radius;
            if (startAltitude < targetAltitude)
                UTofEndOrbitAltDirection = oOrig.StartUT + oOrig.timeToAp;
            else
                UTofEndOrbitAltDirection = oOrig.StartUT + oOrig.timeToPe;

            double periodtoscan = UTofEndOrbitAltDirection - timeStart;
            double maxTime = UTofEndOrbitAltDirection;

            //work out iterations for precision - we only really need to within a second - so how many iterations do we actually need
            //Each iteration gets us 1/10th of the period to scan

            for (int iter = 0; iter < 8; iter++)
            {
                double dt = (maxTime - minTime) / numDivisions;
                for (int i = 0; i < numDivisions; i++)
                {
                    double t = minTime + i * dt;
                    double distance = oOrig.getRelativePositionAtUT(t).magnitude-oOrig.referenceBody.Radius;

                    //TODO: NEED TO DO SOMETHING HERE WITH PRECISION!!!! How many decimal place to compare to
                    //something about = if its not substantially closer, but is substantialy later then ignore it.
                    //ie if the later crossing is 1meter closer, but its 600 secs later then keep the earlier one
                    if (Math.Abs(distance - targetAltitude) < closestApproachAltitude)
                    {
                        closestApproachAltitude = Math.Abs(distance - targetAltitude);
                        closestApproachTime = t;
                    }
                }
                minTime = KACUtils.Clamp(closestApproachTime - dt, timeStart, timeStart + periodtoscan);
                maxTime = KACUtils.Clamp(closestApproachTime + dt, timeStart, timeStart + periodtoscan);
            }

            closestdistance = closestApproachAltitude + targetAltitude;
            return closestApproachTime;
        }


        #endregion

        #endregion

    }
}
