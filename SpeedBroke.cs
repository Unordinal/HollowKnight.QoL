﻿using Modding;
using JetBrains.Annotations;

namespace QoL
{
    [UsedImplicitly]
    public class SpeedBroke : Mod, ITogglableMod
    {
        public override void Initialize()
        {
            On.HeroController.CanOpenInventory += MenuDrop;
            On.HeroController.CanQuickMap += CanQuickMap;
        }

        public void Unload()
        {
            On.HeroController.CanOpenInventory -= MenuDrop;
            On.HeroController.CanQuickMap -= CanQuickMap;
        }

        private static bool CanQuickMap(On.HeroController.orig_CanQuickMap orig, HeroController self)
        {
            return !GameManager.instance.isPaused && !self.cState.onConveyor && !self.cState.dashing &&
                   !self.cState.backDashing && (!self.cState.attacking || self.GetAttr<float?>("attack_time") >= self.ATTACK_RECOVERY_TIME) &&
                   !self.cState.recoiling && !self.cState.hazardDeath &&
                   !self.cState.hazardRespawning;

        }

        private static bool MenuDrop(On.HeroController.orig_CanOpenInventory orig, HeroController self)
        {
            return !GameManager.instance.isPaused && !self.controlReqlinquished && !self.cState.recoiling &&
                   !self.cState.transitioning && !self.cState.hazardDeath && !self.cState.hazardRespawning &&
                   !self.playerData.disablePause && self.CanInput() || self.playerData.atBench;
        }
    }
}
