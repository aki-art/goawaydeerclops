using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace GoAwayDeerClops
{
	public class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[DefaultValue(200)]
		[Range(1, 9999)]
		public int range;

		public static float RangeSquared { get; private set; }

		public override void OnChanged() => RangeSquared = (float)Math.Pow(range * 16f, 2);
	}
}
