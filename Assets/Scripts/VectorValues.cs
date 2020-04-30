using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VectorValues : MonoBehaviour
{
    #region Variables

    [Header ("Table Prefabs")]
    public GameObject value;
    public GameObject number;

    [Header("Sort Table")]
    public GameObject sortTable;
    
    [Header("Table Parent Anchors")]
    public Transform valuesParent;
    public Transform numbersParent;

    [Header("Slider")]
    public Slider arraySlider;
    public TextMeshProUGUI sliderValue;

    [Header("Trocas")]
    public TextMeshProUGUI bubble;
    public TextMeshProUGUI selection;
    public TextMeshProUGUI shell;
    public TextMeshProUGUI quick;
    public TextMeshProUGUI heap;
    public TextMeshProUGUI merge;

    [NonSerialized] public List<GameObject> tables;
    [NonSerialized] public List<GameObject> values;
    [NonSerialized] public List<GameObject> numbers;
    [NonSerialized] public List<TextMeshProUGUI> ranking;

    [NonSerialized] public int[] rootArray;
        


    #endregion

    #region Monobehaviour

    void Awake()
    {
        tables = new List<GameObject>();
        ranking = new List<TextMeshProUGUI>();
    }

    #endregion

    #region Public Methods

    public void GetSliderValues()
    {
        sliderValue.text = arraySlider.value.ToString();
    }
    
    public void InstantiateAll()
    {
        CleanColorRanking();

        InstanceRootArray();

        FillArray(rootArray);

        InstantiateLists();

        InstantiateValues();

        InstantiateTables();

    }

    public void DestroyLists()
    {
        foreach (GameObject obj in tables)
        {
            GameObject.Destroy(obj);
        }

        tables.Clear();

        foreach (GameObject obj in values)
        {
            GameObject.Destroy(obj);
        }

        values.Clear();

        foreach (GameObject obj in numbers)
        {
            GameObject.Destroy(obj);
        }

        numbers.Clear();
    }

    public int[] GetArrayInstance()
    {
        int[] genericArray = new int[rootArray.Length];
        for (int i = 0; i < rootArray.Length;  i++)
            genericArray[i] = rootArray[i];

        return genericArray;
    }

    public void UpdateColors(int index)
    {
        ranking[index].color = Color.green;
    }

    public void ReassignTables()
    {
        for (int table = 0; table < tables.Count; table++)
        {
            Transform values = tables[table].transform.Find("Values").transform;
            Transform numbers = tables[table].transform.Find("Numbers").transform;
            for (int i = 0; i < rootArray.Length; i++)
            {

                values.GetChild(i).GetComponent<Slider>().value = rootArray[i];
                numbers.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = rootArray[i].ToString();
            }
        }

        CleanColorRanking();
    }

    #endregion

    #region Private Methods


    private void InstanceRootArray()
    {
        rootArray = new int[Convert.ToInt32(arraySlider.value)];
    }
    private void FillArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = UnityEngine.Random.Range(1, 99);
        }

    }

    private void InstantiateTables()
    {
        for (int i = 0; i<6; i++)
        {
            tables.Add(Instantiate(sortTable, sortTable.transform.parent, false));
            tables[i].SetActive(true);
        }
    }

    private void InstantiateLists()
    {
        values = new List<GameObject>();
        numbers = new List<GameObject>();
    }

    private void InstantiateValues()
    {
        for (int i = 0; i < rootArray.Length; i++)
        {
            
            values.Add(Instantiate(value, valuesParent, false));
            values[i].GetComponent<Slider>().value = rootArray[i];

            numbers.Add(Instantiate(number, numbersParent, false));
            numbers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rootArray[i].ToString();
        }

    }

    private void CleanColorRanking()
    {
        CleanSwapValues();

        ranking.Clear();
    }

    private void CleanSwapValues()
    {
        bubble.text = "";
        bubble.color = Color.white;

        selection.text = "";
        selection.color = Color.white;

        shell.text = "";
        shell.color = Color.white;

        quick.text = "";
        quick.color = Color.white;

        heap.text = "";
        heap.color = Color.white;

        merge.text = "";
        merge.color = Color.white;

    }



    #endregion


}
