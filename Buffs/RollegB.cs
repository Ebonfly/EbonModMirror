﻿using EbonianMod.Projectiles.Dev;

namespace EbonianMod.Buffs;

public class RollegB : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<EbonianPlayer>().rolleg = true;
        if (player.ownedProjectileCounts[ProjectileType<Rolleg>()] > 0)
            player.buffTime[buffIndex] = 2;
    }
}
