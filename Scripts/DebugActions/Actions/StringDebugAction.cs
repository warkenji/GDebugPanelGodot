using System;
using GDebugPanelGodot.DebugActions.Widgets;
using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.DebugActions.Actions;

public sealed class StringDebugAction : IDebugAction
{
    public string Name { get; }
    public Action<string> SetAction { get; }
    public Func<string> GetAction { get; }
    
    public StringDebugAction(string name, Action<string> setAction, Func<string> getAction)
    {
        Name = name;
        SetAction = setAction;
        GetAction = getAction;
    }
    
    public DebugActionWidget InstantiateWidget(DebugPanelView debugPanelView)
    {
        StringDebugActionWidget widget = debugPanelView.StringDebugActionWidget!.Instantiate<StringDebugActionWidget>();
        widget.Init(debugPanelView.ContentControl!, Name, SetAction, GetAction);
        return widget;
    }
}