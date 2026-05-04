using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static RimWorld.FoodUtility;

namespace AteCorpseMood
{
#if true
    [HarmonyPatch(typeof(FoodUtility))]
    public static class FoodUtilityPatch
    {

        [HarmonyPatch(nameof(FoodUtility.ThoughtsFromIngesting))]
        [HarmonyPostfix]
        public static void ThoughtsIngsetPatch(List<FoodUtility.ThoughtFromIngesting> __result, Pawn ingester,
    Thing foodSource,
    ThingDef foodDef)
        {

            if (!ModSetting_AteCorpseMood.EnableEatCorpseMood) return;
            if (ingester?.needs?.mood != null && foodSource.def.IsCorpse)
            {
                var corpsePawn = (foodSource as Corpse)?.InnerPawn;

                if (corpsePawn != null)
                {


                    var def = Thought_AteCorpse.GetThoughtDefForRelation(ingester, corpsePawn);

                    if (def != null)
                    {

                        ThoughtFromIngesting thoughtFromIngesting = default;
                        thoughtFromIngesting.thought = def;
                        thoughtFromIngesting.fromPrecept = null;
                        __result.Add(thoughtFromIngesting);
                    }
                }
            }
        }
    }
#endif
#if true
    [HarmonyPatch(typeof(Thing))]
    public static class IngestCorpsePatchForThing
    {
        [HarmonyPatch(nameof(Thing.Ingested))]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var thoughtsFromIngesting = AccessTools.Method(typeof(FoodUtility),
                nameof(FoodUtility.ThoughtsFromIngesting));
            if (thoughtsFromIngesting == null)
            {
                L.E("path failed,cannot get method FoodUtility.ThoughtsFromIngesting");
                foreach (var item in instructions)
                {
                    yield return item;
                }
                yield break;
            }
            var patch = AccessTools.Method(typeof(IngestCorpsePatchForThing), nameof(IngestCorpsePatchForThing.Patch));
            if (patch == null)
            {
                L.E("path failed,cannot get method IngestCorpsePatchForThing.Patch");
                foreach (var item in instructions)
                {
                    yield return item;
                }
                yield break;
            }

            var patched = false;
            L.M("patching Thing.Ingested...");
            foreach (var code in instructions)
            {
                if (!patched && code.Calls(thoughtsFromIngesting))
                {
                    patched = true;
                    code.opcode = OpCodes.Call;
                    code.operand = patch;
                }
                yield return code;
            }
            L.M("patched!");
        }

        private static List<ThoughtFromIngesting> tmpList = [];
        static List<ThoughtFromIngesting> Patch(Pawn ingester, Thing foodSource, ThingDef foodDef)
        {
            var list = FoodUtility.ThoughtsFromIngesting(ingester, foodSource, foodDef);
            if (!ModSetting_AteCorpseMood.EnableEatCorpseMood) return list;
            if (ingester?.needs?.mood != null && foodSource.def.IsCorpse && foodSource is Corpse corpse)
            {
                var corpsePawn = corpse.InnerPawn;
                if (corpsePawn == null) { return list; }

                tmpList.Clear();
                foreach (var pair in list)
                {
                    if (pair.thought.thoughtClass != typeof(Thought_AteCorpse))
                    {
                        tmpList.Add(pair);
                        continue;
                    }
                    if (ThoughtMaker.MakeThought(pair.thought, pair.fromPrecept) is not Thought_AteCorpse thought_Memory)
                    {
                        L.E("Cast faild!This error should not happened!on IngestCorpsePatchForThing.Patch");
                        tmpList.Clear();
                        return list;
                    }
                    thought_Memory.SetEatenPawn(corpsePawn);
                    L.D($"{ingester.NameShortColored} ate {corpsePawn.NameShortColored} with {pair.thought.Label}");
                    ingester.needs.mood.thoughts.memories.TryGainMemory(thought_Memory,corpsePawn);
                }
                return tmpList;

            }
            return list;

        }
    }
#endif
}
