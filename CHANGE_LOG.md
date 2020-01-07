# Kerbal Alarm Clock :: Change Log

* 2014-0726: 2.7.8.0 (TriggerAU) for KSP 0.24.2
	+ Download pulled due to bug
	+ Compiled against 0.24.2 binaries
	+ Fixed click through in KSC \* used InputLockManager and the ControlType of KSCFacilities (Fixes #45)
	+ Added Transfer Windows type to SC Alarms (Fixes #46)
	+ `KSP Version: 0.24.2`
* 2014-0718: 2.7.7.0 (TriggerAU) for KSP 0.24.0
	+ Compiled against 0.24 binaries
	+ 4th attempt at texture blurring (Issue #33)
	+ `KSP Version: 0.24.0`
* 2014-0714: 2.7.6.1 (TriggerAU) for KSP 0.23.5
	+ Reversed changed of Images to TGA \* Back to PNGs (Issue #33)
	+ `KSP Version: 0.23.5`
* 2014-0712: 2.7.6.0 (TriggerAU) for KSP 0.23.5
	+ Change ability to Jump to Asteroid to be an Option \* default is disabled (Issue #37)
	+ Fixed issue with Time Margin Entry and putting in large numbers (Issue #36)
	+ Added new code to detect when window is repositioned and save it after its stopped moving (Issue#35)
	+ Changed all Images to TGA format to fix compression artifacts (Issue #33)
	+ Added function to set default Date for Raw type alarms to start at today + 10mins (Issue #31)
	+ `KSP Version: 0.23.5`
* 2014-0625: 2.7.5.0 (TriggerAU) for KSP 0.23.5
	+ Tweaked Update check to be on a background worker process (Issue #28)
	+ Added capability to add certain alarm types to SC and TS Scenes (Issue #24)
	+ Added list icons for Raw and Earth alarms (Issue #29)
	+ License Changed to MIT license (less restrictive than previous)
	+ `KSP Version: 0.23.5`
* 2014-0609: 2.7.4.0 (TriggerAU) for KSP 0.23.5
	+ Removed log message on periodic saves (Issue #25)
	+ Window height now adjusts for Multiline Alarms (Issue #15)
	+ Closest Approach Alarms now allow target of Docking Port (Issue #1)
	+ Fixed duplication of Manuever Nodes on Ship Jump (Issue #23)
	+ Changed latest version file source to be Google site (smaller file)
	+ `KSP Version: 0.23.5`
* 2014-0517: 2.7.3.0 (TriggerAU) for KSP 0.23.5
	+ Fixed the pesky bug with the auto alarm periods not saving
	+ `KSP Version: 0.23.5`
* 2014-0517: 2.7.2.0 (TriggerAU) for KSP 0.23.5
	+ Adding Warp halting code to TS and SC
	+ Added extra code for guiload when texture loads fail
	+ `KSP Version: 0.23.5`
* 2014-0517: 2.7.1.0 (TriggerAU) for KSP 0.23.5
	+ 0.23.5 Recompile for new version of Unity/KSP
	+ Added functionality to detect time display type and match KAC times accordingly
	+ `KSP Version: 0.23.5`
* 2014-0517: 2.7.0.0 (TriggerAU) for KSP 0.23
	+ 0.23 Recompile for new version of Unity
	+ Added Option to choose to use Blizzy's excellent toolbar \* uses latebinding so no need to include DLL and no hard reference
	+ Added link in game so if common toolbar not installed people can jump to forum page
	+ Added option so minimal display will show the next or oldest alarm
	+ Changed texture loading to use GameDB and this should fix Linux x64 issues with the Unity Texture2D Loader crashing
	+ Fixed bug where twitching SOIs on large orbits no longer creates lots of alarms for Auto-SOIs
	+ Fixed Bug using wrong path separators for MacOS/Linux re jumping from non-flight scenes
	+ Fixed Bug displaying alarms in non-flight screens when KAC not visible
	+ `KSP Version: 0.23`
* 2014-0517: 2.6.4.0 (TriggerAU) for KSP 0.22
	+ Added a threshold for Auto adding Maneuver Node alarms, so if you are closer than the threshold it wont make an alarm
	+ Added capacity to Kerbal alarms to store and restore targets and maneuvers regardless of vessel
	+ Fixed Alarm Icon in non Flight Scenes showing warp indicator
	+ Fixed some non-persistant settings in last few updates
	+ `KSP Version: 0.22`
* 2014-0517: 2.6.3.0 (TriggerAU) for KSP 0.22
	+ Tweaked functionality to for ship jumping and backups (now includes self closing alert dialog)
	+ Added Functionality to SOI Auto Alamrs to allow ignoring EVA Kerbals for Auto Add Alarms
	+ Added Functionality to Allow Auto Adding Maneuver Node Alarms
	+ Fixed some case issues with a couple of image files
	+ `KSP Version: 0.22`
* 2014-0517: 2.6.2.0 (TriggerAU) for KSP 0.22
	+ Added functionality to allow you to jump to a ship from space center and tracking station view lists
	+ Added functionality to backup save files before switching ships using KAC
	+ Both these features are configurable
	+ `KSP Version: 0.22`
* 2014-0517: 2.6.1.0 (TriggerAU) for KSP 0.22
	+ Recompiled it for 0.22
	+ Added Crew Alarms (track Kerbal rather than Vessel)
	+ Added Distance Target Alarms \* distance from target vessel or altitude above planet
	+ Added Launch Rendezvous Alarm (under Ascending/Descending Node for Landed craft) \* MechJeb2 code \* thanks r4m0n
	+ Allow restoration of Nodes that you have passed (useful for interplanetary burns)
	+ Added missing Dres Transfer Model data \* thanks Voneiden
	+ Added view only version of Alarm clock to both Space Center and Tracking Station
	+ Added options to hide/move the icon for all three visible scenes
	+ `KSP Version: 0.22`
* 2014-0517: 2.5.0.0 (TriggerAU) for KSP 0.21.1
	+ Recompiled it for 0.21
	+ Fixed some issues with Hyperbolic orbits and AN/DN Nodes
	+ `KSP Version: 0.21.1`
* 2014-0517: 2.4.0.2 (TriggerAU) for KSP 0.20.2
	+ Fixed stupid mistake AN/DN GUI Code
	+ `KSP Version: 0.20.2`
* 2014-0517: 2.4.0.1 (TriggerAU) for KSP 0.20.2
	+ Changed AN/DN code to use Mechjeb style ones to correct for my lack of math skills
	+ Changed AN/DN Add so if there is no target it displays the equatorial AN/DN times
	+ Adjusted Future orbits code to fix rounding issues
	+ Added capacity to edit the frequency of checks \* people can now tweak this for their system and helps prevent severe warp changes that can affect vesel orbits
	+ Adjusted delete alarm on close code so alarm did not reappear
	+ Fixed time display setting not persisting
	+ Sorted out Earth alarms not updating when paused
	+ Fixed Line wrapping on Add Alarm window (still need to work out the right answer for the main window)
	+ Some other Graphical tweaks
	+ `KSP Version: 0.20.2`
* 2014-0517: 2.3.0.0 (TriggerAU) for KSP 0.20.2
	+ Added alarms for "Earth" Universe
	+ Added alarms for closest distance between vessels \* using some of r4m0n's timeToClosestApproach function from MechJeb 1.9.8 under GPLv3.0
	+ Combined Ap/PE and AN/DN alarm buttons to get some more room
	+ Restructured Save File for future improvements
	+ Fixed issue with alarms refiring on vessel changes
	+ Reverted the texture loading to be via KSP.IO to remove compression artifacts from GUI
	+ `KSP Version: 0.20.2`
* 2014-0517: 2.1.2.0 (TriggerAU) for KSP 0.20.2
	+ Verified year calcs
	+ Adjusted Date Entry for Raw alarms and date displays to show Year 1, Day 1 based stuff so the clocks in tracking station, etc and the alarms all match display wise. Also means you can now create an alarm using the dates from external apps like the transfer pork chops without doing maths
	+ Fixed bug where Pe Node was not detected if there was no Ap Node on the flight plan
	+ Fixed bug with autogenerated and auto recalc alarms, by introducing save of the alarms file periodically and on alarm creation (can't rely on memeory in 0.20)
	+ Removed initial config.xml file from package (and added additional code) so upgrades should now maintain alarms AND settings
	+ `KSP Version: 0.20.2`
* 2014-0517: 2.1.1.0 (TriggerAU) for KSP 0.20.2
	+ Fixed issue with Pause alarms not slowing down warp first
	+ `KSP Version: 0.20.2`
* 2014-0517: 2.1.0.0 (TriggerAU) for KSP 0.20.2
	+ Tweaked for 0.20.2
	+ Added functionality to Store/Restore Vessel Targets for AN/DN Alarms
	+ Expanded Store/Restore of Maneuver Nodes to include a list of all nodes from time of alarm
	+ Resolved an issue with the SOI recalc code that was resetting all SOI Alarms to one time
	+ Restored the "Jump and Restore" functionality that stopped working in 0.20
	+ Improved the Save/Load routines to compartmentalise them
	+ `KSP Version: 0.20.2`
* 2014-0517: 2.0.0.0 (TriggerAU) for KSP 0.20
	+ Rebuild and rewrite for 0.20 Game Database Loading Structure
	+ Also fixes for \* only recalc alarms when under Physwarp or nowarp \* stops creep at high warp
	+ Reset Defaults for autocalc to prevent creep
	+ Reset Defaults for Transfer Mode
	+ Added position of icon to config.xml so people can move it around as needed
	+ `KSP Version: 0.20`
* 2014-0517: 1.4.1.0 (TriggerAU) for KSP 0.20
	+ Recompile and minor changes to package structure to facilitate 0.20 Legacy Mode
	+ `KSP Version: 0.20`
* 2014-0517: 1.4.0.3 (TriggerAU) for KSP 0.19.1
	+ Minor changes to facilitate Linux paths and case sensitivity
	+ `KSP Version: 0.19.1`
* 2014-0517: 1.4.0.2 (TriggerAU) for KSP 0.19.1
	+ Changed resource loading method to use direct file access \* prevents some peoples issues with timeouts
	+ Added new Add Alarm window format \* big change...
	+ Added new alarm types \* Apoapsis, Periapsis, Ascending Node, Descending Node \* thanks to Cybutek for use of his AN/DN calc functions from the Kerbal Engineer
	+ These alarms can be set to adjust if the flightplan changes
	+ Added a 2nd form of transfer calculation for transfers between bodies orbiting Kerbol \* this uses voneiden'd excellent modelled data
	+ Ability to disconnect alarm from ships (and see this)
	+ Added another time format \* hh:MM:ss \* can toggle between them
	+ Added extra links to about tab
	+ And all the things I could remmeber from forum posts/PM's before the great crash :P
	+ `KSP Version: 0.19.1`
* 2014-0517: 1.3.5.1 (TriggerAU) for KSP 0.19.1
	+ New Documentation Site link
	+ Tidied up a minor GUI layout or two
	+ `KSP Version: 0.19.1`
* 2014-0517: 1.3.5.0 (TriggerAU) for KSP 0.19.1
	+ Added functionality to alarm windows to Delete on Close
	+ Added Default Settings for Alarm Action, Margin Period and Delete on Close
	+ Updated margin entry/timeentry control to be more robust and granular (now includes seconds)
	+ Added Margin to Auto SOI Alarms
	+ Ability to Edit Margins on existing alarms
	+ Indicator for alarm being edited
	+ A few other bugs
	+ Fixed up lots of annoying GUI design stuff \* finalised main,settings, edit and alarm windows \* new Add window is next
	+ `KSP Version: 0.19.1`
* 2014-0517: 1.3.3.0 (TriggerAU) for KSP 0.19.1
	+ Tweaked Orbital Transfer lists
	+ `KSP Version: 0.19.1`
* 2014-0517: 1.3.0.0 (TriggerAU) for KSP 0.18.4
	+ Added storing and retreiving the maneuver node tied to an alarm
	+ Added ability to jump to the ship that the alarm was created on
	+ Add flight path SOI detection and alarm creation \* when enabled will automatically create alarms for SOI changes and then adjust and maintain them for you
	+ Separated alarm storage from main config file \* now a file per save game
	+ `KSP Version: 0.18.4`
* 2014-0517: 1.2.0.1 (TriggerAU) for KSP 0.18.4
	+ Resolved a few more GUI Issues
	+ `KSP Version: 0.18.4`
* 2014-0517: 1.2.0.0 (TriggerAU) for KSP 0.18.4
	+ Resolved a few more GUI Issues
	+ Added Web update check
	+ Added Settings Pane with a few global options
	+ Added Sphere of Influence Change detection
	+ Added Ability to work in UT as well as days,hours
	+ Added Pause on alarm option
	+ Added configurable scrolling view of alarms
	+ Added View/Edit capability to existing Alarms
	+ `KSP Version: 0.18.4`
* 2014-0517: 1.0.1.0 (TriggerAU) for KSP 0.18.4
	+ Resolved some GUI Issues
	+ `KSP Version: 0.18.4`
* 2014-0517: 1.0.0.0 (TriggerAU) for KSP 0.18.4
	+ Initial Release
	+ Allows for creating Raw Alarms
	+ Allows for creating Alarms based on Manuever Nodes
	+ `KSP Version: 0.18.4`
