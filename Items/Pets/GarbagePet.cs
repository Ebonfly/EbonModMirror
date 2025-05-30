﻿using System.Collections.Generic;

namespace EbonianMod.Items.Pets;

public class GarbagePet : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 0;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ProjectileType<GarbagePetP>();
        Item.width = 16;
        Item.height = 30;
        Item.UseSound = SoundID.Item2;
        Item.useAnimation = 20;
        Item.useTime = 20;
        Item.rare = ItemRarityID.Green;
        Item.master = true;
        Item.noMelee = true;
        Item.value = Item.sellPrice(0, 5, 50, 0);
        Item.buffType = BuffType<GarbagePetB>();
    }

    public override void UseStyle(Player player, Rectangle rec)
    {
        if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
        {
            player.AddBuff(Item.buffType, 3600, true);
        }
    }
}
public class GarbagePetP : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 12;
        Main.projPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(499);
        Projectile.width = 32;
        Projectile.height = 28;
        Projectile.hide = true;
        AIType = 499;
    }
    public int currentframe;
    public int frametimer;
    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
    {
        behindProjectiles.Add(index);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        Texture2D texture = Request<Texture2D>("EbonianMod/Items/Pets/GarbagePetP").Value;
        int frameHeight = Request<Texture2D>("EbonianMod/Items/Pets/GarbagePetP").Value.Height / 9;
        int startY = frameHeight * currentframe;
        Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
        Vector2 origin = sourceRectangle.Size() / 2f;
        origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

        Color drawColor = Projectile.GetAlpha(lightColor);
        Main.EntitySpriteDraw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

        return false;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.rotation = 0;
        if (Projectile.frame == 0 || Projectile.frame == 1)
        {
            currentframe = 0;
        }
        else if (Projectile.frame > 1 && Projectile.frame < 8)
        {
            frametimer++;
            if (frametimer >= 5)
            {
                currentframe++;
                frametimer = 0;
            }
            if (currentframe > 8)
            {
                currentframe = 3;
            }
        }
        else if (Projectile.ai[0] != 0)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + PiOver2;
            currentframe = 4;
        }
        if (player.HasBuff(BuffType<GarbagePetB>()))
        {
            Projectile.timeLeft = 2;
        }
    }
}
public class GarbagePetB : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;

        Main.vanityPet[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.ownedProjectileCounts[ProjectileType<GarbagePetP>()] < 1)
        {
            if (player.whoAmI == Main.myPlayer)
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ProjectileType<GarbagePetP>(), 0, 0, 0);
        }
        else
            player.buffTime[buffIndex] = 18000;
    }
}
