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

    [NonSerialized] public List<GameObject> tables;
    [NonSerialized] public List<GameObject> values;
    [NonSerialized] public List<GameObject> numbers;

    [NonSerialized] public int[] rootArray;

    #endregion

    #region Monobehaviour

    void Awake()
    {
        tables = new List<GameObject>();
    }

    #endregion

    #region Public Methods

    public void GetSliderValues()
    {
        sliderValue.text = arraySlider.value.ToString();
    }

    public void InstantiateAllSorts()
    {
        
        int arraySize = Convert.ToInt32(arraySlider.value);
        rootArray = new int[arraySize];
        FillArray(rootArray);
        CreateLists();
        AssignValues();
        
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

    #endregion

    #region Private Methods

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

    private void CreateLists()
    {
        values = new List<GameObject>();
        numbers = new List<GameObject>();
    }

    private void AssignValues()
    {
        for (int i = 0; i < rootArray.Length; i++)
        {
            
            values.Add(Instantiate(value, valuesParent, false));
            values[i].GetComponent<Slider>().value = rootArray[i];

            numbers.Add(Instantiate(number, numbersParent, false));
            numbers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rootArray[i].ToString();
        }

    }

    #endregion


}
