using System;
using Godot;

namespace GDebugPanelGodot.Extensions;

public static class ColorPickerButtonExtensions
{
    public static void ConnectColorPickerButtonColorChanged(this ColorPickerButton colorPickerButton, Action<Color> action)
    {
        colorPickerButton.Connect(ColorPickerButton.SignalName.ColorChanged, Callable.From(action));
    }
}