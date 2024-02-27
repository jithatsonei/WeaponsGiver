using System;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Utils;

namespace KnifeGiver;
[MinimumApiVersion(175)]

public class KnifeGiver : BasePlugin
{
    public override string ModuleName => "Knife Giver";
    public override string ModuleAuthor => "ji";
    public override string ModuleDescription => "Ensures players in custom gamemodes spawn with a knife.";
    public override string ModuleVersion => "build1";
    public FakeConVar<string> WeaponCvar = new("css_starting_weapon", "What weapon should a player spawn with", "weapon_knife");

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerSpawn>(Event_PlayerSpawn, HookMode.Post);
    }

    private HookResult Event_PlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        var player = @event.Userid;
        
        if(!player.IsValid || !player.PlayerPawn.IsValid) return HookResult.Continue;       
        player.GiveNamedItem(WeaponCvar.Value); 
        return HookResult.Continue;
    }
}
