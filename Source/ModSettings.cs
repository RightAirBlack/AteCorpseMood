using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace AteCorpseMood
{
    public class Mod_AteCorpseMood : Mod
    {
        public static ModSetting_AteCorpseMood? settings;
        public override string SettingsCategory() => "[RT]AteCorpseMood Settings";
        public Mod_AteCorpseMood(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSetting_AteCorpseMood>();

        }

        public override void DoSettingsWindowContents(Rect inRect) => settings?.DoSettingsWindowContents(inRect);
    }

    public class ModSetting_AteCorpseMood : ModSettings
    {
        private Vector2 scrollPos;
        public static readonly float HEIGHT = 700f;

        public static bool EnableEatCorpseMood = true;
        #region details
        public static bool EnableEatCorpseMood_Child = true;
        public static bool EnableEatCorpseMood_Spouse = true;
        public static bool EnableEatCorpseMood_Fiance = true;
        public static bool EnableEatCorpseMood_Lover = true;
        public static bool EnableEatCorpseMood_Sibling = true;
        public static bool EnableEatCorpseMood_Grandchild = true;
        public static bool EnableEatCorpseMood_Parent = true;
        public static bool EnableEatCorpseMood_NieceNephew = true;
        public static bool EnableEatCorpseMood_HalfSibling = true;
        public static bool EnableEatCorpseMood_AuntUncle = true;
        public static bool EnableEatCorpseMood_Grandparent = true;
        public static bool EnableEatCorpseMood_Cousin = true;
        public static bool EnableEatCorpseMood_Kin = true;
        public static bool EnableEatCorpseMood_Friend = true;
        public static bool EnableEatCorpseMood_ExLover = true;
        public static bool EnableEatCorpseMood_ExSpouse = true;
        public static bool EnableEatCorpseMood_Rival = true;
        public static bool EnableEatCorpseMood_VillainRival = true;
        public static bool EnableEatCorpseMood_VillainExLover = true;
        public static bool EnableEatCorpseMood_VillainExSpouse = true;
        #endregion

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref EnableEatCorpseMood, "EnableEatFamilyCorpse", true);

            #region details
            Scribe_Values.Look(ref EnableEatCorpseMood_Child, "EnableEatCorpseMood_Child", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Spouse, "EnableEatCorpseMood_Spouse", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Fiance, "EnableEatCorpseMood_Fiance", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Lover, "EnableEatCorpseMood_Lover", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Sibling, "EnableEatCorpseMood_Sibling", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Grandchild, "EnableEatCorpseMood_Grandchild", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Parent, "EnableEatCorpseMood_Parent", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_NieceNephew, "EnableEatCorpseMood_NieceNephew", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_HalfSibling, "EnableEatCorpseMood_HalfSibling", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_AuntUncle, "EnableEatCorpseMood_AuntUncle", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Grandparent, "EnableEatCorpseMood_Grandparent", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Cousin, "EnableEatCorpseMood_Cousin", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Kin, "EnableEatCorpseMood_Kin", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Friend, "EnableEatCorpseMood_Friend", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_ExLover, "EnableEatCorpseMood_ExLover", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_ExSpouse, "EnableEatCorpseMood_ExSpouse", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_Rival, "EnableEatCorpseMood_Rival", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_VillainRival, "EnableEatCorpseMood_VillainRival", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_VillainExLover, "EnableEatCorpseMood_VillainExLover", true);
            Scribe_Values.Look(ref EnableEatCorpseMood_VillainExSpouse, "EnableEatCorpseMood_VillainExSpouse", true);
            #endregion
        }
        public void DoSettingsWindowContents(Rect inRect)
        {
            var rect = new Rect(0f, 0f, inRect.width - 16f, HEIGHT);
            Widgets.BeginScrollView(inRect, ref scrollPos, rect);

            var list = new Listing_Standard();
            list.Begin(rect);
            #region settings ui
            list.Label("AteCorpseMood.ModSettingsTitle".Translate());
            list.GapLine();


            list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood".Translate(), ref EnableEatCorpseMood);
            if (EnableEatCorpseMood)
            {
                #region details checkboxs
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Child".Translate(), ref EnableEatCorpseMood_Child);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Spouse".Translate(), ref EnableEatCorpseMood_Spouse);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Fiance".Translate(), ref EnableEatCorpseMood_Fiance);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Lover".Translate(), ref EnableEatCorpseMood_Lover);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Sibling".Translate(), ref EnableEatCorpseMood_Sibling);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Grandchild".Translate(), ref EnableEatCorpseMood_Grandchild);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Parent".Translate(), ref EnableEatCorpseMood_Parent);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_NieceNephew".Translate(), ref EnableEatCorpseMood_NieceNephew);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_HalfSibling".Translate(), ref EnableEatCorpseMood_HalfSibling);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_AuntUncle".Translate(), ref EnableEatCorpseMood_AuntUncle);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Grandparent".Translate(), ref EnableEatCorpseMood_Grandparent);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Cousin".Translate(), ref EnableEatCorpseMood_Cousin);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Kin".Translate(), ref EnableEatCorpseMood_Kin);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Friend".Translate(), ref EnableEatCorpseMood_Friend);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_ExLover".Translate(), ref EnableEatCorpseMood_ExLover);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_ExSpouse".Translate(), ref EnableEatCorpseMood_ExSpouse);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_Rival".Translate(), ref EnableEatCorpseMood_Rival);
                list.Gap();
                list.Label("AteCorpseMood.ModSettingsEnableEatCorpseMood_ForBloodLustLike".Translate());
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_VillainRival".Translate(), ref EnableEatCorpseMood_VillainRival);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_VillainExLover".Translate(), ref EnableEatCorpseMood_VillainExLover);
                list.CheckboxLabeled("AteCorpseMood.ModSettingsEnableEatCorpseMood_VillainExSpouse".Translate(), ref EnableEatCorpseMood_VillainExSpouse);
                #endregion
            }
            list.GapLine();
            if (list.ButtonText("AteCorpseMood.ModSettingsReset".Translate()))
            {
                EnableEatCorpseMood = true;
                #region details
                EnableEatCorpseMood_Child = true;
                EnableEatCorpseMood_Spouse = true;
                EnableEatCorpseMood_Fiance = true;
                EnableEatCorpseMood_Lover = true;
                EnableEatCorpseMood_Sibling = true;
                EnableEatCorpseMood_Grandchild = true;
                EnableEatCorpseMood_Parent = true;
                EnableEatCorpseMood_NieceNephew = true;
                EnableEatCorpseMood_HalfSibling = true;
                EnableEatCorpseMood_AuntUncle = true;
                EnableEatCorpseMood_Grandparent = true;
                EnableEatCorpseMood_Cousin = true;
                EnableEatCorpseMood_Kin = true;
                EnableEatCorpseMood_Friend = true;
                EnableEatCorpseMood_ExLover = true;
                EnableEatCorpseMood_ExSpouse = true;
                EnableEatCorpseMood_Rival = true;
                EnableEatCorpseMood_VillainRival = true;
                EnableEatCorpseMood_VillainExLover = true;
                EnableEatCorpseMood_VillainExSpouse = true;
                #endregion

            }
            #endregion
            list.End();
            Widgets.EndScrollView();
        }
    }
}
