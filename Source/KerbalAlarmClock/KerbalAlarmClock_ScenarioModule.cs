using System.Linq;

using KSPPluginFramework;

namespace KerbalAlarmClock
{

    [KSPScenario(ScenarioCreationOptions.AddToAllGames, GameScenes.SPACECENTER, GameScenes.EDITOR, GameScenes.FLIGHT, GameScenes.TRACKSTATION)]
    internal class KerbalAlarmClockScenario : ScenarioModule
    {
        public override void OnLoad(ConfigNode gameNode)
        {
            //reset the list here
            //KerbalAlarmClock.alarms = new KACAlarmList();
            KerbalAlarmClock.alarms.RemoveRange(0,KerbalAlarmClock.alarms.Count);

            base.OnLoad(gameNode);
			Log.info("BaseLoadDone. Alarms Count (Should be 0):{0}", KerbalAlarmClock.alarms.Count);

            Log.dbg("OnLoad: {0}", gameNode);

            if (gameNode.HasNode("KerbalAlarmClockScenario")) Log.dbg("Found {0}","KerbalAlarmClockScenario");
            if (gameNode.HasNode("KACAlarmListStorage")) Log.dbg("Found {0}", "KACAlarmListStorage");
            if(gameNode.HasNode("KACAlarmListStorage"))
            {
                KerbalAlarmClock.alarms.DecodeFromCN(gameNode.GetNode("KACAlarmListStorage"));

                foreach (KACAlarm a in KerbalAlarmClock.alarms)
                {
                    if (!a.AlarmActionConverted) {
                        a.AlarmActionConvert = a.AlarmAction;
                        a.AlarmAction = KACAlarm.AlarmActionEnum.Converted;
                        a.AlarmActionConverted = true;
                    }
                }
            }

            Log.detail("ScenarioLoadDone. Alarms Count:{0}", KerbalAlarmClock.alarms.Count);
            //{Log.dbg("A");} else {Log.dbg("B");}
            //KerbalAlarmClock.alarms.DecodeFromCN(gameNode.GetNode(this.GetType().Name));
        }

        public override void OnSave(ConfigNode gameNode)
        {
            base.OnSave(gameNode);

            foreach (KACAlarm a in KerbalAlarmClock.alarms.Where(a=>!a.AlarmActionConverted && a.AlarmAction== KACAlarm.AlarmActionEnum.Converted) ) {
                a.AlarmActionConverted = true;
            }
            foreach (KACAlarm a in KerbalAlarmClock.alarms.Where(a => a.Actions.Warp== AlarmActions.WarpEnum.PauseGame)) {
                a.Actions.Message = AlarmActions.MessageEnum.Yes;
            }

            Log.info("OnSave: {0}", gameNode);
            gameNode.AddNode(KerbalAlarmClock.alarms.EncodeToCN());
        }
    }
}
