using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace GoAwayDeerClops.Content.Conditions
{
	public class FirstDeerclopsKillCondition : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		private static LocalizedText description;

		public FirstDeerclopsKillCondition() => description ??= Language.GetOrRegister("Mods.GoAwayDeerClops.DropConditions.FirstKill");

		public bool CanDrop(DropAttemptInfo info) => !BossTracker.HasDownedDeerclops;

		public bool CanShowItemDropInUI() => true;

		public string GetConditionDescription() => description.Value;
	}
}
