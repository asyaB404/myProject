using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WorldCanvas : MonoBehaviour
{
    private static WorldCanvas instance;
    public static WorldCanvas Instacne
    {
        get { return instance; }
    }

    [SerializeField]
    private GameObject messageUIprefab;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    public Text ShowMessage(Vector2 pos, string message)
    {
        GameObject uiobj = Instantiate(messageUIprefab, transform);
        uiobj.transform.position = pos;
        Text text = uiobj.GetComponent<Text>();
        text.text = message;
        float d = 1.25f;
        uiobj.transform.localScale = Vector3.zero;
        uiobj.transform.DOScale(0.010f, d / 4);
        uiobj
            .transform.DOLocalMoveY(uiobj.transform.position.y + 1.1f, d)
            .OnComplete(() => Destroy(uiobj));
        return text;
    }
}
