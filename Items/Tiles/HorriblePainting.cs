﻿using EbonianMod.Tiles.Paintings;

namespace EbonianMod.Items.Tiles;

public class HorriblePainting : ModItem
{
    public override bool IsLoadingEnabled(Mod mod) => false;
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = 999;
        Item.rare = 0;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.rare = ItemRarityID.Purple;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = TileType<ThePaintingOfAllTime>();
    }
}
