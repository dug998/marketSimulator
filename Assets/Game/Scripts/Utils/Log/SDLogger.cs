using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SDLogger : MonoSingleton<SDLogger>
{
    [SerializeField] private LogType logFilter;
    [SerializeField] 
    [HideInInspector]
    private bool enableLog;
    private const string _USE_LOG = "USE_LOG";

    [Button]
    public bool EnableLog
    {
        get => enableLog;
        set
        {
            enableLog = value;
#if UNITY_EDITOR
            DefineLogSymbols(value);
#endif
        }
    }

    private LogType LogFilter
    {
        get => logFilter;
        set => logFilter = value;
    }

    private void Start()
    {
        LogFilter = logFilter;
    }

    private bool IsCanShowLog(LogType type)
    {
        if (!enableLog) return false;
        if (type == LogType.Exception) return true;
        return type <= logFilter;
    }

    private void Log(LogType type, object message)
    {
        if (IsCanShowLog(type))
        {
            Debug.unityLogger.Log(type, message);
        }
    }


    [Conditional("USE_LOG")]
    public static void Log(object message)
    {
        Instance.Log(LogType.Log, message);
    }

    [Conditional("USE_LOG")]
    public static void LogWarning(object message)
    {
        Instance.Log(LogType.Warning, message);
    }

    [Conditional("USE_LOG")]
    public static void LogError(object message)
    {
        Instance.Log(LogType.Error, message);
    }

    [Conditional("USE_LOG")]
    public static void LogException(object message)
    {
        Instance.Log(LogType.Exception, message);
    }

#if UNITY_EDITOR
    private List<BuildTargetGroup> _supportArchitect = new List<BuildTargetGroup>
    {
        BuildTargetGroup.Standalone,
        BuildTargetGroup.Android,
        BuildTargetGroup.iOS
    };

    private void DefineLogSymbols(bool enable)
    {
        if (enable)
        {
            foreach (var targetGroup in _supportArchitect)
            {
                var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

                if (!symbols.Contains(_USE_LOG))
                {
                    symbols += ";" + _USE_LOG;
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbols);
                }
            }
        }
        else
        {
            foreach (var targetGroup in _supportArchitect)
            {
                var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
                if (symbols.Contains(_USE_LOG))
                {
                    symbols = symbols.Replace($";{_USE_LOG}", "");
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbols);
                }
            }
        }
    }
#endif
}