using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoAwayDeerClops.Content.Items.Placeable
{
	public class Effigy : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Effigy>());
			Item.width = 42;
			Item.height = 60;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 1);
		}
	}
}
