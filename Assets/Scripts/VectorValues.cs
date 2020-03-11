using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VectorValues : MonoBehaviour
{
    #region Variables

    private int amount = 5;

    public GameObject value;
    public List<GameObject> values;
    public Transform valuesParent;
    private Vector2 valueAnchor;

    public GameObject number;
    public List<GameObject> numbers;
    public Transform numbersParent;
    private Vector2 numberAnchor;

    public GameObject marker;

    public GameObject button;

    public int[] arr = { 12, 17, 29, 4, 7 };

    #endregion

    #region Monobehaviour

    void Awake()
    {
        CreateLists();
        GetAnchors();
    }

    void Start()
    {
        StartCoroutine(DelayAndAssign());
    }
    #endregion

    #region Private Methods

    private void CreateLists()
    {
        values = new List<GameObject>();
        numbers = new List<GameObject>();
        values.Add(value);
        numbers.Add(number);
    }

    private void GetAnchors()
    {
        valueAnchor = value.GetComponent<RectTransform>().anchoredPosition;
        numberAnchor = number.GetComponent<RectTransform>().anchoredPosition;
    }

    #endregion

    #region Coroutines and Protected Methods

    private IEnumerator DelayAndAssign()
    {

        value.GetComponent<Slider>().value = arr[0];
        number.GetComponent<TextMeshProUGUI>().text = arr[0].ToString();

        for (int i = 1; i < amount; i++)
        {
            yield return new WaitForSeconds(0f);

            values.Add(Instantiate(value, valuesParent, false));
            valueAnchor.x = (valueAnchor.x + 50);
            values[i].GetComponent<RectTransform>().anchoredPosition = valueAnchor;
            values[i].GetComponent<Slider>().value = arr[i];

            numbers.Add(Instantiate(number, numbersParent, false));
            numberAnchor.x = (numberAnchor.x + 50);
            numbers[i].GetComponent<RectTransform>().anchoredPosition = numberAnchor;
            numbers[i].GetComponent<TextMeshProUGUI>().text = arr[i].ToString();
        }
    }

    #endregion

}
