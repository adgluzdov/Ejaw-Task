using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GeometryObjectModel : MonoBehaviour
{
    public string ObjectType;
    private int clickCount = 0;
    private Color color;

    public Color Color
    {
        get => color;

        set
        {
            color = value;
            GetComponent<MeshRenderer>().material.color = Color;
        }
    }

    public int ClickCount
    {
        get => clickCount;

        set
        {
            clickCount = value;
            Color = GeometryObjectDataManager.Instance.GetColor(ObjectType, ClickCount);
        }
    }

    private void OnEnable()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(GameDataManager.Instance.GameData.ObservableTime)) 
        .Repeat()
        .Subscribe(_ => {
            OnTimer();
        }).AddTo(new CompositeDisposable()); 
    }

    private bool Down = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!Down) {
                Down = true;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    OnClick();
                }
            }
        }
        else {
            Down = false;
        }
    }

    private void OnTimer() {
        Color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
    }

    private void OnClick() {
        ClickCount++;
    }
}
