using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SortAlgorithms : MonoBehaviour
{
    private float executionTime;

    [SerializeField] private VectorValues vectorValues;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject statusText;

    private int returnedQuickSortPivot;

    public void Sort()
    {
        SetCoroutineDelayTime(vectorValues.rootArray.Length);

        StartCoroutine(BubbleSort());
        StartCoroutine(SelectionSort());
        StartCoroutine(ShellSort());
        StartCoroutine(QuickSort());
        StartCoroutine(HeapSort());
        StartCoroutine(MergeSort());
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
                
                

                if (sortThis[i] > sortThis[i + 1])
                {

                    Highlight(values.GetChild(i), true);
                    Highlight(values.GetChild(i + 1), true);

                    yield return new WaitForSeconds(executionTime / 2f); // after highlight





                    temp = sortThis[i + 1];
                    sortThis[i + 1] = sortThis[i];
                    sortThis[i] = temp;

                    trocas++;
                    vectorValues.bubble.text = trocas.ToString();




                    UpdateTables(values.GetChild(i), numbers.GetChild(i), sortThis[i]);
                    UpdateTables(values.GetChild(i + 1), numbers.GetChild(i + 1), sortThis[i + 1]);

                    yield return new WaitForSeconds(executionTime / 4f); // after update

                    Highlight(values.GetChild(i), false);
                    Highlight(values.GetChild(i + 1), false);







                }

                
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

                yield return new WaitForSeconds(executionTime / 2f); // after highlight

                if (sortThis[j] < sortThis[smallest])
                {
                    smallest = j;


                }

                

                Highlight(values.GetChild(aux1), false);
                Highlight(values.GetChild(aux2), false);

            }

            temp = sortThis[smallest];
            sortThis[smallest] = sortThis[i];
            sortThis[i] = temp;

            trocas++;
            vectorValues.selection.text = trocas.ToString();



            UpdateTables(values.GetChild(i), numbers.GetChild(i), sortThis[i]);
            UpdateTables(values.GetChild(smallest), numbers.GetChild(smallest), sortThis[smallest]);

            yield return new WaitForSeconds(executionTime / 4f); // after update


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

                    trocas++;
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

    IEnumerator QuickSort()
    {
        int trocas = 0;

        var sortThis = vectorValues.GetArrayInstance();

        Transform values = vectorValues.tables[3].transform.Find("Values").transform;
        Transform numbers = vectorValues.tables[3].transform.Find("Numbers").transform;

        yield return StartCoroutine(DoQuickSort(sortThis, 0, sortThis.Length - 1));


        IEnumerator DoQuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                yield return StartCoroutine(Partition(array, low, high));

                int partitionIndex = returnedQuickSortPivot;

                //3. Recursively continue sorting the array
                yield return StartCoroutine(DoQuickSort(array, low, partitionIndex - 1));
                yield return StartCoroutine(DoQuickSort(array, partitionIndex + 1, high));

            }

            
        }

        IEnumerator Partition(int[] array, int low, int high)
        {
            returnedQuickSortPivot = 0;
            //1. Select a pivot point.
            int pivot = array[high];

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    lowIndex++;

                    if (lowIndex != j)
                    {
                        Highlight(values.GetChild(lowIndex), true);
                        Highlight(values.GetChild(j), true);
                        yield return new WaitForSeconds(executionTime); // after highlight
                    }

                    int temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;

                    if (lowIndex != j)
                    {
                        UpdateTables(values.GetChild(j), numbers.GetChild(j), sortThis[j]);
                        UpdateTables(values.GetChild(lowIndex), numbers.GetChild(lowIndex), sortThis[lowIndex]);

                        yield return new WaitForSeconds(executionTime / 2f); // after update

                        Highlight(values.GetChild(lowIndex), false);
                        Highlight(values.GetChild(j), false);
                    }


                    trocas++;
                    vectorValues.quick.text = trocas.ToString();
                }
            }

            Highlight(values.GetChild(lowIndex + 1), true);
            Highlight(values.GetChild(high), true);

            yield return new WaitForSeconds(executionTime); // after highlight



            int temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            trocas++;
            vectorValues.quick.text = trocas.ToString();

            UpdateTables(values.GetChild(lowIndex+1), numbers.GetChild(lowIndex + 1), sortThis[lowIndex + 1]);
            UpdateTables(values.GetChild(high), numbers.GetChild(high), sortThis[high]);

            yield return new WaitForSeconds(executionTime / 2f); // after update

            Highlight(values.GetChild(lowIndex + 1), false);
            Highlight(values.GetChild(high), false);



            returnedQuickSortPivot = lowIndex + 1;
        }

        returnedQuickSortPivot = 0;

        EndSort(vectorValues.quick);
    }

    IEnumerator HeapSort()
    {
        int trocas = 0;

        var sortThis = vectorValues.GetArrayInstance();

        Transform values = vectorValues.tables[4].transform.Find("Values").transform;
        Transform numbers = vectorValues.tables[4].transform.Find("Numbers").transform;


        for (int i = sortThis.Length / 2 - 1; i >= 0; i--)
            yield return StartCoroutine(Heapify(sortThis, sortThis.Length, i));
        for (int i = sortThis.Length - 1; i >= 0; i--)
        {
            Highlight(values.GetChild(0), true);
            Highlight(values.GetChild(i), true);

            yield return new WaitForSeconds(executionTime); // after highlight

            int temp = sortThis[0];
            sortThis[0] = sortThis[i];
            sortThis[i] = temp;

            trocas++;
            vectorValues.heap.text = trocas.ToString();

            UpdateTables(values.GetChild(0), numbers.GetChild(0), sortThis[0]);
            UpdateTables(values.GetChild(i), numbers.GetChild(i), sortThis[i]);

            yield return new WaitForSeconds(executionTime / 2f); // after update

            Highlight(values.GetChild(0), false);
            Highlight(values.GetChild(i), false);


            yield return StartCoroutine(Heapify(sortThis, i, 0));

        }

        
        IEnumerator Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && arr[left] > arr[largest])
                largest = left;
            if (right < n && arr[right] > arr[largest])
                largest = right;
            if (largest != i)
            {

                Highlight(values.GetChild(i), true);
                Highlight(values.GetChild(largest), true);

                yield return new WaitForSeconds(executionTime); // after highlight

                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                trocas = trocas + 1;
                vectorValues.heap.text = trocas.ToString();

                UpdateTables(values.GetChild(i), numbers.GetChild(i), sortThis[i]);
                UpdateTables(values.GetChild(largest), numbers.GetChild(largest), sortThis[largest]);

                yield return new WaitForSeconds(executionTime / 2f); // after update

                Highlight(values.GetChild(i), false);
                Highlight(values.GetChild(largest), false);



                yield return StartCoroutine(Heapify(arr, n, largest));
            }

         
        }

        yield return new WaitForEndOfFrame();


        EndSort(vectorValues.heap);
    }

    IEnumerator MergeSort()
    {
        int trocas = 0;

        Transform values = vectorValues.tables[5].transform.Find("Values").transform;
        Transform numbers = vectorValues.tables[5].transform.Find("Numbers").transform;

        var sortThis = vectorValues.GetArrayInstance();       


        yield return mergeSort(sortThis, 0, sortThis.Length-1);

        // l is for left index and r is right index of the sub-array of arr to be sorted 
        IEnumerator mergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {

                // Same as (l + r) / 2, but avoids overflow 
                // for large l and r 
                int m = l + (r - l) / 2;

                // Sort first and second halves 
                yield return mergeSort(arr, l, m);
                yield return mergeSort(arr, m + 1, r);

                yield return StartCoroutine(merge(arr, l, m, r));
            }

            yield return null;
        }


        // Merges two subarrays of arr[].  First subarray is arr[l..m]  Second subarray is arr[m+1..r] 
        IEnumerator merge(int[] arr, int start, int mid, int end)
        {
            int start2 = mid + 1;

            // If the direct merge is already sorted 
            if (arr[mid] <= arr[start2])
            {
                yield break;
            }

            // Two pointers to maintain start 
            // of both arrays to merge 
            while (start <= mid && start2 <= end)
            {

                // If element 1 is in right place 
                if (arr[start] <= arr[start2])
                {
                    start++;
                }
                else
                {
                    int value = arr[start2];
                    int index = start2;

                    // Shift all the elements between element 1 
                    // element 2, right by 1. 
                    while (index != start)
                    {
                        Highlight(values.GetChild(index), true);
                        Highlight(values.GetChild(index-1), true);
                        arr[index] = arr[index - 1];

                        
                        yield return new WaitForSeconds(executionTime/2);
                        UpdateTables(values.GetChild(index), numbers.GetChild(index), arr[index]);
                        Highlight(values.GetChild(index), false);
                        Highlight(values.GetChild(index - 1), false);

                        trocas++;
                        vectorValues.merge.text = trocas.ToString();

                        index--;
                    }
                    arr[start] = value;

                    Highlight(values.GetChild(start), true);
                    UpdateTables(values.GetChild(start), numbers.GetChild(start), arr[start]);
                    yield return new WaitForSeconds(executionTime/2);
                    Highlight(values.GetChild(start), false);

                    trocas++;
                    vectorValues.merge.text = trocas.ToString();

                    // Update all the pointers 
                    start++;
                    mid++;
                    start2++;
                }
            }
        }

        EndSort(vectorValues.merge);
    }


    #region Private Methods

    private void EndSort(TextMeshProUGUI sort)
    {
        vectorValues.ranking.Add(sort);
        vectorValues.UpdateColors(sort);

        if (vectorValues.ranking.Count == 6)
            ResetSortButton();
    }

    private void ResetSortButton()
    {
        statusText.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(true);

    }
    private void UpdateTables(Transform value, Transform number, int i)
    {
        value.GetComponent<Slider>().value = i;
        number.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
    }

    private void Highlight(Transform value, bool onOff)
    {
        if (value != null)
            value.Find("Highlight").gameObject.SetActive(onOff);
    }

    private void SetCoroutineDelayTime(int arraySize)
    {
        executionTime = ((8.85f - 0.25f * arraySize) / 27f);
    }

    #endregion



}
