using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SliderPreview : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private TextMeshProUGUI previewText;

    void Awake()
    {
        previewText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        UpdateSliderPreview();
    }


    public void _OnSliderValueChanged()
    {
        UpdateSliderPreview();
    }


    private void UpdateSliderPreview()
    {
        previewText.text = slider.value.ToString();
    }

}
