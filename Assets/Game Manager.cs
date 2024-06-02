using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ColorPicker ColorPicker;
    [SerializeField] private Text ResultHex;
    [SerializeField] private Image ResultImg;
    void Start()
    {
        ColorPicker.OnSubmit.AddListener(ColorPicked);
    }

    private void ColorPicked(Color color) {
        ResultHex.text = color.ToHexString();
        ResultImg.color = color;
    }
}
