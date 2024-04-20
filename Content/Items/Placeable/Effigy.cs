using Terraria;
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
			Item.value = 10_000;
		}
	}
}
