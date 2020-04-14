using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAssetFromAssetBundleController : Singleton<LoadingAssetFromAssetBundleController>
{
    public string assetName;
    private bool loadingStatus = false;
    private Coroutine loadingCoroutine;

    public void StartLoading(AssetBundle assetBundle, Action<GameObject> onSuccess, Action<string> onErrorLoading) {
        if (!loadingStatus)
        {
            loadingCoroutine = StartCoroutine(LoadingRoutine(assetBundle, onSuccess, onErrorLoading));
        }
        else
        {
            Debug.LogWarning("Loading asset has already start!");
        }
    }

    public void StopLoading()
    {
        if (loadingStatus)
        {
            StopCoroutine(loadingCoroutine);
        }
        else
        {
            Debug.LogWarning("Loading asset has not already start!");
        }
    }

    public bool IsLoading()
    {
        return loadingStatus;
    }

    private IEnumerator LoadingRoutine(AssetBundle assetBundle, Action<GameObject> onSuccess, Action<string> onErrorLoading) {
        var request = assetBundle.LoadAssetAsync<GameObject>(assetName);
        yield return request;
        if ((GameObject)request.asset == null) 
        {
            onErrorLoading.Invoke("Asset не найден");
        }
        else 
        {
            onSuccess.Invoke((GameObject)request.asset);
        }

    }
}
