using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SortAlgorithms : MonoBehaviour
{
    private float executionTime;

    [SerializeField] private VectorValues vectorValues;
    [SerializeField] private GameObject reSort;
    [SerializeField] private GameObject statusText;

    public void Sort()
    {
        SetCoroutineDelayTime(vectorValues.rootArray.Length);

        StartCoroutine(BubbleSort());
        StartCoroutine(SelectionSort());
        StartCoroutine(ShellSort());
        //StartCoroutine(HeapSort());
        //StartCoroutine(MergeSort());
    }

    IEnumerator BubbleSort()
    {
        int trocas = 0;

        var sortThis = vectorValues.GetArrayInstance();
                
        Transform values = vectorValues.tables[0].transform.Find("Values").transform;
        Transform numbers = vectorValues.tables[0].transform.Find("Numbers").transform;

        int temp;
        for (int j = 0; j < sortThis.Length - 1; j++)
        {
            for (int i = 0; i < (sortThis.Length - j - 1); i++)
            {
                
                Highlight(values.GetChild(i), true);
                Highlight(values.GetChild(i+1), true);

                yield return new WaitForSeconds(executionTime); // after highlight

                if (sortThis[i] > sortThis[i + 1])
                {
                    

                    temp = sortThis[i + 1];
                    sortThis[i + 1] = sortThis[i];
                    sortThis[i] = temp;
                        
                    trocas = trocas + 1;
                    vectorValues.bubble.text = trocas.ToString();
                }
                UpdateTables(values.GetChild(i), numbers.GetChild(i), sortThis[i]);
                UpdateTables(values.GetChild(i + 1), numbers.GetChild(i+1), sortThis[i+1]);

                yield return new WaitForSeconds(executionTime / 2f); // after update

                Highlight(values.GetChild(i), false);
                Highlight(values.GetChild(i + 1), false);
            }
        }

        EndSort(vectorValues.bubble);

    }

    IEnumerator SelectionSort()
    {
        int trocas = 0;

        var sortThis = vectorValues.GetArrayInstance();

        Transform values = vectorValues.tables[1].transform.Find("Values").transform;
        Transform numbers = vectorValues.tables[1].transform.Find("Numbers").transform;

        int temp, smallest = 0;
        int n = sortThis.Length;

        int aux1, aux2 = 0;
        
        for (int i = 0; i < n - 1; i++)
        {
            smallest = i;
            for (int j = i + 1; j < n; j++)
            {
                aux1 = smallest;
                aux2 = j;

                Highlight(values.GetChild(aux1), true);
                Highlight(values.GetChild(aux2), true);

                yield return new WaitForSeconds(executionTime); // after highlight

                if (sortThis[j] < sortThis[smallest])
                {
                    smallest = j;

                    trocas = trocas + 1;
                    vectorValues.selection.text = trocas.ToString();
                }

                

                Highlight(values.GetChild(aux1), false);
                Highlight(values.GetChild(aux2), false);

            }

            temp = sortThis[smallest];
            sortThis[smallest] = sortThis[i];
            sortThis[i] = temp;

            UpdateTables(values.GetChild(i), numbers.GetChild(i), sortThis[i]);
            UpdateTables(values.GetChild(smallest), numbers.GetChild(smallest), sortThis[smallest]);

            yield return new WaitForSeconds(executionTime/2); // after update


        }

        EndSort(vectorValues.selection);
    }

    IEnumerator ShellSort()
    {
        int trocas = 0;

        var sortThis = vectorValues.GetArrayInstance();

        Transform values = vectorValues.tables[2].transform.Find("Values").transform;
        Transform numbers = vectorValues.tables[2].transform.Find("Numbers").transform;


        int n = sortThis.Length;
        int gap = n / 2;
        int temp;

        int aux = 0; //

        while (gap > 0)
        {

            for (int i = 0; i + gap < n; i++)
            {
                 

                int j = i + gap;
                temp = sortThis[j];
                

                while (j - gap >= 0 && temp < sortThis[j - gap])
                {

                    Highlight(values.GetChild(j), true);
                    Highlight(values.GetChild(j - gap), true);

                    yield return new WaitForSeconds(executionTime); // after highlight


                    sortThis[j] = sortThis[j - gap];


                    aux = j; //
                    

                    UpdateTables(values.GetChild(aux), numbers.GetChild(aux), sortThis[aux]);
                    UpdateTables(values.GetChild(j), numbers.GetChild(j), sortThis[j]);

                    j = j - gap;

                    Highlight(values.GetChild(aux), false);

                    trocas = trocas + 1;
                    vectorValues.shell.text = trocas.ToString();
                }

                sortThis[j] = temp;


                UpdateTables(values.GetChild(j), numbers.GetChild(j), sortThis[j]);


                yield return new WaitForSeconds(executionTime / 2f); // after update

                Highlight(values.GetChild(aux), false);
                Highlight(values.GetChild(j), false);
            }
            gap = gap / 2;
        }

        EndSort(vectorValues.shell);

    }

    


    #region Private Methods

    private void EndSort(TextMeshProUGUI sort)
    {
        vectorValues.ranking.Add(sort);
        vectorValues.UpdateColors(vectorValues.ranking.Count - 1);

        if (vectorValues.ranking.Count == 6)
            ResetSortButton();
    }

    private void ResetSortButton()
    {
        statusText.gameObject.SetActive(false);
        reSort.gameObject.SetActive(true);

    }
    private void UpdateTables(Transform value, Transform number, int i)
    {
        value.GetComponent<Slider>().value = i;
        number.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
    }

    private void Highlight(Transform value, bool onOff)
    {
        value.Find("Highlight").gameObject.SetActive(onOff);
    }

    private void SetCoroutineDelayTime(int arraySize)
    {
        executionTime = ((14.85f - 0.45f * arraySize) / 27f);
    }

    #endregion











}
