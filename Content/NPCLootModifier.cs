using GoAwayDeerClops.Content.Conditions;
using GoAwayDeerClops.Content.Items.Placeable;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoAwayDeerClops.Content
{
	public class NPCLootModifier : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.Deerclops)
			{
				var effigy = ModContent.ItemType<Effigy>();

				var rule = ItemDropRule.ByCondition(new FirstDeerclopsKillCondition(), effigy);
				rule.OnFailedConditions(ItemDropRule.Common(effigy, 999999));

				npcLoot.Add(rule);
			}
		}
	}
}
