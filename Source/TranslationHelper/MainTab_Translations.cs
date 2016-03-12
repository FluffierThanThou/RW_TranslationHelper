using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace TranslationHelper
{
    public class MainTab_Translations : MainTabWindow
    {
        #region Methods

        public override void DoWindowContents( Rect inRect )
        {
            base.DoWindowContents( inRect );

            StringBuilder text = new StringBuilder();
            foreach ( var translation in TranslationHelper.failedKeys )
                text.AppendLine( "<" + translation.Key + ">" + translation.Value + " argument(s)</" + translation.Key + ">" );

            GUI.TextArea( inRect, text.ToString() );
        }

        #endregion Methods
    }
}