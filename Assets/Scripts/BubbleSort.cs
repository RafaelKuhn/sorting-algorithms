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

    void Update()
    {
        /*foreach (int i in vectorValues.rootArray)
        {
            vectorValues.numbers[i].GetComponent<TextMeshProUGUI>().text = vectorValues.rootArray[i].ToString();
            vectorValues.values[i].GetComponent<Slider>().value = vectorValues.rootArray[i];
        }*/
        
    }




    IEnumerator DelayAndSort()
    {
        int temp;
        for (int j = 0; j < vectorValues.rootArray.Length - 1; j++)
        {
            for (int i = 0; i < vectorValues.rootArray.Length - 1; i++)
            {
                
                if (vectorValues.rootArray[i] > vectorValues.rootArray[i + 1])
                {
                    yield return new WaitForSeconds(1f);
                    temp = vectorValues.rootArray[i + 1];
                    vectorValues.rootArray[i + 1] = vectorValues.rootArray[i];
                    vectorValues.rootArray[i] = temp;

                }

            }
        }



        yield return new WaitForSeconds(0.5f);
    }



    
}
