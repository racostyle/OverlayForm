# OverlayForm

`OverlayForm` is a simple Windows Form that displays a semi-transparent overlay over another form, providing a visual indication that an operation is in progress. The form includes a progress panel that moves across the screen to indicate activity.

## Features

- **Overlay Effect:** Covers the specified form with a black, semi-transparent overlay.
- **Progress Indicator:** A panel that moves across the screen, indicating ongoing work.
- **Thread-Safe UI Updates:** Utilizes asynchronous tasks to update the UI without freezing the main thread.

## Usage

### Initialization

To create and display the `OverlayForm`:

```csharp
// Assuming `mainForm` is the form you want to cover with the overlay
var overlay = new OverlayForm(mainForm);
overlay.Show();
```

### Closing the Overlay

The overlay will automatically stop its progress when the form is closed:

```csharp
overlay.Close();
```

### Example

Here's a quick example of how to use the `OverlayForm`:

```csharp
public void ShowOverlay(Form mainForm)
{
    var backOpacity = .5f;
    var overlay = new OverlayForm(mainForm, backOpacity);
    overlay.Show();
    // Set text
    overlay.SetText("Operating...");
    // Simulate some work
    Task.Delay(5000).ContinueWith(_ => overlay.Close());
}
```
