using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#nullable disable

namespace AteCorpseMood
{
    [DefOf]
    public static class AteDefOf
    {
        static AteDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(AteDefOf));
        }
        #region thoughtDefs

        public static ThoughtDef Rt_AteMyAuntCorpse;
        public static ThoughtDef Rt_AteMyBrotherCorpse;
        public static ThoughtDef Rt_AteMyCousinCorpse;
        public static ThoughtDef Rt_AteMyDaughterCorpse;
        public static ThoughtDef Rt_AteMyExHusbandCorpse;
        public static ThoughtDef Rt_AteMyExHusbandCorpse2;
        public static ThoughtDef Rt_AteMyExLoverCorpse;
        public static ThoughtDef Rt_AteMyExLoverCorpse2;
        public static ThoughtDef Rt_AteMyExWifeCorpse;
        public static ThoughtDef Rt_AteMyExWifeCorpse2;
        public static ThoughtDef Rt_AteMyFatherCorpse;
        public static ThoughtDef Rt_AteMyFianceCorpse;
        public static ThoughtDef Rt_AteMyFianceeCorpse;
        public static ThoughtDef Rt_AteMyFriendCorpse;
        public static ThoughtDef Rt_AteMyGrandchildCorpse;
        public static ThoughtDef Rt_AteMyGrandparentCorpse;
        public static ThoughtDef Rt_AteMyHalfSiblingCorpse;
        public static ThoughtDef Rt_AteMyHusbandCorpse;
        public static ThoughtDef Rt_AteMyKinCorpse;
        public static ThoughtDef Rt_AteMyLoverCorpse;
        public static ThoughtDef Rt_AteMyMotherCorpse;
        public static ThoughtDef Rt_AteMyNephewCorpse;
        public static ThoughtDef Rt_AteMyNieceCorpse;
        public static ThoughtDef Rt_AteMyRivalCorpse;
        public static ThoughtDef Rt_AteMyRivalCorpse2;
        public static ThoughtDef Rt_AteMySisterCorpse;
        public static ThoughtDef Rt_AteMySonCorpse;
        public static ThoughtDef Rt_AteMyUncleCorpse;
        public static ThoughtDef Rt_AteMyWifeCorpse;

        #endregion

        public static TraitDef Cannibal;
    }
}
