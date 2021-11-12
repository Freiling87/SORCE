using BepInEx.Logging;
using System;
using System.Diagnostics;

namespace SORCE.Logging
{
	class SORCELogger
	{
		private static string GetLoggerName(Type containingClass)
		{
			return $"SORCE_{containingClass.Name}";
		}

		public static ManualLogSource GetLogger()
		{
			Type containingClass = new StackFrame(1, false).GetMethod().ReflectedType;
			return Logger.CreateLogSource(GetLoggerName(containingClass));
		}
	}
}