using System;
using GDebugPanelGodot.DebugActions.Widgets;
using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.DebugActions.Actions;

public sealed class ColorDebugAction : IDebugAction
{
    public string Name { get; }
    public Action<Color> SetAction { get; }
    public Func<Color> GetAction { get; }
    
    public ColorDebugAction(string name, Action<Color> setAction, Func<Color> getAction)
    {
        Name = name;
        SetAction = setAction;
        GetAction = getAction;
    }
    
    public DebugActionWidget InstantiateWidget(DebugPanelView debugPanelView)
    {
        ColorDebugActionWidget widget = debugPanelView.ColorDebugActionWidget!.Instantiate<ColorDebugActionWidget>();
        widget.Init(debugPanelView.ContentControl!, Name, SetAction, GetAction);
        return widget;
    }
}