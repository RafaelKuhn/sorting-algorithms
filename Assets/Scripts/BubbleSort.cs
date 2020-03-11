using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BubbleSort : MonoBehaviour
{
    public VectorValues vectorValues;
    public void Sort()
    {
        StartCoroutine(DelayAndSort());
    }



    IEnumerator DelayAndSort()
    {


        int temp;
        for (int j = 0; j < vectorValues.arr.Length - 1; j++)
        {
            for (int i = 0; i < vectorValues.arr.Length - 1; i++)
            {
                
                if (vectorValues.arr[i] > vectorValues.arr[i + 1])
                {
                    yield return new WaitForSeconds(1f);
                    temp = vectorValues.arr[i + 1];
                    vectorValues.arr[i + 1] = vectorValues.arr[i];
                    vectorValues.arr[i] = temp;
                    vectorValues.numbers[i].GetComponent<TextMeshProUGUI>().text = vectorValues.arr[i].ToString();
                    vectorValues.numbers[i+1].GetComponent<TextMeshProUGUI>().text = vectorValues.arr[i+1].ToString();
                    vectorValues.values[i].GetComponent<Slider>().value = vectorValues.arr[i];
                    vectorValues.values[i+1].GetComponent<Slider>().value = vectorValues.arr[i+1];
                }

            }
        }



        yield return new WaitForSeconds(0.5f);
    }

}
