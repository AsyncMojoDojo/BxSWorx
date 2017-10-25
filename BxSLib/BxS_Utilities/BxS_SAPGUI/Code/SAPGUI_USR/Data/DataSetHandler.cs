﻿using System;
using System.IO;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class DataSetHandler
		{
			#region "Declarations"

				private	Type	_string;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal string DataSetName			{ get	{ return "SAPGUI_USR"; } }
				internal string SchemaName			{ get	{ return "SAPGUI_USR_Schema.xml"; } }
				internal string RepositoryName	{ get	{ return "SAPGUI_USR_Repos.xml"; } }

			#endregion

			////===========================================================================================
			//#region "Constructors"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal DSCreateSchema(string fullFileName, string dataSetName)
			//		{
			//			//this._FullName	= fullFileName;
			//			//this._DSName		= dataSetName;
			//			//this._string	= typeof(string);
			//			//this._sapgui	= new DataSet(dataSetName);
			//			////.............................................
			//			//this.AddTable_Services();
			//			//this.AddTable_MsgServer();
			//			//this.AddTable_WorkSpace();
			//			////.............................................
			//			//this.AddRelation_Service2MsgSrv();
			//			//this.AddRelation_Service2Workspace();
			//			////.............................................
			//			//using (var xmlSW = new StreamWriter(fullFileName))
			//			//	{
			//			//		this._sapgui.WriteXmlSchema(xmlSW);
			//			//		xmlSW.Close();
			//			//	}
			//		}

			//#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	DataSet	GetDataSet(string path)
					{
						var			lo_DS							= new DataSet(this.DataSetName);
						string	lc_SchemaFullName	= Path.Combine(path,	this.SchemaName);
						string	lc_ReposFullName	= Path.Combine(path,	this.RepositoryName);
						//.............................................
						if (Directory.Exists(path))
							{
								if (!File.Exists(lc_SchemaFullName))
									{ this.CreateDSSchema(lo_DS, lc_SchemaFullName); }

								try
									{
										lo_DS.ReadXmlSchema(lc_SchemaFullName);
										lo_DS.ReadXml(lc_ReposFullName, XmlReadMode.IgnoreSchema);
									}
									catch (Exception)
										{
											bool x = false;
											x = !x;
											// TO-DO: log exception
										}
							}
						else
							{ lo_DS	= null; }
						//.............................................
						return	lo_DS;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CreateDSSchema(DataSet dataSet, string schemaFullName)
					{
						this._string	= typeof(string);
						//.............................................
						this.AddTable_Services(dataSet);
						this.AddTable_MsgServer(dataSet);
						this.AddTable_WorkSpace(dataSet);
						//.............................................
						this.AddRelation_Service2MsgSrv(dataSet);
						this.AddRelation_Service2Workspace(dataSet);
						//.............................................
						FileStream lo_FS = null;

						try
							{
								lo_FS	= new FileStream(schemaFullName, FileMode.Create);
								using (var SW = new StreamWriter(lo_FS, System.Text.Encoding.UTF8, 512, false ))
									{
										dataSet.WriteXmlSchema(SW);
										SW.Close();
									}

							}
							catch (Exception)
								{
									bool x = false;
									x = !x;
									// TO-DO: log exception
								}
							finally
								{ lo_FS?.Dispose(); }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2MsgSrv(DataSet dataSet)
					{
						DataColumn	lo_ColParent	= dataSet.Tables["MsgServer"].Columns["UUID"];
						DataColumn	lo_ColChild		= dataSet.Tables["Services"].Columns["MsgServer_ID"];
						var					lo_Rel				= new DataRelation("Service2MsgServer", lo_ColParent, lo_ColChild);
						//.............................................
						dataSet.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2Workspace(DataSet dataSet)
					{
						DataColumn	lo_ColParent	= dataSet.Tables["WorkSpace"].Columns["UUID"];
						DataColumn	lo_ColChild		= dataSet.Tables["Services"].Columns["Workspace_ID"];
						var					lo_Rel				= new DataRelation("Service2Workspace", lo_ColParent, lo_ColChild);
						//.............................................
						dataSet.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpace(DataSet dataSet)
					{
						var WorkSpace	= new DataTable("WorkSpace");
						//.............................................
						this.AddColumn_UUID(WorkSpace);
						//.............................................
						WorkSpace.Columns.Add("Description"		, this._string);
						WorkSpace.Columns.Add("Parent_UUID"		, this._string);
						WorkSpace.Columns.Add("Hierarchy_ID"	, this._string);
						//.............................................
						dataSet.Tables.Add(WorkSpace);	
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_MsgServer(DataSet dataSet)
					{
						var MsgServer	= new DataTable("MsgServer");
						//.............................................
						this.AddColumn_UUID					(MsgServer);
						this.AddColumn_Name					(MsgServer);
						this.AddColumn_Description	(MsgServer);
						//.............................................
						this.AddColumn_TypeString(MsgServer, "Host", 20);
						this.AddColumn_TypeString(MsgServer, "Port", 20);
						//.............................................
						dataSet.Tables.Add(MsgServer);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_Services(DataSet dataSet)
					{
						var Services	= new DataTable("Services");
						//.............................................
						this.AddColumn_UUID					(Services);
						this.AddColumn_Name					(Services);
						this.AddColumn_Description	(Services);
						//.............................................
						Services.Columns.Add("Type"						, this._string);
						Services.Columns.Add("Server"					, this._string);
						Services.Columns.Add("SystemID"				, this._string);
						Services.Columns.Add("SNC_Name"				, this._string);
						Services.Columns.Add("SNC_Op"					, this._string);
						Services.Columns.Add("SAPCodePage"		, this._string);
						Services.Columns.Add("DownUpCodePage"	, this._string);
						//.............................................
						this.AddColumn_TypeString(Services, "MsgServer_ID", 32);
						this.AddColumn_TypeString(Services, "Workspace_ID", 32);
						//.............................................
						dataSet.Tables.Add(Services);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Common private routines
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_UUID(DataTable	dataTable)
					{	this.AddColumn_TypeString(dataTable, "UUID", 32, true); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Name(DataTable	dataTable)
					{	this.AddColumn_TypeString(dataTable, "Name", 20); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Description(DataTable	dataTable)
					{	this.AddColumn_TypeString(dataTable, "Description", 50); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeString(DataTable	dataTable	,
																					string		name			,
																					int				maxLength	,
																					bool			isKey = false)
					{
						var Col		= new DataColumn(name, this._string)
													{	Unique		= isKey			,
														MaxLength	= maxLength		};
						dataTable.Columns.Add(Col);
					}

			#endregion

		}
}
