# Base Color Picker
The "Base Color Picker" component that allow the selection of a color based on a hue slider (slider, right) and an HSB picker to tune the values of brightness and saturation (box, left).  
The actual selected color and the final picker color are shown below the color picker.  
The color picker throw onSubmit events that must be subscriben to listen the color change and allow the user to reset the selection via reset method.  
The Color picker will fit the parent container in which the prefab is put, suggested size for the prent container is 375x450 px.  

## Unity Package download
The unity package of the color picker is available for download from the releases section of GitHub as attachment of the release.  

## Basic Usage
1. Import the package into your project.  
2. Import the UI element prefab "Color Picker" (NOTE: use the "Color Picker" prefab not the script) available from the package into your Canvas.  
3. In your script (where the color picked will be used) set the reference to the Color Picker.  
```
[SerializeField] private ColorPicker ColorPicker;
```
4. Bind the Color Picker to your script, by dragging the Color Picker prefab (2.) into the public/serialized (3.) slot of your script.  
5. In your script, create the method that will handle the Color Picker selection in your script, the only requirement is that your method will accept a `UnityEngine.Color` parameter.
```
private void YourResultHandlingMethod(Color color) {  
	// ... your code here
}
```
6. In your script, during the setup phase (Start), subscribe to the OnSubmit event provided from the Color Picker, and execute your method (5.) when is triggered.
```
	ColorPicker.OnSubmit.AddListener(YourResultHandlingMethod);
```
## Functionalities
- Pick a color selecting the hue via slider and adjusting the values of brightness and saturation via HSB picker handle.
- Reset the position (hue slider to bottom, handle on HSB centered, HSB color restored to the default hue) of the Color Picker invoking the `Reset()` method available for the Color Picker.
```
ColorPicker.Reset();
```
	
