using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour
{
    // Hue Value change event
    public delegate void OnValueChanged(ColorSlider sender, float value);
    public OnValueChanged ValueChangedEvent;

    /**
        The value of the color slider define the hue of the final color
    */
    public float Value
    {
        get { return Slider.value; }
        set { Slider.SetValueWithoutNotify(value); }
    }

    private Slider Slider;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    private void Start()
    {
        Slider.onValueChanged.AddListener(ValueChange);
    }

    /**
    * Emit the color (hue) change event (triggered by the slider onValueChange event) used by the HSBPicker script
    */ 
    private void ValueChange(float value) {
        ValueChangedEvent.Invoke(this, value);
    }

    /**
    * Reset the color (hue) slider to default value (value zero, position bottom, color red)
    */
    public void Reset() {
        Value = 0;
    }
}
