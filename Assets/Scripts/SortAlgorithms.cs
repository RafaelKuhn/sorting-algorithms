using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SortAlgorithms : MonoBehaviour
{
   
    void Awake()
    {

    }

    [SerializeField] private VectorValues vectorValues;
    public void Sort()
    {
        StartCoroutine(BubbleSort());
        //StartCoroutine(BubbleSort());
        //StartCoroutine(BubbleSort());
        //StartCoroutine(BubbleSort());
        //StartCoroutine(BubbleSort());
    }


    IEnumerator BubbleSort()
    {
        int[] unsorted = vectorValues.rootArray;
        Transform values = vectorValues.tables[0].transform.GetChild(0).transform;
        Transform numbers = vectorValues.tables[0].transform.GetChild(1).transform;
        //Debug.Log(vectorValues.tables[0].transform.GetChild(1).transform.GetChild(0).name);

        foreach (Transform value in values)
        {
            value.GetComponent<Slider>().value = 15;
        }

        foreach (Transform number in numbers)
        {
            Debug.Log(number.name);
            number.GetChild(0).GetComponent<TextMeshProUGUI>().text = 7.ToString();
            //number.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 7.ToString();
        }




        //int temp;
        //for (int j = 0; j < rootArray.Length - 1; j++)
        //{
        //    for (int i = 0; i < vectorValues.rootArray.Length - 1; i++)
        //    {

        //        if (vectorValues.rootArray[i] > vectorValues.rootArray[i + 1])
        //        {
        //            yield return new WaitForSeconds(1f);
        //            temp = vectorValues.rootArray[i + 1];
        //            vectorValues.rootArray[i + 1] = vectorValues.rootArray[i];
        //            vectorValues.rootArray[i] = temp;

        //        }

        //    }
        //}

        yield return new WaitForSeconds(0.5f);
    }












}
