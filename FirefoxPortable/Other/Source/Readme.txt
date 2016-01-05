Firefox Portable Launcher
=========================
Copyright 2004-2008 John T. Haller of PortableApps.com

Website: http://PortableApps.com/FirefoxPortable

This software is OSI Certified Open Source Software.
OSI Certified is a certification mark of the Open Source Initiative.

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

ABOUT FIREFOX PORTABLE
======================
The Firefox Portable Launcher allows you to run Firefox from a removable drive
whose letter changes as you move it to another computer.  The browser and the
profile can be entirely self-contained on the drive and then used on any Windows
computer.  Specific configuration changes are made to the chrome.rdf so that
your extensions will still work as the drive letter changes.


LICENSE
=======
This code is released under the GPL.  Within the FirefoxPortableSource directory
you will find the code (FirefoxPortable.nsi) as well as the full GPL license
(License.txt).  If you use the launcher or code in your own product, please give
proper and prominent attribution.


INSTALLATION / DIRECTORY STRUCTURE
==================================
By default, the program expects the following directory structure:

-\ <--- Directory with FirefoxPortable.exe
	+\App\
		+\firefox\
	+\Data\
		+\profile\
		+\settings\
		+\plugins\ (optional)
		
The above files may also be placed in a FirefoxPortable directory with the 
FirefoxPortable.exe launcher a directory above that.

It can be used in other directory configurations by including the FirefoxPortable.ini
file in the same directory as FirefoxPortable.exe and configuring it as detailed in
the INI file section below.


FIREFOXPORTABLE.INI CONFIGURATION
=================================
The Firefox Portable Launcher will look for an ini file called FirefoxPortable.ini
within its directory.  If you are happy with the default options, it is not necessary,
though.  There is an example INI included with this package to get you started.  The
INI file is formatted as follows:

[FirefoxPortable]
FirefoxDirectory=App\firefox
ProfileDirectory=Data\profile
SettingsDirectory=Data\settings
PluginsDirectory=Data\plugins
FirefoxExecutable=firefox.exe
AdditionalParameters=
LocalHomepage=
WaitForFirefox=false
DisableSplashScreen=false
AllowMultipleInstances=false
DisableIntelligentStart=false
SkipCompregFix=false
RunLocally=false

The FirefoxDirectory, ProfileDrectory, SettingsDirectory and PluginsDirectory entries
should be set to the *relative* path to the directories containing firefox.exe, your
profile, your plugins, etc. from the current directory.  All must be a subdirectory
(or multiple subdirectories) of the directory containing FirefoxPortable.exe.  The
default entries for these are described in the installation section above.

The FirefoxExecutable entry allows you to set the Firefox Portable Launcher to use
an alternate EXE call to launch firefox.  This is helpful if you are using a machine
that is set to deny firefox.exe from running.  You'll need to rename the firefox.exe
file and then enter the name you gave it on the firefoxexecutable= line of the INI.

The AdditionalParameters entry allows you to pass additional commandline parameter
entries to firefox.exe.  Whatever you enter here will be appended to the call to
firefox.exe.

The LocalHomepage entry allows you to set Firefox Portable to use a local file on your
read-only disc (CD/DVD/etc) as your homepage when running in Live mode.  It's not necessary
for use on regular drives (the launcher handles portablizing your homepage path).  The
file must be a path relative to the launcher. If you were to set it to a file in the
same directory as FirefoxPortable.exe, you would use LocalHomepage=homepage.html  If it
was in a subdirectory called homepage, you would enter LocalHomepage=homepage/homepage.html.

The WaitForFirefox entry allows you to set the launcher to remain active until Firefox has
closed.  This is useful if you wish to have another process wait until Firefox Portable
has closed.  The launcher will automatically wait for Firefox to close in cases where it
needs to clean up after firefox.exe (for instance, to clean up %APPDATA%\Mozilla\Firefox
when there is no locally-installed Firefox).

The DisableSplashScreen entry allows you to run the Firefox Portable Launcher without the
splash screen showing up.  The default is false.

The AllowMultipleInstances entry will allow Firefox Portable to run alongside your
regular local copy of Firefox if you set it to true (lowercase).  The default is false.

The DisableIntelligentStart entry allows you to to have Firefox Portable run its chrome
and component registry fixes on every start.  Normally, it tracks when you've moved to a
new path (switching PCs for instance) and only processes the chrome and component
registry when you do.  By skipping it when the path is the same, Firefox Portable starts
up faster.  But, if you copy a profile into Firefox Portable between sessions (it handles
a copy in on first run automatically), it won't know to process these.  This usually
happens if you copy a profile into Firefox Portable from your local PC on a regular basis
with a sync utility that doesn't work with Firefox Portable (like Portable Apps Sync
does).  Setting this to true causes Firefox Portable to process each on every start.

The SkipCompregFix entry allows you to set Firefox Portable not to adjust the component
registry (compreg.dat) for certain extension compatibility on launch.  It is useful if
you are only using Firefox Portable on computers you control and are able to have the
drive letter set the same each time or if you are not using extensions which make use of
the component registry (like Forecast Fox or the Mozilla Calendar) as Firefox Portable
will launch more quickly.  Set it to true (lowercase) to skip chrome.rdf processing.  The
default is false.

The RunLocally entry allows you to set Firefox Portable to copy your profile, plugins and
Firefox binaries to the local machine's temp directory.  This can be useful for instances
where you'd like to run Firefox Portable from a CD (aka Firefox Portable Live) or when
you're working on a machine that may have spyware or viruses and you'd like to keep your
device set to read-only mode.  The only caveat is, of course, that any changes you make
that session (cookies, bookmarks, etc) aren't saved back to your device.  When done
running, the local temp directories used by Firefox Portable are removed. RunLocally does
not currently work with AllowMultipleInstances as it cannot track which version of
Firefox is running.


PROGRAM HISTORY / ABOUT THE AUTHORS
===================================
This launcher contains suggestions from multiple sources.  It began as a batch file launcher
written by myself (John T. Haller) and posted to the mozillaZine.org thread about running
Firefox from a USB key.  tracon later released a launcher called fflaunch which I
enhanced and re-released as Firefox Portable.  mai9 later improved on fflaunch's
techniques and released it as Free The Fox.  Multiple suggestions back and forth from mai9,
myself and others lead to the launcher we have today.  This most recent version adds my
methods for allowing the code to be run from anywhere on first launch (as opposed to a
specific directory), pass in commandline options, run without an ini file, allow the use
of profiles from local installations, intelligent startup (the launcher determines whether
paths have changed) and more.


CURRENT LIMITATIONS
===================
INCOMPATIBLE EXTENSIONS - Certain extensions use additional local files or prefs.js in a
non-standard way to store information, neither of which are handled by the Firefox Portable
launcher when moving between machines.

NO PORTABLE JAVA - Sun's Java VM needs to be installed locally as Firefox looks for specific
registry keys to run Java. There is no way to make it portable at present, so you will only
be able to use Java-enabled sites on machines that have the Sun Java VM installed locally.