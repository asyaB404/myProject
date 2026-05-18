using UnityEngine;

public static class Utils
{
    static Camera _mainCameraCache;

    public static Camera CachedMainCamera =>
        _mainCameraCache != null ? _mainCameraCache : (_mainCameraCache = Camera.main);

    public static Vector3 MouseWorldPos
    {
        get { return CachedMainCamera.ScreenToWorldPoint(Input.mousePosition); }
    }
}
