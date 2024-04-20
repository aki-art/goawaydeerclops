using GoAwayDeerClops.Content.Buffs;
using GoAwayDeerClops.Content.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoAwayDeerClops.Content.TileEntities
{
	public class EffigyTileEntity : ModTileEntity
	{
		public Vector2 GetCenter() => Position.ToWorldCoordinates(Effigy.WIDTH * 8f, Effigy.HEIGHT * 8f);

		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && tile.TileType == ModContent.TileType<Effigy>() && tile.TileFrameX == 0 && tile.TileFrameY == 0;
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				// Sync the entire multitile's area.  Modify "width" and "height" to the size of your multitile in tiles
				int width = Effigy.WIDTH;
				int height = Effigy.HEIGHT;
				NetMessage.SendTileSquare(Main.myPlayer, i, j, width, height);

				// Sync the placement of the tile entity with other clients
				// The "type" parameter refers to the tile type which placed the tile entity, so "Type" (the type of the tile entity) needs to be used here instead
				NetMessage.SendData(MessageID.TileEntityPlacement, number: i, number2: j, number3: Type);

				return -1;
			}

			// ModTileEntity.Place() handles checking if the entity can be placed, then places it for you
			int placedEntity = Place(i, j);

			return placedEntity;
		}


		public override void Update()
		{
			base.Update();
			var center = GetCenter();

			foreach (var player in Main.player)
			{
				if (player.DistanceSQ(center) < Config.RangeSquared)
					player.AddBuff(ModContent.BuffType<PreventDeerclopsBuff>(), 60);
			}
		}
	}
}
