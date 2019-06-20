
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBucketlist : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform tContent;
    private RectTransform content;
    string path;
    string jsonString;
    private workShopsList workShopList = new workShopsList();

    // Use this for initialization
    void Start()
    {
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        int desplazador = 0;

        //cargar archivo speakers.json
        TextAsset asset = Resources.Load("bucketList") as TextAsset;
        workShopList = JsonUtility.FromJson<workShopsList>(asset.text);

        Debug.Log(workShopList);

        foreach (workshop s in workShopList.workshops)
        {
            //creamos boton
            GameObject but = Instantiate(buttonPrefab) as GameObject;


            desplazador += 100;

            //añadirlo al content y cambiar tamaño
            but.transform.SetParent(tContent.transform);
            but.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            but.SetActive(true);

            but.GetComponent<Button>().onClick.AddListener(() => OnButtonClick());

        }

        //cambiar tamaño del content
        content = tContent.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, desplazador);
    }

    private void OnButtonClick()
    {
        //abrir detalles workshop o solo inscribirse
    }

}





