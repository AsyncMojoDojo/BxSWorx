using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.DataContainer;
//�������������������������������������������������������������������������������������������������
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_DataContainer
		{
			private const string	cz_TestDir			= "Test Resources"		;
			private const string	cz_TestFileName	= "Test DTCntlr.xml"	;
			private const string	cz_TestIniName	= "saplogon_Test.ini"	;
			//...................................................
			private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest			= Path.Combine(_Path		,	cz_TestDir			);
			private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestFileName	);
			//...................................................
			private readonly ToolSet _TS	= new ToolSet();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_DataCon_Cntlr()
				{
					int	ln_Cnt		= 0;
					var lt_Types	= new List<Type>	{	typeof(DTO), typeof(DTOSUB)	};
					//...............................................
					ln_Cnt	++;

					if (File.Exists(_TestFullNme))	File.Delete(_TestFullNme);
					Assert.IsFalse	(	File.Exists(_TestFullNme)	,	$"DTCntlr1: {ln_Cnt}: Not Exists"	);
					//...............................................
					ln_Cnt	++;

					DTController<IDTO, Guid>	lo_DC0	= this._TS.CreateDTController<IDTO, Guid>(_TestFullNme,	(Guid ID) => new DTO() { Key = ID } );
					IDTO											lo_DTO0	= lo_DC0.DataTable.Create(Guid.NewGuid());

					lo_DC0.DataTable.AddUpdate(lo_DTO0.Key, lo_DTO0);
					bool lb_Sve0	= lo_DC0.Save();

					Assert.IsNotNull	(	lo_DC0	,	$"DTCntlr: {ln_Cnt}: Instantiate"	);
					Assert.IsFalse		(	lb_Sve0	,	$"DTCntlr1: {ln_Cnt}: Save"				);
					//...............................................
					ln_Cnt	++;

					DTController<IDTO, Guid>	lo_DC1	= this._TS.CreateDTController<IDTO, Guid>(_TestFullNme,	(Guid ID) => new DTO() { Key = ID }, lt_Types );
					IDTO											lo_DTO1	= lo_DC1.DataTable.Create(Guid.NewGuid());
					var												lo_DTOs	= new DTOSUB() { Key = Guid.NewGuid() };
					lo_DTO1.Dto	= lo_DTOs;

					lo_DC1.DataTable.AddUpdate(lo_DTO1.Key, lo_DTO1);
					bool lb_Sve1	= lo_DC1.Save();

					Assert.IsNotNull	(	lo_DC1											,	$"DTCntlr1: {ln_Cnt}: Instantiate"	);
					Assert.IsTrue			(	lb_Sve1											,	$"DTCntlr1: {ln_Cnt}: Save"					);
					Assert.IsTrue			(	File.Exists(_TestFullNme)		,	$"DTCntlr1: {ln_Cnt}: Exists"				);

					Assert.AreEqual		(	1	, lo_DC1.DataTable.Count	,	$"DTCntlr1: {ln_Cnt}: Count"				);
					//...............................................
					ln_Cnt	++;

					DTController<IDTO, Guid>	lo_DC2;
					DTController<IDTO, Guid>	lo_DC3;

					lo_DC2	= this._TS.CreateDTController<IDTO, Guid>(_TestFullNme,	(Guid ID) => new DTO() { Key = ID }						);
					lo_DC3	= this._TS.CreateDTController<IDTO, Guid>(_TestFullNme,	(Guid ID) => new DTO() { Key = ID }, lt_Types );

					Assert.AreEqual		(0, lo_DC2.DataTable.Count	,	$"DCTable: {ln_Cnt}: Load1");
					Assert.AreEqual		(1, lo_DC3.DataTable.Count	,	$"DCTable: {ln_Cnt}: Load2");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_DataCont_Table()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;
					DataTable<IDTO, Guid> lo_DT = this._TS.CreateDataTable<IDTO, Guid>(	(Guid ID) => new DTO() );

					Assert.IsNotNull	(		lo_DT				,	$"DCTable: {ln_Cnt}: Ins");
					Assert.AreEqual		(0, lo_DT.Count	,	$"DCTable: {ln_Cnt}: Cnt");
					Assert.IsFalse		(lo_DT.IsDirty	, $"DCTable: {ln_Cnt}: Pro");
					//...............................................
					ln_Cnt	++;
					IDTO lo_Obj	= new DTO() { Key = Guid.NewGuid(), Key1 = Guid.NewGuid(), IsValid	= true };

					Assert.IsTrue		(		lo_DT.AddUpdate(lo_Obj.Key, lo_Obj)	,	$"DCTable: {ln_Cnt}: Add"			);
					Assert.AreEqual	(1, lo_DT.Count													,	$"DCTable: {ln_Cnt}: Write"		);
					Assert.IsTrue		(		lo_DT.IsDirty												, $"DCTable: {ln_Cnt}: Prop"		);
					Assert.IsTrue		(		lo_DT.Exists(lo_Obj.Key)						, $"DCTable: {ln_Cnt}: Prop"		);
					//...............................................
					ln_Cnt	++;
					IDTO lo_Obj0	= lo_DT.Get(Guid.Empty);
					IDTO lo_Obj1	= lo_DT.Get(lo_Obj.Key);

					Assert.IsNotNull	(lo_Obj0										,	$"DCTable: {ln_Cnt}: Write"	);
					Assert.AreEqual		(Guid.Empty	, lo_Obj0.Key		,	$"DCTable: {ln_Cnt}: Write"	);
					Assert.AreEqual		(lo_Obj.Key	, lo_Obj1.Key		,	$"DCTable: {ln_Cnt}: Write"	);

					IList<Guid> e = lo_DT.List<Guid>("XXX");
					IList<Guid> x = lo_DT.List<Guid>("Key");
					Assert.AreEqual	(0, e.Count	,	$"DCTable: {ln_Cnt}: Write"	);
					Assert.AreEqual	(1, x.Count	,	$"DCTable: {ln_Cnt}: Write"	);

					IList<IDTO> a = lo_DT.ValueListFor("Key", lo_Obj.Key);
					IList<IDTO> b = lo_DT.ValueListFor("Key", lo_Obj.Key, "Key1", lo_Obj.Key1);
					IList<IDTO> c = lo_DT.ValueListFor("Key", lo_Obj.Key, "Key1", Guid.Empty);
					IList<IDTO> d = lo_DT.ValueListFor("Key", lo_Obj.Key, "Key1", Guid.NewGuid());

					Assert.AreEqual	(1, a.Count	,	$"DCTable: {ln_Cnt}: Write"	);
					Assert.AreEqual	(1, b.Count	,	$"DCTable: {ln_Cnt}: Write"	);
					Assert.AreEqual	(0, c.Count	,	$"DCTable: {ln_Cnt}: Write"	);
					Assert.AreEqual	(0, d.Count	,	$"DCTable: {ln_Cnt}: Write"	);
				}

			//===========================================================================================
			#region "Local"

				private interface IDTO
					{
						Guid Key			{ get; set; }
						Guid Key1			{ get; set; }
						bool IsValid	{ get; set;	}
						DTOSUB	Dto		{ get; set;	}
					}

				[DataContract]

				private class DTO : IDTO
					{
						[DataMember]	public Guid		Key     { get; set; }
						[DataMember]	public Guid		Key1    { get; set; }
						[DataMember]	public bool		IsValid { get; set; }
						[DataMember]	public DTOSUB Dto			{ get; set; }
					}

				[DataContract]

				private class DTOSUB
					{
						[DataMember]	public Guid Key     { get; set; }
					}
			#endregion

		}
}
