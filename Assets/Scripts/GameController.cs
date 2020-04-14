using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ObjectsData objectsData;
    private Vector3 position;
    private ObjectData currentObject;
    private GameObject currentGameObject;

    private bool cliked = false;
    void Update() {
        if (Input.GetMouseButtonUp(0) && !cliked) {
            cliked = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            OnClick(ray.GetPoint(10f));
        }
    }

    private void OnClick(Vector3 position)
    {
        this.position = position;
        LoadingAssetBundleController.Instance.bundleURL = objectsData.AssetBundlePath;
        LoadingAssetBundleController.Instance.StartLoading(OnLoadAssetBundle, onError);
    }

    private void OnLoadAssetBundle(AssetBundle assetBundle) 
    {
        currentObject = GetRandomObject();
        LoadingAssetFromAssetBundleController.Instance.assetName = currentObject.AssetName;
        LoadingAssetFromAssetBundleController.Instance.StartLoading(assetBundle, onLoadAsset, onError);
    }

    private void onLoadAsset(GameObject gameObject)
    {
        currentGameObject = Instantiate(gameObject);
        currentGameObject.name = NamesDataManager.Instance.GetName(currentObject.ObjectType);
        currentGameObject.transform.position = position;
        currentGameObject.AddComponent<GeometryObjectModel>().ObjectType = currentObject.ObjectType;
    }

    private void onError(string error)
    {
        Debug.LogError(error);
    }

    public ObjectData GetRandomObject() {
        return objectsData.objects[UnityEngine.Random.Range(0, objectsData.objects.Length)];
    }

}

[Serializable]
public struct ObjectsData
{
    public string AssetBundlePath;
    public ObjectData[] objects;
}

[Serializable]
public struct ObjectData {
    public string ObjectType;
    public string AssetName;
}

