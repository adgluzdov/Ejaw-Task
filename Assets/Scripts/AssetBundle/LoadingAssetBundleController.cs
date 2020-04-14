using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class LoadingAssetBundleController : Singleton<LoadingAssetBundleController>
{
    public string bundleURL;
    private bool loadingStatus = false;
    private Coroutine loadingCoroutine;

    public void StartLoading(Action<AssetBundle> onSuccess, Action<string> onErrorLoading) {
        if (!loadingStatus)
        {
            loadingCoroutine = StartCoroutine(LoadingRoutine(onSuccess, onErrorLoading));
        }
        else
        { 
            Debug.LogWarning("Loading assetBundle has already start!");
        }
    }

    public void StopLoading() {
        if (loadingStatus) 
        {
            StopCoroutine(loadingCoroutine);
        }
        else 
        {
            Debug.LogWarning("Loading assetBundle has not already start!");
        }
    }

    public bool IsLoading() {
        return loadingStatus;
    }

    private IEnumerator LoadingRoutine(Action<AssetBundle> onSuccess, Action<string> onErrorLoading) {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return request.SendWebRequest();
        if (!request.isHttpError && !request.isNetworkError)
        {
            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);
            onSuccess.Invoke(assetBundle);
        }
        else 
        {
            Debug.LogErrorFormat("Loading assetBundle error [{0}, {1}]", bundleURL, request.error);
            onErrorLoading.Invoke(request.error);
        }
        request.Dispose();
        yield return null;
    }
}
