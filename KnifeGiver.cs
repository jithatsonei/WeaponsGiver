using System;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Utils;

namespace KnifeGiver;
[MinimumApiVersion(175)]

public class KnifeGiver : BasePlugin
{
    public override string ModuleName => "WeaponsGiver";
    public override string ModuleAuthor => "ji";
    public override string ModuleDescription => "Ensures players in custom gamemodes spawn with starting weapons.";
    public override string ModuleVersion => "build3";

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerSpawn>(Event_PlayerSpawn, HookMode.Post);
        RegisterEventHandler<EventRoundPrestart>(Event_RoundPrestart, HookMode.Pre);
    }

    public void GetVars()
    {
        var tPrimary = ConVar.Find("mp_t_default_primary").StringValue;
        var tSecondary = ConVar.Find("mp_t_default_secondary").StringValue;
        var tMelee = ConVar.Find("mp_t_default_melee").StringValue;

        var ctPrimary = ConVar.Find("mp_ct_default_primary").StringValue;
        var ctSecondary = ConVar.Find("mp_ct_default_secondary").StringValue;
        var ctMelee = ConVar.Find("mp_ct_default_melee").StringValue;
    }

    private HookResult Event_RoundPrestart(EventRoundPrestart @event, GameEventInfo info)
    {
        GetVars();
        return HookResult.Continue;
    }

    private HookResult Event_PlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        var player = @event.Userid;
        
        if(!player.IsValid || !player.PlayerPawn.IsValid) return HookResult.Continue;       

        switch((CsTeam)player.TeamNum)
        {
            case CsTeam.Terrorist:
                if(!String.IsNullOrEmpty(tPrimary)) player.GiveNamedItem(tPrimary);
                if(!String.IsNullOrEmpty(tSecondary)) player.GiveNamedItem(tSecondary);
                if(!String.IsNullOrEmpty(tMelee)) player.GiveNamedItem(tMelee);
                break;
            
            case CsTeam.CounterTerrorist:
                if(!String.IsNullOrEmpty(ctPrimary)) player.GiveNamedItem(ctPrimary);
                if(!String.IsNullOrEmpty(ctSecondary)) player.GiveNamedItem(ctSecondary);
                if(!String.IsNullOrEmpty(ctMelee)) player.GiveNamedItem(ctMelee);
                break;
        }

        return HookResult.Continue;
    }
}
