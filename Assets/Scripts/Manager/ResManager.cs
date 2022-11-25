using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResManager : Singleton<ResManager>
{
    private AssetBundle mainAB = null;
    private AssetBundleManifest manifest = null;
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    private string PathUrl { get { return Application.streamingAssetsPath + "/"; } }
    private string MainABName { get { return "Android"; } }

    public void LoadAB(string abName)
    {
        // 加载主包
        if(mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        // 获取依赖
        AssetBundle ab;
        string[] strs = manifest.GetAllDependencies(abName);
        for(int i = 0; i < strs.Length; i++)
        {
            if(!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        // 加载目标包
        if(!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    public T LoadRes<T>(string abName, string resName) where T : Object
    {
        #if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>("Assets/GameRes/Prefabs/UI/" + resName + ".prefab");
        #else
        LoadAB(abName);
        // 加载资源
        return abDic[abName].LoadAsset<T>(resName);
        #endif
    }

    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callback) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callback));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callback) where T : Object
    {
        LoadAB(abName);
        // 加载资源
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        callback(abr.asset as T);
    }

    public void UnLoad(string abName)
    {
        if(abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
