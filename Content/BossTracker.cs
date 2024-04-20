using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GoAwayDeerClops.Content
{
	public class BossTracker : ModSystem
	{
		public static bool HasDownedDeerclops { get; private set; }

		private const string DOWNED_DEERCLOPS_KEY = "DownedDeerClops";

		public override void Load()
		{
			base.Load();
			On_NPC.DoDeathEvents += OnNpcDeathPostLoot;
		}

		private void OnNpcDeathPostLoot(On_NPC.orig_DoDeathEvents original, NPC npc, Player _)
		{
			if (npc.type == NPCID.Deerclops)
				HasDownedDeerclops = true;
		}

		public override void LoadWorldData(TagCompound tag) => HasDownedDeerclops = tag.GetBool(DOWNED_DEERCLOPS_KEY);

		public override void SaveWorldData(TagCompound tag) => tag[DOWNED_DEERCLOPS_KEY] = HasDownedDeerclops;

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = HasDownedDeerclops;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			HasDownedDeerclops = flags[0];
		}
	}
}
