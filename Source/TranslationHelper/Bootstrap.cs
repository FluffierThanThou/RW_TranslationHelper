using CommunityCoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace TranslationHelper
{
    public class Bootstrap : SpecialInjector
    {
        #region Methods

        public override bool Inject()
        {
            // detour the base Translate to our custom version.
            MethodInfo source, destination;

            // version with arguments ( both Binder and ParamaterModifiers can be left null? )
            source = typeof( Translator ).GetMethod( "Translate",
                                                           BindingFlags.Public | BindingFlags.Static,
                                                           null,
                                                           new[] { typeof( string ), typeof( object[] ) },
                                                           null );
            destination = typeof( TranslationHelper ).GetMethod( "Translate",
                                                           BindingFlags.Public | BindingFlags.Static,
                                                           null,
                                                           new[] { typeof( string ), typeof( object[] ) },
                                                           null );
            CommunityCoreLibrary.Detours.TryDetourFromTo( source, destination );

            // version without arguments
            source = typeof( Translator ).GetMethod( "Translate",
                                                           BindingFlags.Public | BindingFlags.Static,
                                                           null,
                                                           new[] { typeof( string ) },
                                                           null );
            destination = typeof( TranslationHelper ).GetMethod( "Translate",
                                                           BindingFlags.Public | BindingFlags.Static,
                                                           null,
                                                           new[] { typeof( string ) },
                                                           null );
            CommunityCoreLibrary.Detours.TryDetourFromTo( source, destination );

            return true;
        }

        #endregion Methods
    }
}