using System;
using UnityEngine;
using UnityEngine.UI;

public class HSBPicker : MonoBehaviour
{
    [SerializeField] private ColorSlider ColorSlider;
    [SerializeField] private Image ImageColor;

    private HSBHandleTracker HandleTracker;
    
    public Boolean ResetRequested = false;

    // Color change event
    public delegate void OnColorChange(HSBPicker sender, float hue, float saturation, float brightness);
    public OnColorChange ColorChangeEvent;

    // Selected Color elements
    public float Hue { get { return ColorSlider.Value; } }
    public float Saturation { get { return HandleTracker.NormalizedValue.x; } }
    public float Brightness { get { return HandleTracker.NormalizedValue.y; } }

    private void Awake()
    {
        HandleTracker = GetComponent<HSBHandleTracker>();
    }

    void Start()
    {
        // Set initial hue color
        UpdateImageColor(ColorSlider.Value);
        // Listen hue changes
        ColorSlider.ValueChangedEvent = OnSliderValueChanged;
        // Listen handle changes (saturation and brightness)
        HandleTracker.HandleMovedEvent = OnHandleMoved;
    }

    void Update()
    {
        if(ResetRequested) {
            Reset();
            ResetRequested = false; 
        }
    }

    /**
    * Trigger the background color change on hue color slider change event
    */
    private void OnSliderValueChanged(ColorSlider sender, float value)
    {
        UpdateImageColor(value);
    }

    /**
    * Change the background color for the HSB Picker box
    * Note: Data passed from the event are not stored, they are retrieved directly by the parent script getters method during the final color generation
    */
    private void UpdateImageColor(float hue)
    {
        ImageColor.color = Color.HSVToRGB(hue, 1, 1);
        OnColorChanged();
    }

    /**
    * On handle movement trigger the color (saturation and brightness) change event
    * Note: Data passed from the event are not stored, they are retrieved directly by the parent script getters method during the final color generation
    */
    private void OnHandleMoved(HSBHandleTracker sender, Vector2 position)
    {
        OnColorChanged();
    }

    /**
    * Throw the event of color change (used by the ColorPicker script to generate the final color)
    */
    private void OnColorChanged() {
        // Color components data are retrieved directly from the child scripts getters, not stored from the events
        ColorChangeEvent?.Invoke(this, Hue, Saturation,Brightness);
    }

    /**
    * Trigger the hue (ColorSlider) and Handle (HSBHandleTracker) reset, revert the HSBPicker box background color to default (red)
    */
    public void Reset() {
        ColorSlider.Reset();
        UpdateImageColor(ColorSlider.Value);
        HandleTracker.Reset();
    }
}
