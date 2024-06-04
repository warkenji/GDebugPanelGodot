using System;
using GDebugPanelGodot.Extensions;
using GDebugPanelGodot.Nodes;
using Godot;

namespace GDebugPanelGodot.DebugActions.Widgets;

public partial class ColorDebugActionWidget : DebugActionWidget
{
    [Export] public LabelAutowrapSelectionByControlWidthController? LabelAutowrapController;
    [Export] public Label? Label;
    [Export] public ColorPickerButton? ColorPickerButton;
    
    Action<Color>? _setAction;
    Func<Color>? _getAction;
    
    public void Init(Control sizeControl, string name, Action<Color> setAction, Func<Color> getAction)
    {
        LabelAutowrapController!.SizeControl = sizeControl;
        _setAction = setAction;
        _getAction = getAction;
        
        Label!.Text = name;
        ColorPickerButton!.Color = getAction.Invoke();
        ColorPickerButton!.EmitSignal(ColorPickerButton.SignalName.ColorChanged);
        ColorPickerButton!.ConnectColorPickerButtonColorChanged(Changed);
    }
    
    public override bool Focus()
    {
        ColorPickerButton!.GrabFocus();
        return true;
    }

    void Changed(Color color)
    {
        _setAction!.Invoke(color);
        ColorPickerButton!.Color = _getAction!.Invoke();
    }
}