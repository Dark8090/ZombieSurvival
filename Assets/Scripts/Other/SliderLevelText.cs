using UnityEngine;
using UnityEngine.UI;

public class SliderLevelText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text textLevel;
    [SerializeField] private Text textCurrentExperience;

    // private int experienceCount;
    // private int maxExperience;

    private void Start()
    {
        slider = GetComponent<Slider>();
        // textLevel = GetComponentInChildren<Text>();
        // textCurrentExperience = GetComponentInChildren<Text>();
    }

    // public void AddExperience(int count)
    // {
    //     experienceCount += count;
    //     TextUpdate();
    // }
    // private void TextUpdate()
    // {
    //     text.text = experienceCount.ToString();
    //     slider.value = experienceCount;
    // }
    public void SetSlider(int currentExperience, int maxExperience, int level)
    {
        slider.value = currentExperience;
        slider.maxValue = maxExperience;
        SliderUpdate(currentExperience, maxExperience, level);
    }
    private void SliderUpdate(int currentExperience, int maxExperience, int level)
    {
        textLevel.text = "Уровень: " + level.ToString();
        textCurrentExperience.text = currentExperience.ToString() + "/" + maxExperience.ToString();
    }

}
