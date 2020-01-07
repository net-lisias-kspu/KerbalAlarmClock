# Kerbal Alarm Clock :: Changes

* 2020-0107: 3.12.0.1 (Lisias) for KSP >= 1.4.1
	+ Making the thing compatible again downto KSP 1.4.1 (1.4.0 not tested, but who uses it anyway?)
		- Small annoyances on UI resizing and Crew name displays are expected
	+ Fixed a deprecated call (for Unity 2017! Gosh!)
	+ Added KSPe facilities:
		- for Logging
		- For Assets
			- Moved Textures to PluginData to prevent them for being double loaded.
		- For Data files
	+ Audio Clips now can be added in any format supported by Unity (and not only WAV anymore).
		- Yes, MP3 and OGG :)
	+ Supporting for custom audio files on `<KSP>/GameData/__LOCAL/TriggerTech/KerbalAlarmClock`
		- Now you don't need to tamper the default installments with custom artifacts
		- Safer and easier updates
	+ Moved configuration settings to `<KSP>/PluginData/TriggerTech/KerbalAlarmClock`.
		- Safer and easier updates	 

