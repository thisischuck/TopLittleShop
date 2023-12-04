using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class AssetUtils
{
    public static void CreateAsset<T>(string path, Object asset)
    {
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(asset, path);
#endif
    }
}
