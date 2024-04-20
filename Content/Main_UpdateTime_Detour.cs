using FullSerializer.Internal;
using GoAwayDeerClops.Content.Buffs;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoAwayDeerClops.Content
{
	public class Main_UpdateTime_Detour : ModSystem
	{
		public override void Load()
		{
			base.Load();
			IL_Main.UpdateTime += UpdateTimePatch;
		}

		private void UpdateTimePatch(ILContext context)
		{
			try
			{
				var c = new ILCursor(context);

				var m_SpawnOnPlayer = typeof(NPC)
					.GetMethod(nameof(NPC.SpawnOnPlayer), new[]
					{
						typeof(int),
						typeof(int)
					});

				var m_ModifyDeerClopsCondition = typeof(Main_UpdateTime_Detour).GetDeclaredMethod(nameof(ModifyDeerClopsCondition));
				var f_active = typeof(Entity).GetField(nameof(Entity.active));

				if (c.TryGotoNext(i => i.MatchCall(m_SpawnOnPlayer) && i.Previous.MatchLdcI4(NPCID.Deerclops)))
				{
					if (c.TryGotoPrev(i => i.MatchLdfld(f_active)))
					{
						// dup player
						c.EmitDup();

						// move one line down
						c.Index++;

						// modify the player.active condition to return false, preventing spawn
						c.EmitCall(m_ModifyDeerClopsCondition);
					}
				}
			}
			catch (Exception ex)
			{
				MonoModHooks.DumpIL(ModContent.GetInstance<GoAwayDeerclops>(), context);
				throw new ILPatchFailureException(ModContent.GetInstance<GoAwayDeerclops>(), context, ex);
			}
		}

		// return false to skip spawning
		private static bool ModifyDeerClopsCondition(Player player, bool playerActive)
		{
			if (!playerActive || player == null)
				return false;

			var shouldBlockSpawn = player.HasBuff<PreventDeerclopsBuff>();

			if (shouldBlockSpawn)
				ModContent.GetInstance<GoAwayDeerclops>().Logger.Info($"Deerclops is attempting to spawn but was prevented by Deer Effigy.");

			return !shouldBlockSpawn;
		}
	}
}
