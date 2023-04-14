# RetiledActionCenter - Windows Phone 8.1-like Action Center UI for the
#                       Retiled project.
# Copyright (C) 2021-2023 Drew Naylor
# (Note that the copyright years include the years left out by the hyphen.)
# (This file is based off RetiledStart, hence the copyright including 2021.)
# Windows Phone and all other related copyrights and trademarks are property
# of Microsoft Corporation. All rights reserved.
#
# This file is a part of the Retiled project.
# Neither Retiled nor Drew Naylor are associated with Microsoft
# and Microsoft does not endorse Retiled.
# Any other copyrights and trademarks belong to their
# respective people and companies/organizations.
#
#
#   Licensed under the Apache License, Version 2.0 (the "License");
#   you may not use this file except in compliance with the License.
#   You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
#   Unless required by applicable law or agreed to in writing, software
#   distributed under the License is distributed on an "AS IS" BASIS,
#   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
#   See the License for the specific language governing permissions and
#   limitations under the License.




import os
from pathlib import Path
import sys
# from libs.libRetiledStartPy import appslist as AppsList
# from libs.libRetiledStartPy import tileslist as TilesList
from libs.libRetiledActionCenter import actioncentercommands as ActionCenterCommands

# Settings file loader.
# TODO: Switch to a script that can just run the Python 
# file as a script so that the library doesn't have to
# be copied into each program and waste space and make
# updating more confusing.
from libs.libRetiledSettings import settingsReader as settingsReader

from PySide6.QtGui import QGuiApplication
from PySide6.QtQml import QQmlApplicationEngine
from PySide6.QtCore import QObject, Slot, Property, QStringListModel

# Trying to figure out buttons with this:
# https://stackoverflow.com/questions/57619227/connect-qml-signal-to-pyside2-slot
		
class ActionCenterActionButtonsViewModel(QObject):
	# Currently only offers functionality to run commands.
	# TODO: Load the list of buttons from a config file.
	@Slot(str)
	def runCommand(self, command):
		# Pass the command to the library so it runs.
		ActionCenterCommands.runCommand(command)

class SettingsLoader(QObject):
	# Slots still need to exist when using PySide.
	@Slot(str, str, str, result=str)
	def getSetting(self, SettingType, RequestedSetting, DefaultValue):
		# Get the settings.
		# TODO: Switch to a script that can just run the Python 
		# file as a script so that the library doesn't have to
		# be copied into each program and waste space and make
		# updating more confusing.
		# Set main file path for the config file to get it from the repo, or an install.
		# The two backslashes at the beginning are required on Windows, or it won't go up.
		# (I think I changed this at some point, as there are no backslashes anymore.)
		# Important: RetiledActionCenter is only one folder down.
		SettingsFilePath = "".join([os.getcwd(), "/../RetiledSettings/configs/", SettingType, ".config"])
		
		if not sys.platform.startswith("win32"):
			# If not on Windows, check if the config file is in the user's home directory,
			# and update the path accordingly.
			if os.path.exists("".join([os.path.expanduser("~"), "/.config/Retiled/RetiledSettings/configs/", SettingType, ".config"])):
				SettingsFilePath = "".join([os.path.expanduser("~"), "/.config/Retiled/RetiledSettings/configs/", SettingType, ".config"])
		
		#print(SettingsFilePath)
		
		# Return the requested setting.
		return settingsReader.getSetting(SettingsFilePath, RequestedSetting, DefaultValue)
	
	# We need to sometimes convert strings to bools for settings
	# loading in QML.
	# Please note: this only covers when the string
	# is "true"; "1", "on", and "yes" are not
	# yet covered.
	# I kinda got this idea from this SO post,
	# since just returning bool(StringToConvert)
	# didn't work:
	# https://stackoverflow.com/a/18472142
	@Slot(str, result=bool)
	def convertSettingToBool(self, StringToConvert):
		if StringToConvert.lower() == "true":
			return True
		else:
			return False

def shutdown():
	# This is the cleanup code as described in the link.
	engine.rootObjects()[0].deleteLater()

if __name__ == "__main__":
	# Set the Universal style.
	sys.argv += ['--style', 'Universal']
	app = QGuiApplication(sys.argv)
	# Clean up the engine stuff before closing:
	# https://bugreports.qt.io/browse/QTBUG-81247?focusedCommentId=512347&page=com.atlassian.jira.plugin.system.issuetabpanels%3Acomment-tabpanel#comment-512347
	# But we're actually taking the one from September 17, 2021, also from Benjamin Green.
	app.aboutToQuit.connect(shutdown)
	
	# # Define the AllAppsListItems class so I can use it.
	# allAppsListItems = AllAppsListItems()
	
	# Bind the theme settings loader to access it from QML.
	settingsLoader = SettingsLoader()
	
	# Hook up some stuff so I can access the ActionCenterActionButtonsViewModel from QML.
	actionCenterActionButtonsViewModel = ActionCenterActionButtonsViewModel()
	
	engine = QQmlApplicationEngine()
	# engine.rootContext().setContextProperty("allAppsListItems", allAppsListItems)
	# Theme settings loader binding.
	engine.rootContext().setContextProperty("settingsLoader", settingsLoader)
	# Action buttons for the Action Center.
	engine.rootContext().setContextProperty("actionCenterActionButtonsViewModel", actionCenterActionButtonsViewModel)
	engine.load("pages/ActionCenterWindow.qml")
	if not engine.rootObjects():
		sys.exit(-1)
	sys.exit(app.exec())
