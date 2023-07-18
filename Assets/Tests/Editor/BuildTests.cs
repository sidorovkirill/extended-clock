using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Windows;

namespace Tests.Editor
{
	public class BuildTests
	{
		private const string OutputFolderPath = @"{0}/ScriptAssemblies/PlayerBuildInterface/{1}";
		private const string iosPlatform = "iOS";
		private const string windowsPlatform = "StandAloneWin";
		
		private static string[] _platformNames = {iosPlatform, windowsPlatform};
		
		private readonly Dictionary<string, ScriptCompilationSettings> _platformSettings = new()
		{
			{iosPlatform, new ScriptCompilationSettings
			{
				group = BuildTargetGroup.iOS,
				options = ScriptCompilationOptions.None,
				target = BuildTarget.iOS,
			}},
			{windowsPlatform, new ScriptCompilationSettings
			{
				group = BuildTargetGroup.Standalone, options = ScriptCompilationOptions.None, target = BuildTarget.StandaloneWindows,
			}}
		};

		[Test]
		public void Build([ValueSource(nameof(_platformNames))] string platformName)
		{
			var platformCompilationSettings = _platformSettings[platformName];
			var rootPath = Application.dataPath.Replace("/Assets", "");
			var folderPath = String.Format(OutputFolderPath, rootPath, platformCompilationSettings.target);

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			CompilationPipeline.assemblyCompilationFinished += CheckCompilationResults;
			PlayerBuildInterface.CompilePlayerScripts(platformCompilationSettings, folderPath);
			CompilationPipeline.assemblyCompilationFinished -= CheckCompilationResults;
		}

		private static void CheckCompilationResults(string assemblyName, CompilerMessage[] compilerMessages)
		{
			foreach (var message in compilerMessages)
			{
				switch (message.type)
				{
					case CompilerMessageType.Warning:
						Debug.LogWarning(message.message);
						break;
					case CompilerMessageType.Error:
						Debug.LogError(message.message);

						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}