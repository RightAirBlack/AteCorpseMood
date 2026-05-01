using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AteCorpseMood
{
    [HarmonyPatch(typeof(FoodUtility))]
    public static class IngestCorpsePatch
    {
        [HarmonyPatch(nameof(FoodUtility.ThoughtsFromIngesting))]
        [HarmonyPostfix]
        public static void IngsetPatch(List<FoodUtility.ThoughtFromIngesting> __result, Pawn ingester,
    Thing foodSource,
    ThingDef foodDef)
        {
            if (!ModSetting_AteCorpseMood.EnableEatCorpseMood) return;
            if (ingester?.needs?.mood != null && foodSource.def.IsCorpse)
            {
                var corpsePawn = (foodSource as Corpse)?.InnerPawn;
                L.M("1");
                if (corpsePawn != null)
                {
                    L.M("2");
                    var thought = Thought_AteCorpse.MakeThought(ingester, corpsePawn);
                    L.M("3");
                    if (thought != null)
                    {
                        L.M("4");
                        thought.SetEatenPawn(corpsePawn);
                        ingester.needs.mood.thoughts?.memories?.TryGainMemory(thought, corpsePawn);
                    }
                }


            }

        }



    }
}
