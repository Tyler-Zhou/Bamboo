//-----------------------------------------------------------------------
// <copyright file="OceanExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanExport.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Helper;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Common.ServiceInterface.DataObjects;
    using System.Xml;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    partial class OceanExportService
    {

        #region IAMSACIISFService 成员
        /// <summary>
        /// 查询HBL下ams列表
        /// </summary>
        /// <param name="HBLID"></param>
        /// <returns></returns>
        public List<OceanHBL2AmsAciIsf> GetAmsAciIsfOjbectsList(Guid HBLID, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(HBLID, "HBLID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanHBL2AmsAciIsfByHBLID");

                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, HBLID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanHBL2AmsAciIsf> results = BulidAmsAciIsfByDataSet(ds);

                return results;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询相同Customer HBL上次ams列表
        /// </summary>
        /// <param name="HBLID"></param>
        /// <returns></returns>
        public List<OceanHBL2AmsAciIsf> GetLastAmsAciIsfOjbectsList(Guid HBLID, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(HBLID, "HBLID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetLastOceanHBL2AmsAciIsfByHBLID");

                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, HBLID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanHBL2AmsAciIsf> results = BulidAmsAciIsfByDataSet(ds);

                return results;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存AMS列表
        /// </summary>
        /// <param name="oha"></param>
        public void SaveAmsAciIsfOjbects(List<OceanHBL2AmsAciIsf> oha, Guid hblID, Guid saveBy, AMSEntryType amsEntryType)
        {
            List<OceanHBL2AmsAciIsf> list = GetAmsAciIsfOjbectsList(hblID, true);
            if (list != null)
                if (list.Count > 0)
                {
                    Guid[] id = new Guid[list.Count];
                    DateTime?[] dt = new DateTime?[list.Count];
                    for (int k = 0; k < list.Count; k++)
                    {
                        OceanHBL2AmsAciIsf o = list[k];
                        id[k] = o.ID;
                        dt[k] = o.UpdateDate;
                    }
                    //删除原有数据
                    this.RemoveAmsAciIsfByID(id, saveBy, dt);
                }

            int i = oha.Count;
            Guid[] _ids = new Guid[i];
            Guid[] _oceanhblids = new Guid[i];
            string[] _vesselName = new string[i];
            string[] _imo = new string[i];
            Guid?[] _flag = new Guid?[i];
            string[] _voyageNumber = new string[i];
            string[] _marks = new string[i];
            string[] _shippers = new string[i];
            string[] _consignees = new string[i];
            string[] _notifypartys = new string[i];
            string[] _sellers = new string[i];
            string[] _buyers = new string[i];
            string[] _shiptos = new string[i];
            string[] _manufacturers = new string[i];
            string[] _stuffinglocations = new string[i];
            string[] _consolidators = new string[i];
            string[] _bookingpartyinfo = new string[i];
            byte?[] _isfimporteridtypes = new byte?[i];
            string[] _isfimporterids = new string[i];
            string[] _isfimporterfirstnames = new string[i];
            string[] _isfimporterlastnames = new string[i];
            DateTime?[] _isfimporterdateofbirths = new DateTime?[i];
            Guid?[] _isfimportercountryofissuances = new Guid?[i];
            byte?[] _cargoType = new byte?[i];
            byte?[] _bondreferencetypes = new byte?[i];
            string[] _bondreferencenumbers = new string[i];
            byte?[] _bondactivitycodes = new byte?[i];
            byte?[] _shipmenttypes = new byte?[i];
            byte?[] _importerofrecordnumberqualifiers = new byte?[i];
            string[] _importerofrecordnumbers = new string[i];
            string[] _importerofrecordfirstnames = new string[i];
            string[] _importerofrecordlastnames = new string[i];
            DateTime?[] _importerofrecorddobs = new DateTime?[i];
            Guid?[] _importerofpassportissuancecountrys = new Guid?[i];
            byte?[] _consigneenumberqualifiers = new byte?[i];
            string[] _consigneenumbers = new string[i];
            string[] _consigneefirstnames = new string[i];
            string[] _consigneelastnames = new string[i];
            DateTime?[] _consigneepassportdobs = new DateTime?[i];
            Guid?[] _consigneepassportissuancecountrys = new Guid?[i];
            string[] _containers = new string[i];
            string[] _containersDetails = new string[i];
            DateTime?[] _updatedates = new DateTime?[i];
            Guid _saveBy = oha[0].CreateBy;
            string[] lastportofcall = new string[i];
            string[] firstportofcall = new string[i];
            string[] pol = new string[i];
            DateTime?[] etd = new DateTime?[i];
            DateTime?[] firstPortOfCallDate = new DateTime?[i];
            for (int j = 0; j < oha.Count; j++)
            {
                OceanHBL2AmsAciIsf o = oha[j];

                _ids[j] = Guid.Empty;
                _oceanhblids[j] = o.OceanHBLID;
                _vesselName[j] = o.VesselName;
                _imo[j] = o.IMO;
                _flag[j] = o.Flag;
                _voyageNumber[j] = o.VoyageNumber;
                _marks[j] = o.Mark;
                _shippers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.Shipper, true, false);
                _consignees[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.Consignee, true, false);
                _shiptos[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.ShipTo, true, false);
                if (amsEntryType != AMSEntryType.StayonBoard)
                {
                    _notifypartys[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.NotifyParty, true, false);
                    _sellers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.Seller, true, false);
                    _buyers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.Buyer, true, false);
                    _manufacturers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.Manufacturer, true, false);
                    _stuffinglocations[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.StuffingLocation, true, false);
                    _consolidators[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.Consolidator, true, false);
                    _bookingpartyinfo[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                    //Consignee和buyer税号
                    _importerofrecordnumberqualifiers[j] = o.ImporterOfRecordNumberQualifier == null ? (byte)0 : (byte)o.ImporterOfRecordNumberQualifier;
                    _importerofrecordnumbers[j] = o.ImporterOfRecordNumber;
                    if (_importerofrecordnumberqualifiers[j] == 1 || _importerofrecordnumberqualifiers[j] == 3)
                    {
                        _importerofrecordfirstnames[j] = string.Empty;
                        _importerofrecordlastnames[j] = string.Empty;
                        _importerofrecorddobs[j] = null;
                        _importerofpassportissuancecountrys[j] = null;
                        if (_importerofrecordnumberqualifiers[j] == 1 && _importerofrecordnumbers[j].Length == 10)
                        {
                            _importerofrecordnumbers[j] += "00";
                        }
                    }
                    else
                    {
                        if (_importerofrecordnumberqualifiers[j] == 2)
                            _importerofpassportissuancecountrys[j] = null;
                        else
                            _importerofpassportissuancecountrys[j] = o.ImporterOfPassportIssuanceCountry;
                        _importerofrecordfirstnames[j] = o.ImporterOfRecordFirstName;
                        _importerofrecordlastnames[j] = o.ImporterOfRecordLastName;
                        _importerofrecorddobs[j] = o.ImporterOfRecordDOB;
                    }
                    _consigneenumberqualifiers[j] = o.ConsigneeNumberQualifier == null ? (byte)0 : (byte)o.ConsigneeNumberQualifier;
                    _consigneenumbers[j] = o.ConsigneeNumber;
                    if (_consigneenumberqualifiers[j] == 1 || _consigneenumberqualifiers[j] == 3)
                    {
                        _consigneefirstnames[j] = string.Empty;
                        _consigneelastnames[j] = string.Empty;
                        _consigneepassportdobs[j] = null;
                        _consigneepassportissuancecountrys[j] = null;
                        if (_consigneenumberqualifiers[j] == 1 && _consigneenumbers[j].Length == 10)
                        {
                            _consigneenumbers[j] += "00";
                        }
                    }
                    else
                    {
                        if (_consigneenumberqualifiers[j] == 2)
                            _consigneepassportissuancecountrys[j] = null;
                        else
                            _consigneepassportissuancecountrys[j] = o.ConsigneePassportIssuanceCountry;
                        _consigneefirstnames[j] = o.ConsigneeFirstName;
                        _consigneelastnames[j] = o.ConsigneeLastName;
                        _consigneepassportdobs[j] = o.ConsigneePassportDOB;
                    }
                }
                else
                {
                    firstPortOfCallDate[j] = o.FirstPortOfCallDate;
                    _bookingpartyinfo[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(o.BookingPartyInfo, true, false);
                    _notifypartys[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                    _sellers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                    _buyers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                    _manufacturers[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                    _stuffinglocations[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                    _consolidators[j] = SerializerHelper.SerializeToString<CustomerDescriptionForAMS>(new CustomerDescriptionForAMS(), true, false);
                }
                _isfimporteridtypes[j] = o.ISFImporterIDType == null ? (byte)0 : (byte)o.ISFImporterIDType;
                _isfimporterids[j] = o.ISFImporterID;
                if (_isfimporteridtypes[j] == 5 || _isfimporteridtypes[j] == 1 || _isfimporteridtypes[j] == 3)
                {
                    _isfimporterfirstnames[j] = string.Empty;
                    _isfimporterlastnames[j] = string.Empty;
                    _isfimporterdateofbirths[j] = null;
                    _isfimportercountryofissuances[j] = null;
                    if (_isfimporteridtypes[j] == 1 && _isfimporterids[j].Length == 10)
                    {
                        _isfimporterids[j] += "00";
                    }
                }
                else//是passport
                {
                    if (_isfimporteridtypes[j] == 2)//如果是ssn，国家为空
                        _isfimportercountryofissuances[j] = null;
                    else
                        _isfimportercountryofissuances[j] = o.ISFImporterCountryOfIssuance;
                    _isfimporterfirstnames[j] = o.ISFImporterFirstName;
                    _isfimporterlastnames[j] = o.ISFImporterLastName;
                    _isfimporterdateofbirths[j] = o.ISFImporterDateOfBirth;
                }
                _cargoType[j] = o.CargoTypeForAMS == null ? (byte)0 : (byte)o.CargoTypeForAMS;
                if (_cargoType[j] == 3)
                    _bondactivitycodes[j] = null;
                else
                    _bondactivitycodes[j] = o.BondActivityCode == null ? (byte)0 : (byte)o.BondActivityCode;
                _bondreferencetypes[j] = o.BondReferenceType == null ? (byte)0 : (byte)o.BondReferenceType;
                _bondreferencenumbers[j] = o.BondReferenceNumber;
                if (_bondreferencetypes[j] == 1 && _bondreferencenumbers[j].Length == 10)
                {
                    _bondreferencenumbers[j] += "00";
                }
                _shipmenttypes[j] = o.ShipmentType == null ? (byte)0 : (byte)o.ShipmentType;

                _updatedates[j] = o.UpdateDate;
                if (o.Container != null)
                {
                    _containers[j] = "<Containers>";
                    foreach (ContainerForAMS c in o.Container)
                    {
                        _containers[j] += SerializerHelper.SerializeToString<ContainerForAMS>(c, true, false);
                    }
                    _containers[j] += "</Containers>";
                }
                else
                    _containers[j] = null;
                if (o.ContainerDetails != null)
                {
                    _containersDetails[j] = "<ContainerDetails>";
                    foreach (ContainerDetailsForAMS c in o.ContainerDetails)
                    {
                        _containersDetails[j] += SerializerHelper.SerializeToString<ContainerDetailsForAMS>(c, true, false);
                    }
                    _containersDetails[j] += "</ContainerDetails>";
                }
                else
                    _containersDetails[j] = null;
                lastportofcall[j] = o.LastPortOfCall;
                firstportofcall[j] = o.FirstPorOtfCall;
                pol[j] = o.PortOfLoading;
                etd[j] = o.Etd;
            }
            ArgumentHelper.AssertGuidNotEmpty(_oceanhblids, "_oceanhblids");
            ArgumentHelper.AssertGuidNotEmpty(_saveBy, "_saveBy");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanHBL2AmsAciIsfInfo");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, _ids.Join());
                db.AddInParameter(dbCommand, "@OceanHBLIDs", DbType.String, _oceanhblids.Join());
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, _vesselName.Join());
                db.AddInParameter(dbCommand, "@IMO", DbType.String, _imo.Join());
                db.AddInParameter(dbCommand, "@Flag", DbType.String, _flag.Join());
                db.AddInParameter(dbCommand, "@VoyageNumber", DbType.String, _voyageNumber.Join());
                db.AddInParameter(dbCommand, "@Marks", DbType.String, _marks.Join());
                db.AddInParameter(dbCommand, "@Shippers", DbType.String, _shippers.Join());
                db.AddInParameter(dbCommand, "@Consignees", DbType.String, _consignees.Join());
                db.AddInParameter(dbCommand, "@NotifyPartys", DbType.String, _notifypartys.Join());
                db.AddInParameter(dbCommand, "@Sellers", DbType.String, _sellers.Join());
                db.AddInParameter(dbCommand, "@Buyers", DbType.String, _buyers.Join());
                db.AddInParameter(dbCommand, "@ShipTos", DbType.String, _shiptos.Join());
                db.AddInParameter(dbCommand, "@Manufacturers", DbType.String, _manufacturers.Join());
                db.AddInParameter(dbCommand, "@StuffingLocations", DbType.String, _stuffinglocations.Join());
                db.AddInParameter(dbCommand, "@Consolidators", DbType.String, _consolidators.Join());
                db.AddInParameter(dbCommand, "@BookingPartyInfo", DbType.String, _bookingpartyinfo.Join());
                db.AddInParameter(dbCommand, "@ISFImporterIDTypes", DbType.String, _isfimporteridtypes.Join());
                db.AddInParameter(dbCommand, "@ISFImporterIDs", DbType.String, _isfimporterids.Join());
                db.AddInParameter(dbCommand, "@ISFImporterFirstNames", DbType.String, _isfimporterfirstnames.Join());
                db.AddInParameter(dbCommand, "@ISFImporterLastNames", DbType.String, _isfimporterlastnames.Join());
                db.AddInParameter(dbCommand, "@ISFImporterDateOfBirths", DbType.String, _isfimporterdateofbirths.Join());
                db.AddInParameter(dbCommand, "@ISFImporterCountryOfIssuances", DbType.String, _isfimportercountryofissuances.Join());
                db.AddInParameter(dbCommand, "@CargoType", DbType.String, _cargoType.Join());
                db.AddInParameter(dbCommand, "@BondReferenceTypes", DbType.String, _bondreferencetypes.Join());
                db.AddInParameter(dbCommand, "@BondReferenceNumbers", DbType.String, _bondreferencenumbers.Join());
                db.AddInParameter(dbCommand, "@BondActivityCodes", DbType.String, _bondactivitycodes.Join());
                db.AddInParameter(dbCommand, "@ShipmentTypes", DbType.String, _shipmenttypes.Join());
                db.AddInParameter(dbCommand, "@ImporterOfRecordNumberQualifiers", DbType.String, _importerofrecordnumberqualifiers.Join());
                db.AddInParameter(dbCommand, "@ImporterOfRecordNumbers", DbType.String, _importerofrecordnumbers.Join());
                db.AddInParameter(dbCommand, "@ImporterOfRecordFirstNames", DbType.String, _importerofrecordfirstnames.Join());
                db.AddInParameter(dbCommand, "@ImporterOfRecordLastNames", DbType.String, _importerofrecordlastnames.Join());
                db.AddInParameter(dbCommand, "@ImporterOfRecordDOBs", DbType.String, _importerofrecorddobs.Join());
                db.AddInParameter(dbCommand, "@ImporterOfPassportIssuanceCountrys", DbType.String, _importerofpassportissuancecountrys.Join());
                db.AddInParameter(dbCommand, "@ConsigneeNumberQualifiers", DbType.String, _consigneenumberqualifiers.Join());
                db.AddInParameter(dbCommand, "@ConsigneeNumbers", DbType.String, _consigneenumbers.Join());
                db.AddInParameter(dbCommand, "@ConsigneeFirstNames", DbType.String, _consigneefirstnames.Join());
                db.AddInParameter(dbCommand, "@ConsigneeLastNames", DbType.String, _consigneelastnames.Join());
                db.AddInParameter(dbCommand, "@ConsigneePassportDOBs", DbType.String, _consigneepassportdobs.Join());
                db.AddInParameter(dbCommand, "@ConsigneePassportIssuanceCountrys", DbType.String, _consigneepassportissuancecountrys.Join());
                db.AddInParameter(dbCommand, "@Containers", DbType.String, _containers.Join());
                db.AddInParameter(dbCommand, "@ContainerDetails", DbType.String, _containersDetails.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, _updatedates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, _saveBy);
                db.AddInParameter(dbCommand, "@LastPortOfCall", DbType.String, lastportofcall.Join());
                db.AddInParameter(dbCommand, "@FirstPortOfCall", DbType.String, firstportofcall.Join());
                db.AddInParameter(dbCommand, "@PortOfLoading", DbType.String, pol.Join());
                db.AddInParameter(dbCommand, "@ETD", DbType.String, etd.Join());
                db.AddInParameter(dbCommand, "@FirstPortOfCallDate", DbType.String, firstPortOfCallDate.Join());

                db.ExecuteNonQuery(dbCommand);
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除AMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="removeByID"></param>
        /// <param name="updateDate"></param>
        public void RemoveAmsAciIsfByID(Guid[] id, Guid removeByID, DateTime?[] updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspRemoveOceanHBL2AmsAciIsfByHBLID]");
                //Guid[] ids = new Guid[] { id };
                //DateTime?[] updateDates = new DateTime?[] { updateDate };
                db.AddInParameter(dbCommand, "@Ids", DbType.String, id.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDate.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);

                db.ExecuteNonQuery(dbCommand);
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<OceanHBL2AmsAciIsf> BulidAmsAciIsfByDataSet(DataSet ds)
        {
            List<OceanHBL2AmsAciIsf> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new OceanHBL2AmsAciIsf
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             OceanHBLID = b.Field<Guid>("OceanHBLID"),
                                             VesselName = b.Field<string>("VesselName"),
                                             IMO = b.Field<string>("IMO"),
                                             Flag = b.Field<Guid?>("Flag"),
                                             VoyageNumber = b.Field<string>("VoyageNumber"),
                                             FlagName = b.Field<string>("FlagName"),
                                             Mark = b.Field<string>("Mark"),
                                             Shipper = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Shipper")),
                                             Consignee = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Consignee")),
                                             NotifyParty = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("NotifyParty")),
                                             Seller = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Seller")),
                                             Buyer = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Buyer")),
                                             ShipTo = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("ShipTo")),
                                             Manufacturer = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Manufacturer")),
                                             StuffingLocation = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("StuffingLocation")),
                                             Consolidator = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Consolidator")),
                                             BookingPartyInfo = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("BookingPartyInfo")),
                                             ShipperDesc = b.Field<string>("Shipper") == null ? string.Empty : (SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Shipper"))).ToString(true),
                                             ConsigneeDesc = b.Field<string>("Consignee") == null ? string.Empty : (SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Consignee"))).ToString(true),
                                             NotifyPartyDesc = b.Field<string>("NotifyParty") == null ? string.Empty : (SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("NotifyParty"))).ToString(true),
                                             SellerDesc = b.Field<string>("Seller") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Seller")).ToString(true),
                                             BuyerDesc = b.Field<string>("Buyer") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Buyer")).ToString(true),
                                             ShipToDesc = b.Field<string>("ShipTo") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("ShipTo")).ToString(true),
                                             ManufacturerDesc = b.Field<string>("Manufacturer") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Manufacturer")).ToString(true),
                                             StuffingLocationDesc = b.Field<string>("StuffingLocation") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("StuffingLocation")).ToString(true),
                                             ConsolidatorDesc = b.Field<string>("Consolidator") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Consolidator")).ToString(true),
                                             BookingPartyInfoDesc = b.Field<string>("BookingPartyInfo") == null ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("BookingPartyInfo")).ToString(true),
                                             ISFImporterIDType = b.IsNull("ISFImporterIDType") ? ImportRefType.Unknown : (ImportRefType)Enum.ToObject(typeof(ImportRefType), b.Field<byte>("ISFImporterIDType")),
                                             ISFImporterID = b.Field<string>("ISFImporterID"),
                                             ISFImporterFirstName = b.Field<string>("ISFImporterFirstName"),
                                             ISFImporterLastName = b.Field<string>("ISFImporterLastName"),
                                             ISFImporterDateOfBirth = b.Field<DateTime?>("ISFImporterDateOfBirth"),
                                             ISFImporterCountryOfIssuance = b.Field<Guid?>("ISFImporterCountryOfIssuance"),
                                             IsfImporterCountryOfIssuanceName = b.Field<string>("ISFImporterCountryOfIssuanceName"),
                                             CargoTypeForAMS = b.IsNull("CargoType") ? CargoTypeForAMS.Unknown : (CargoTypeForAMS)Enum.ToObject(typeof(CargoTypeForAMS), b.Field<byte>("CargoType")),
                                             BondReferenceType = b.IsNull("BondReferenceType") ? BondRef.Unknown : (BondRef)Enum.ToObject(typeof(BondRef), b.Field<byte>("BondReferenceType")),
                                             BondReferenceNumber = b.Field<string>("BondReferenceNumber"),
                                             BondActivityCode = b.IsNull("BondActivityCode") ? BondActivityCode.Unknown : (BondActivityCode)Enum.ToObject(typeof(BondActivityCode), b.Field<byte>("BondActivityCode")),
                                             ShipmentType = b.IsNull("ShipmentType") ? ShipmentType.Unknown : (ShipmentType)b.Field<byte>("ShipmentType"),
                                             ImporterOfRecordNumberQualifier = b.IsNull("ImporterOfRecordNumberQualifier") ? ConsigneeAndBuyerType.Unknown : (ConsigneeAndBuyerType)Enum.ToObject(typeof(ConsigneeAndBuyerType), b.Field<byte>("ImporterOfRecordNumberQualifier")),
                                             ImporterOfRecordNumber = b.Field<string>("ImporterOfRecordNumber"),
                                             ImporterOfRecordFirstName = b.Field<string>("ImporterOfRecordFirstName"),
                                             ImporterOfRecordLastName = b.Field<string>("ImporterOfRecordLastName"),
                                             ImporterOfRecordDOB = b.Field<DateTime?>("ImporterOfRecordDOB"),
                                             ImporterOfPassportIssuanceCountry = b.Field<Guid?>("ImporterOfPassportIssuanceCountry"),
                                             ImporterOfPassportIssuanceCountryName = b.Field<string>("ImporterOfPassportIssuanceCountryName"),
                                             ConsigneeNumberQualifier = b.IsNull("ConsigneeNumberQualifier") ? ConsigneeAndBuyerType.Unknown : (ConsigneeAndBuyerType)Enum.ToObject(typeof(ConsigneeAndBuyerType), b.Field<byte>("ConsigneeNumberQualifier")),
                                             ConsigneeNumber = b.Field<string>("ConsigneeNumber"),
                                             ConsigneeFirstName = b.Field<string>("ConsigneeFirstName"),
                                             ConsigneeLastName = b.Field<string>("ConsigneeLastName"),
                                             ConsigneePassportDOB = b.Field<DateTime?>("ConsigneePassportDOB"),
                                             ConsigneePassportIssuanceCountry = b.Field<Guid?>("ConsigneePassportIssuanceCountry"),
                                             ConsigneePassportIssuanceCountryName = b.Field<string>("ConsigneePassportIssuanceCountryName"),
                                             Container = GetContainerListByXMLStr(b.Field<string>("Containers")),
                                             ContainerDetails = GetContainerDetailsListByXMLStr(b.Field<string>("ContainerDetails")),
                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                             UpdateBy = b.Field<Guid?>("UpdateBy"),
                                             CreateDate = b.Field<DateTime>("CreateDate"),
                                             CreateBy = b.Field<Guid>("CreateBy"),
                                             IsDirty = false,
                                             LastPortOfCall = b.Field<string>("LastPortOfCall"),
                                             FirstPorOtfCall = b.Field<string>("FirstPortOfCall"),
                                             PortOfLoading = b.Field<string>("PortOfLoading"),
                                             Etd = b.Field<DateTime?>("ETD"),
                                             FirstPortOfCallDate = b.Field<DateTime?>("FirstPortOfCallDate")
                                         }).ToList();
            return results;
        }

        /// <summary>
        /// 根据HBLID查询箱号封条号信息
        /// </summary>
        /// <param name="hblid"></param>
        /// <returns></returns>
        public List<ContainerForAMS> GetContainerNumByHBLID(Guid hblid)
        {
            ArgumentHelper.AssertGuidNotEmpty(hblid, "hblid");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetContainerByHBLID]");

                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, hblid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ContainerForAMS> results = BulidContainerByDataSet(ds);

                return results;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ContainerForAMS> BulidContainerByDataSet(DataSet ds)
        {
            List<ContainerForAMS> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new ContainerForAMS
                                                {
                                                    ContainerNumber = b.Field<string>("ContainerNumber"),
                                                    FreeFormDescription = b.Field<string>("FreeFormDescription"),
                                                    Kilos = b.Field<decimal>("Kilos").ToString(),
                                                    Quantity = b.Field<int>("Quantity").ToString(),
                                                    Seal = b.Field<string>("Seal"),
                                                    UnitOfMeasure = b.Field<string>("UnitOfMeasure"),
                                                    IsDirty = false
                                                }).ToList();
            return results;
        }
        /// <summary>
        /// GetXMLList
        /// </summary>
        /// <param name="xmlStr">XML字符串</param>
        /// <returns>List</returns>
        private List<ContainerForAMS> GetContainerListByXMLStr(string xmlStr)
        {
            List<ContainerForAMS> containers = null;
            if (!string.IsNullOrEmpty(xmlStr))
            {
                XmlDocument xmlDoc = new XmlDocument();
                byte[] bs = Encoding.UTF8.GetBytes(xmlStr);
                MemoryStream ms = new MemoryStream(bs);
                xmlDoc.Load(ms); //加载XML文档   
                XmlNodeList list = xmlDoc.SelectSingleNode("Containers").ChildNodes;
                //Console.WriteLine(list.Count);
                if (list != null && list.Count > 0)
                {
                    containers = new List<ContainerForAMS>();
                    foreach (XmlNode node in list)
                    {
                        ContainerForAMS container = new ContainerForAMS();
                        container.ContainerNumber = (node["ContainerNumber"]).InnerText;
                        container.FreeFormDescription = (node["FreeFormDescription"]).InnerText;
                        container.Kilos = (node["Kilos"]).InnerText;
                        container.Quantity = (node["Quantity"]).InnerText;
                        container.Seal = (node["Seal"]).InnerText;
                        if (node["UnitOfMeasure"] != null)
                            container.UnitOfMeasure = (node["UnitOfMeasure"]).InnerText;

                        containers.Add(container);
                    }
                }
            }
            return containers;
        }

        private List<ContainerDetailsForAMS> GetContainerDetailsListByXMLStr(string xmlStr)
        {
            List<ContainerDetailsForAMS> containers = null;
            if (!string.IsNullOrEmpty(xmlStr))
            {
                XmlDocument xmlDoc = new XmlDocument();
                byte[] bs = Encoding.UTF8.GetBytes(xmlStr);
                MemoryStream ms = new MemoryStream(bs);
                xmlDoc.Load(ms); //加载XML文档   
                XmlNodeList list = xmlDoc.SelectSingleNode("ContainerDetails").ChildNodes;
                //Console.WriteLine(list.Count);
                if (list != null && list.Count > 0)
                {
                    containers = new List<ContainerDetailsForAMS>();
                    foreach (XmlNode node in list)
                    {
                        ContainerDetailsForAMS containerDetail = new ContainerDetailsForAMS();
                        containerDetail.HarmonizedTariffCode = (node["HarmonizedTariffCode"]).InnerText;
                        containerDetail.CountryOfOrigin = new Guid((node["CountryOfOrigin"]).InnerText);
                        containerDetail.CountryName = (node["CountryName"]).InnerText;
                        containers.Add(containerDetail);
                    }
                }
            }
            return containers;
        }

        /// <summary>
        /// 客户名称获取列表
        /// </summary>
        /// <param name="ShipperName"></param>
        /// <param name="ConsigneeName"></param>
        /// <param name="SellerName"></param>
        /// <param name="BuyerName"></param>
        /// <param name="ManufacturerName"></param>
        /// <param name="StuffingName"></param>
        /// <param name="ConsolidatorName"></param>
        /// <param name="ShipToName"></param>
        /// <param name="BookingPartyName"></param>
        /// <returns></returns>
        public List<OceanHBL2AmsAciIsf> GetAmsListByCustomerNames(string ShipperName, string ConsigneeName, string SellerName, string BuyerName, string ManufacturerName, string StuffingName, string ConsolidatorName, string ShipToName, string BookingPartyName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetAmsCustomerNames]");
                db.AddInParameter(dbCommand, "@ShipperName", DbType.String, ShipperName);
                db.AddInParameter(dbCommand, "@ConsigneeName", DbType.String, ConsigneeName);
                db.AddInParameter(dbCommand, "@SellerName", DbType.String, SellerName);
                db.AddInParameter(dbCommand, "@BuyerName", DbType.String, BuyerName);
                db.AddInParameter(dbCommand, "@ManufacturerName", DbType.String, ManufacturerName);
                db.AddInParameter(dbCommand, "@StuffingName", DbType.String, StuffingName);
                db.AddInParameter(dbCommand, "@ConsolidatorName", DbType.String, ConsolidatorName);
                db.AddInParameter(dbCommand, "@ShipToName", DbType.String, ShipToName);
                db.AddInParameter(dbCommand, "@BookingPartyName", DbType.String, BookingPartyName);

                db.ExecuteDataSet(dbCommand);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanHBL2AmsAciIsf> AmsList = (from b in ds.Tables[0].AsEnumerable()
                                                select new OceanHBL2AmsAciIsf
                                         {
                                             Shipper = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Shipper")),
                                             Consignee = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Consignee")),
                                             NotifyParty = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("NotifyParty")),
                                             Seller = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Seller")),
                                             Buyer = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Buyer")),
                                             ShipTo = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("ShipTo")),
                                             Manufacturer = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Manufacturer")),
                                             StuffingLocation = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("StuffingLocation")),
                                             Consolidator = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("Consolidator")),
                                             BookingPartyInfo = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("BookingPartyInfo")),
                                         }).ToList();
                return AmsList;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
