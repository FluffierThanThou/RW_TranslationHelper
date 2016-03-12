using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace TranslationHelper
{
    public static class TranslationHelper
    {
        #region Fields

        public static Dictionary<string, int> failedKeys = new Dictionary<string, int>();

        #endregion Fields

        #region Methods

        public static string Translate( this string key )
        {
            if ( key == null || key == string.Empty )
            {
                return key;
            }
            if ( LanguageDatabase.activeLanguage == null )
            {
                Log.Error( "No active language! Cannot translate from key " + key + "." );
                return key;
            }
            string text;
            if ( !LanguageDatabase.activeLanguage.TryGetTextFromKey( key, out text ) )
            {
                LanguageDatabase.defaultLanguage.TryGetTextFromKey( key, out text );
                if ( Prefs.DevMode && !failedKeys.ContainsKey( key ) )
                {
                    failedKeys.Add( key, 0 );
                }
            }
            return text;
        }

        public static string Translate( this string key, params object[] args )
        {
            if ( key == null || key == string.Empty )
            {
                return key;
            }
            if ( LanguageDatabase.activeLanguage == null )
            {
                Log.Error( "No active language! Cannot translate from key " + key + "." );
                return key;
            }
            string text;
            if ( !LanguageDatabase.activeLanguage.TryGetTextFromKey( key, out text ) )
            {
                LanguageDatabase.defaultLanguage.TryGetTextFromKey( key, out text );
                if ( Prefs.DevMode && !failedKeys.ContainsKey( key ) )
                {
                    failedKeys.Add( key, args.Count() );
                }
            }
            string result = text;
            try
            {
                result = string.Format( text, args );
            }
            catch ( Exception ex )
            {
                Log.Error( "Exception translating '" + text + "': " + ex.ToString() );
            }
            return result;
        }

        #endregion Methods
    }
}