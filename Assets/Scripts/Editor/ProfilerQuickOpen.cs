#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// 编辑器快捷入口：打开 Profiler，便于在项目后期波次录制 CPU / GC 基线。
/// </summary>
public static class ProfilerQuickOpen
{
    [MenuItem("Tools/Profiling/打开 Profiler")]
    public static void OpenProfiler()
    {
        EditorApplication.ExecuteMenuItem("Window/Analysis/Profiler");
    }

    [MenuItem("Tools/Profiling/打开 Frame Debugger")]
    public static void OpenFrameDebugger()
    {
        EditorApplication.ExecuteMenuItem("Window/Analysis/Frame Debugger");
    }
}
#endif
