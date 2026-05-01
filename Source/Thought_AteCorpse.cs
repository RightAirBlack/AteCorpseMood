using RimWorld;
using System.Linq;
using Verse;

namespace AteCorpseMood
{
    /// <summary>
    /// A memory thought triggered when a pawn eats the corpse of a relative.
    /// Similar to Thought_FoodEaten, this stores metadata about the consumed corpse
    /// and displays it in the thought description.
    /// </summary>
    public class Thought_AteCorpse : Thought_Memory
    {
        private string? corpsePawnLabel;

        /// <summary>
        /// The label of the pawn whose corpse was eaten, cached for display.
        /// </summary>
        public string CorpsePawnLabel => corpsePawnLabel ?? "[unknown]";

        /// <summary>
        /// Appends the name of the consumed pawn to the base description.
        /// </summary>
        public override string Description
        {
            get
            {
                string baseDesc = base.Description;
                if (!corpsePawnLabel.NullOrEmpty())
                {
                    baseDesc += "\n\n" + ("AteCorpseMood.AteCorpse_PawnName".Translate() + ": " + corpsePawnLabel);
                }
                return baseDesc;
            }
        }



        /// <summary>
        /// Alternate method to set the eaten pawn's label directly.
        /// </summary>
        public void SetEatenPawn(Pawn eatenPawn)
        {
            if (eatenPawn != null)
            {

                corpsePawnLabel = eatenPawn.LabelCap;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref corpsePawnLabel, "corpsePawnLabel", "[unknown]");
        }

        /// <summary>
        /// Creates a Thought_AteCorpse based on the relationship between the eater and the eaten pawn.
        /// Returns null if there is no family/romantic relationship.
        /// </summary>
        public static Thought_AteCorpse? MakeThought(Pawn ingester, Pawn eatenPawn)
        {
            if (ingester?.relations == null || eatenPawn == null)
                return null;

            ThoughtDef? def = GetThoughtDefForRelation(ingester, eatenPawn);
            if (def == null)
                return null;
            L.M($"thoughtDef:{def.defName}");
            Thought_AteCorpse thought = (Thought_AteCorpse)ThoughtMaker.MakeThought(def);
            thought.SetEatenPawn(eatenPawn);

            return thought;
        }


        public static ThoughtDef? GetThoughtDefForRelation(Pawn ingester, Pawn eatenPawn)
        {
            var relations = ingester.GetRelations(eatenPawn);
            if (relations == null) return null;
            var opinon = ingester.relations.OpinionOf(eatenPawn);
            var traits = ingester.story?.traits;
            var bloodLustLike = traits != null && (traits.HasTrait(TraitDefOf.Psychopath)
                    || traits.HasTrait(TraitDefOf.Bloodlust)
                    || traits.HasTrait(AteDefOf.Cannibal));

            if (relations.Count() > 0)
            {


                // Parent — father/mother
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Parent && relations.Contains(PawnRelationDefOf.Parent))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyMotherCorpse : AteDefOf.Rt_AteMyFatherCorpse;
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Parent && relations.Contains(PawnRelationDefOf.ParentBirth))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyMotherCorpse : AteDefOf.Rt_AteMyFatherCorpse;
                // Child — son/daughter
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Child && relations.Contains(PawnRelationDefOf.Child))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyDaughterCorpse : AteDefOf.Rt_AteMySonCorpse;
                // Spouse — husband/wife
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Spouse && relations.Contains(PawnRelationDefOf.Spouse))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyWifeCorpse : AteDefOf.Rt_AteMyHusbandCorpse;

                // Fiance
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Fiance && relations.Contains(PawnRelationDefOf.Fiance))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyFianceeCorpse : AteDefOf.Rt_AteMyFianceCorpse;

                // Lover
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Lover && relations.Contains(PawnRelationDefOf.Lover))
                    return AteDefOf.Rt_AteMyLoverCorpse;

                // Sibling — brother/sister
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Sibling && relations.Contains(PawnRelationDefOf.Sibling))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMySisterCorpse : AteDefOf.Rt_AteMyBrotherCorpse;

                // Grandchild
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Grandchild && relations.Contains(PawnRelationDefOf.Grandchild))
                    return AteDefOf.Rt_AteMyGrandchildCorpse;

                // Nephew or Niece
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_NieceNephew && relations.Contains(PawnRelationDefOf.NephewOrNiece))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyNieceCorpse : AteDefOf.Rt_AteMyNephewCorpse;

                // Half-sibling
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_HalfSibling && relations.Contains(PawnRelationDefOf.HalfSibling))
                    return AteDefOf.Rt_AteMyHalfSiblingCorpse;

                // Uncle or Aunt
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_AuntUncle && relations.Contains(PawnRelationDefOf.UncleOrAunt))
                    return eatenPawn.gender == Gender.Female
                        ? AteDefOf.Rt_AteMyAuntCorpse : AteDefOf.Rt_AteMyUncleCorpse;

                // Grandparent
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Grandparent && relations.Contains(PawnRelationDefOf.Grandparent))
                    return AteDefOf.Rt_AteMyGrandparentCorpse;

                // Cousin
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Cousin && relations.Contains(PawnRelationDefOf.Cousin))
                    return AteDefOf.Rt_AteMyCousinCorpse;



                // Kin — distant relative (fallback)
                if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Kin && relations.Contains(PawnRelationDefOf.Kin))
                    return AteDefOf.Rt_AteMyKinCorpse;

                if (bloodLustLike)
                {
                    // ExLover
                    if (ModSetting_AteCorpseMood.EnableEatCorpseMood_VillainExLover && relations.Contains(PawnRelationDefOf.ExLover))
                        return AteDefOf.Rt_AteMyExLoverCorpse2;
                    // ExSpouse — exHusband/exWife
                    if (ModSetting_AteCorpseMood.EnableEatCorpseMood_VillainExSpouse && relations.Contains(PawnRelationDefOf.ExSpouse))
                        return eatenPawn.gender == Gender.Female
                            ? AteDefOf.Rt_AteMyExWifeCorpse2 : AteDefOf.Rt_AteMyExHusbandCorpse2;
                }
                else
                {
                    // ExLover
                    if (ModSetting_AteCorpseMood.EnableEatCorpseMood_ExLover && relations.Contains(PawnRelationDefOf.ExLover))
                        return AteDefOf.Rt_AteMyExLoverCorpse;
                    // ExSpouse — exHusband/exWife
                    if (ModSetting_AteCorpseMood.EnableEatCorpseMood_ExSpouse && relations.Contains(PawnRelationDefOf.ExSpouse))
                        return eatenPawn.gender == Gender.Female
                            ? AteDefOf.Rt_AteMyExWifeCorpse : AteDefOf.Rt_AteMyExHusbandCorpse;
                }

            }

            if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Friend && opinon >= 20)
                return AteDefOf.Rt_AteMyFriendCorpse;
            else if (ModSetting_AteCorpseMood.EnableEatCorpseMood_Rival && opinon <= -20 && !bloodLustLike)
                return AteDefOf.Rt_AteMyRivalCorpse;
            else if (ModSetting_AteCorpseMood.EnableEatCorpseMood_VillainRival && opinon <= -20 && bloodLustLike)
                return AteDefOf.Rt_AteMyRivalCorpse2;


            return null;
        }
    }
}
