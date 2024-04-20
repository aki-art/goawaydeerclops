using GoAwayDeerClops.Content.TileEntities;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoAwayDeerClops.Content.Tiles
{
	public class Effigy : ModTile
	{
		public const int
			WIDTH = 4,
			HEIGHT = 5;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;

			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.Width = WIDTH;
			TileObjectData.newTile.Height = HEIGHT;
			TileObjectData.newTile.Origin = new Point16(2, 3);
			TileObjectData.newTile.AnchorBottom = new AnchorData(
				AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide,
				TileObjectData.newTile.Width,
				0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = Enumerable.Repeat(16, HEIGHT).ToArray();
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = true;

			var tileEntity = ModContent.GetInstance<EffigyTileEntity>();
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(tileEntity.Hook_AfterPlacement, -1, 0, true);
			TileObjectData.newTile.UsesCustomCanPlace = true;

			TileObjectData.addTile(Type);

			DustType = DustID.Bone;

			AddMapEntry(new Color(114, 94, 74), CreateMapEntryName());
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			base.KillMultiTile(i, j, frameX, frameY);
			ModContent.GetInstance<EffigyTileEntity>().Kill(i, j);
		}

		/*		public override bool RightClick(int i, int j)
				{
					Main.StartRain();

					Main.dayTime = true;
					var time = (4) * 3600 + 29 * 60;  //just before midnight

					Main.SkipToTime(time, false);

					return true;
				}*/
	}
}
