﻿using System											;
using System.Collections.Generic	;
using System.Reflection						;
//.........................................................
using BxS_Worx.Dashboard.UI.Button	;
using BxS_Worx.Dashboard.Utilities	;

using static	BxS_Worx.Dashboard.UI.DB_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal sealed class DB_TBarFactory : IDB_TBarFactory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy< IDB_TBarFactory >	_Instance		= new		Lazy< IDB_TBarFactory >( ()=>		new DB_TBarFactory() , cz_LM );
				internal	static					IDB_TBarFactory					Instance		{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DB_TBarFactory()
					{
						this._BtnTypes	= new	Lazy<Dictionary<string , Type>>	(	()=>	GetManifestOf()	, cz_LM	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<Dictionary<string , Type>>		_BtnTypes	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Toolbar"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_TBarScene CreateScene( string id , IList<IButtonProfile> list )
					{
						var lo_Scene	= new	UC_TBarScene( id );

						foreach ( IButtonProfile lo_BtnProf in list )
							{
								lo_Scene.AddButton( this.CreateButton( lo_BtnProf ) );
							}

						lo_Scene.IsReady		= true ;
						//...
						return	lo_Scene ;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IUC_Button CreateButton( IButtonProfile	profile )
					{
						IUC_Button	lo_Btn	= null;
						//...
						if ( this._BtnTypes.Value.TryGetValue( profile.ButtonType , out Type lo_BtnType ) )
							{
								lo_Btn	= Activator.CreateInstance( lo_BtnType )	as IUC_Button;
								lo_Btn.SetProfile		= profile	;
							}
						//...
						return	lo_Btn ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	Dictionary<string , Type>	GetManifestOf()
					{
						var lt_Types	=	new Dictionary<string , Type>();
						//...
						foreach ( Type lo_Type in Toolset.TypesImplementingIFaceOf<IUC_Button>() )
							{
								ButtonTypeAttribute lc_Attr		=	lo_Type.GetCustomAttribute<ButtonTypeAttribute>();
								lt_Types.Add( lc_Attr.BtnType , lo_Type );
							}
						//...
						return	lt_Types;
					}

			#endregion

		}
}