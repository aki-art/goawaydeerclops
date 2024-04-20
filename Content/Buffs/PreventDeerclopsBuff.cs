using Terraria;
using Terraria.ModLoader;

namespace GoAwayDeerClops.Content.Buffs
{
	public class PreventDeerclopsBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}
