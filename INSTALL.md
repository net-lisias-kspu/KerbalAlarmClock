# KerbalAlarmClock /L Unofficial

A management and utility plugin for [Kerbal Space Program](https://www.kerbalspaceprogram.com/)


## Installation Instructions

To install, place the GameData folder inside your Kerbal Space Program folder. Optionally, you can also do the same for the PluginData (be careful to do not overwrite your custom settings):

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
	+ Delete `<KSP_ROOT>/GameData/TriggerTech/KerbalAlarmClock `
* Extract the package's `GameData` folder into your KSP's root:
	+ `<PACKAGE>/GameData` --> `<KSP_ROOT>/`
* Extract the package's `PluginData` folder (if available) into your KSP's root, taking precautions to do not overwrite your custom settings if this is not what you want to.
	+ `<PACKAGE>/PluginData` --> `<KSP_ROOT>/`
	+ You can safely ignore this step if you already had installed it previously and didn't deleted your custom configurable files.

The following file layout must be present after installation:

```
<KSP_ROOT>
	[GameData]
		[TriggerTech]
			[Flags]
				TriggerTechLogo.png
				TriggerTecgLogo2.png
			[KerbalAlarmClock]
				[PluginDara]
					[Data]
						...
					[Textures]
						cur_ResizeBoth.png
						...
					[ToolbarIcons]
						KACIcon-Alarm.png
						...
				[Sounds]
					Alarm1.wav
					...
				CHANGE_LOG.md
				LICENSE
				NOTICE
				README.md
				KerbalAlarmClock.dll
				KerbalAlarmClock.version
		[__LOCAL]
			[TriggerTech]
				[KerbalAlarmClock]
					[Sounds]
						...
		000_KSPe.dll
		ModuleManager.dll
		...
	[PluginData]
		[TriggerTech]
			[KerbalAlarmClock]
				settings.cfg
			...
	KSP.log
	PastDatabase.cfg
	...
```

Please note that the `PluginData` hierarchy can be present only after running the installed plugin by the first time.

The `__LOCAL` stunt is for you add your custom sounds (in any format supported by Unity), so you can delete and reinstall the Add'On without worrying deleting your customizatinos.

### Dependencies

* [KSP API Extensions/L](https://github.com/net-lisias-ksp/KSPAPIExtensions)

