﻿namespace EbonianMod.Items.Armor.Vanity;

[AutoloadEquip(EquipType.Head)]
public class ClementinesCap : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 24;
        Item.value = Item.sellPrice(gold: 1);
        Item.rare = ItemRarityID.Blue;
        Item.vanity = true;
    }

}
