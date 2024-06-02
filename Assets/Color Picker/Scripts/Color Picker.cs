using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/**
* Main manager of the color picker, receive the picked color from the sub components, blend the color parts to obtain the final color 
* and trigger the submit event returning the final color.
*/
public class ColorPicker : MonoBehaviour
{
    [SerializeField] private HSBPicker HSBPicker;
    [SerializeField] private Image ActualColorImage;
    [SerializeField] private Image FinalColorImage;
    [SerializeField] private Button ConfirmButton;

    private Color CurrentColor = Color.white;

    // External exposed event
    public UnityEvent<Color> OnSubmit;

    // Start is called before the first frame update
    void Start()
    {
        HSBPicker.ColorChangeEvent = ColorChanged;   
        ConfirmButton.onClick.AddListener(SetFinalColor);
    }

    /**
    * Handle method for the color changed event triggered by the HSBPicker script, it generates a new color based on hue, saturation and brightness
    * result from the user interaction (pointer down) on the HSB Box
    */
    private void ColorChanged(HSBPicker sender, float hue, float saturation, float brightness)
    {
        // saturation increase on x, bright on y
        var color = Color.HSVToRGB(hue, saturation, brightness);

        SetActualColor(color);
    }

    /**
    * Store the picked color in var, and set it to graphic feedback
    */
    private void SetActualColor(Color color)
    {
        CurrentColor = color;
        ActualColorImage.color = CurrentColor;
    }

    /**
    * On user confirm via "Pick" button, trigger the event (On Submit) with the new color
    */
    private void SetFinalColor()
    {
        FinalColorImage.color = CurrentColor;
        OnSubmit?.Invoke(CurrentColor);
    }

    /**
    * Reset the Color Picker value to default and trigger HSBPicker script reset request
    */
    public void Reset() {
        CurrentColor = Color.white;
        ActualColorImage.color = Color.white;
        FinalColorImage.color = Color.white;
        HSBPicker.ResetRequested = true;
    }
}
