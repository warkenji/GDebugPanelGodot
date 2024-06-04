using System;
using GDebugPanelGodot.Extensions;
using GDebugPanelGodot.Nodes;
using Godot;

namespace GDebugPanelGodot.DebugActions.Widgets;

public partial class StringDebugActionWidget : DebugActionWidget
{
    [Export] public LabelAutowrapSelectionByControlWidthController? LabelAutowrapController;
    [Export] public Label? Label;
    [Export] public LineEdit? LineEdit;
    
    Action<string>? _setAction;
    Func<string>? _getAction;
    
    public void Init(Control sizeControl, string name, Action<string> setAction, Func<string> getAction)
    {
        LabelAutowrapController!.SizeControl = sizeControl;
        _setAction = setAction;
        _getAction = getAction;
        
        Label!.Text = name;
        LineEdit!.Text = getAction.Invoke();
        LineEdit!.EmitSignal(LineEdit.SignalName.TextChanged);
        LineEdit!.ConnectLineEditTextChanged(Changed);
    }
    
    public override bool Focus()
    {
        LineEdit!.GrabFocus();
        return true;
    }

    void Changed(string text)
    {
        int caretColumn = LineEdit!.CaretColumn;
        _setAction!.Invoke(text);
        LineEdit!.Text = _getAction!.Invoke();
        LineEdit!.CaretColumn = caretColumn;
    }
}