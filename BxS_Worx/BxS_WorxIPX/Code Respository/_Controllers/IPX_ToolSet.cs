﻿using System;
//.........................................................
using BxS_WorxUtil.Main;
using BxS_WorxUtil.General;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal sealed class IPX_ToolSet
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy< IPX_ToolSet >	_Instance		= new		Lazy< IPX_ToolSet >( ()=>	new IPX_ToolSet() , cz_LM );
				internal	static					IPX_ToolSet					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPX_ToolSet()
					{
						this._UtlCntlr	= new	Lazy< IUTL_Controller >	( ()=>	UTL_Controller.Instance	, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IO					IO					{ get	{ return	this._UtlCntlr.Value.IO					; } }
				internal	Serializer	Serializer	{ get	{ return	this._UtlCntlr.Value.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< IUTL_Controller >		_UtlCntlr;

			#endregion

		}
}