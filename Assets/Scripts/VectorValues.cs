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
    
    [Header("Instance spaces")]
    public Transform rightInstanceSpace;
    public Transform leftInstanceSpace;

    [Header("Slider")]
    public Slider arraySlider;

    [Header("Trocas")]
    public TextMeshProUGUI bubble;
    public TextMeshProUGUI selection;
    public TextMeshProUGUI shell;
    public TextMeshProUGUI quick;
    public TextMeshProUGUI heap;
    public TextMeshProUGUI merge;

    [HideInInspector] public List<GameObject> tables;
    [HideInInspector] public List<GameObject> values;
    [HideInInspector] public List<GameObject> numbers;
    [HideInInspector] public List<TextMeshProUGUI> ranking;

    [HideInInspector] public int[] rootArray;

    [HideInInspector] private byte arrayType = 0;



    #endregion

    #region Monobehaviour

    void Awake()
    {
        tables = new List<GameObject>();
        ranking = new List<TextMeshProUGUI>();
    }

    #endregion

    #region Public Methods

    public void SetArrayType(int index)
    {
        arrayType = (byte)index;
    }

    public void InstantiateAll()
    {
        CleanColorRanking();

        CreateNewRootArray();

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

    public void UpdateColors(TextMeshProUGUI swapText)
    {
        swapText.color = Color.green;
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

    private void CreateNewRootArray()
    {
        rootArray = new int[Convert.ToInt32(arraySlider.value)];
    }
    private void FillArray(int[] arr)
    {
        switch (arrayType)
        {
            case 0: // all random
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = UnityEngine.Random.Range(1, 99);
                }
                break;

            case 1: // nearly sorted
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = UnityEngine.Random.Range(1, 99);
                }
                PseudoShellSort(arr);
                break;

            case 2: // inversely sorted
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = UnityEngine.Random.Range(1, 99);
                }
                QuickSort(arr);
                InvertArray(arr);
                break;

            case 3: // few values
                GenerateFewValues(arr);
                break;
        }

        

    }

    private void InstantiateTables()
    {
        // dumb code lul
        for (int i = 0; i<3; i++)
        {
            GameObject obj = Instantiate( sortTable, leftInstanceSpace, false );
            tables.Add( obj );
            obj.name = $"left {i}";
            tables[i].SetActive( true );
        }
        for (int i = 3; i < 6; i++)
        {
            GameObject obj = Instantiate( sortTable, rightInstanceSpace, false );
            tables.Add( obj );
            obj.name = $"right {i}";
            tables[i].SetActive( true );
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
            values.Add(Instantiate(value, sortTable.transform.GetChild(0))); // instantiate "value" prefab in "values" array
            values[i].GetComponent<Slider>().value = rootArray[i];

            numbers.Add(Instantiate(number, sortTable.transform.GetChild(1))); // instantiate "number" prefab in "numbers" array
            numbers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rootArray[i].ToString();
        }

    }

    private void CleanColorRanking()
    {
        ResetSwapUI();

        ranking.Clear();
    }

    private void ResetSwapUI()
    {
        var whiteColor = Color.white;

        bubble.text = "";
        bubble.color = whiteColor;

        selection.text = "";
        selection.color = whiteColor;

        shell.text = "";
        shell.color = whiteColor;

        quick.text = "";
        quick.color = whiteColor;

        heap.text = "";
        heap.color = whiteColor;

        merge.text = "";
        merge.color = whiteColor;

    }

    #endregion

    #region Presorting

    private void InvertArray(int[] arr)
    {
        int swap;

        int x = arr.Length-1;

        for (int i = 0; i<(arr.Length/2); i++)
        {
            swap = arr[i];
            arr[i] = arr[x];
            arr[x] = swap;
            x--;
        }
    }

    private int[] QuickSort(int[] vetor)
    {
        int inicio = 0;
        int fim = vetor.Length - 1;

        QuickSort(vetor, inicio, fim);

        return vetor;
    }

    private void QuickSort(int[] vetor, int inicio, int fim)
    {
        if (inicio < fim)
        {
            int p = vetor[inicio];
            int i = inicio + 1;
            int f = fim;

            while (i <= f)
            {
                if (vetor[i] <= p)
                {
                    i++;
                }
                else if (p < vetor[f])
                {
                    f--;
                }
                else
                {
                    int troca = vetor[i];
                    vetor[i] = vetor[f];
                    vetor[f] = troca;
                    i++;
                    f--;
                }
            }

            vetor[inicio] = vetor[f];
            vetor[f] = p;

            QuickSort(vetor, inicio, f - 1);
            QuickSort(vetor, f + 1, fim);
        }
    }

    private void PseudoShellSort(int[] arr)
    {
        int n = arr.Length;
        int gap = n / 2;
        int temp;

        int aux = 0; //

        while (gap > 1)
        {
            for (int i = 0; i + gap < n; i++)
            {
                int j = i + gap;
                temp = arr[j];

                while (j - gap >= 0 && temp < arr[j - gap])
                {
                    arr[j] = arr[j - gap];

                    aux = j;

                    j = j - gap;
                }

                arr[j] = temp;

            }
            gap = gap / 2;
        }
    }

    private void GenerateFewValues(int[] arr)
    {
        var values = new List<int>();

        values.Add(UnityEngine.Random.Range(1, 100));

        if (arr.Length> 5)
            values.Add(UnityEngine.Random.Range(1, 100));
        if (arr.Length > 10)
            values.Add(UnityEngine.Random.Range(1, 100));
            values.Add(UnityEngine.Random.Range(1, 100));
        if (arr.Length > 15)
            values.Add(UnityEngine.Random.Range(1, 100));
        if (arr.Length > 20)
            values.Add(UnityEngine.Random.Range(1, 100));
        if (arr.Length > 25)
            values.Add(UnityEngine.Random.Range(1, 100));

        for (int i = 0; i<values.Count; i++)
        {
            arr[i] = values[i];
        }


        values.Clear();




    }


    #endregion


}
