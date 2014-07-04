using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdnWebAPI;


namespace AdnWebAPI.ZGWSAMPLE_SRV
{

	#region ZGWSAMPLE_SRV class

    ///<summary>
	///This is the extension class to the generated proxy which contains oData4SAP semantics information like labels, filter values as maintained in the service in SAP NetWeaver Gateway system.
	///</summary>
    public partial class ZGWSAMPLE_SRV
    {
	static Dictionary<string, EntitySetMetadata> sapEntitySetAttributes = new Dictionary<string, EntitySetMetadata>() {
					
					{"SalesOrderLineItemCollection", new EntitySetMetadata() { Creatable = true, Updatable = true,Deletable=  true,Queryable = true,Addressable=false,Pageable=   true,RequiresFilter=false,Searchable =true,Subscribable = false}},
					
					{"BusinessPartnerCollection", new EntitySetMetadata() { Creatable = true, Updatable = true,Deletable=  true,Queryable = true,Addressable=true,Pageable=   true,RequiresFilter=false,Searchable =true,Subscribable = false}},
					
					{"ContactCollection", new EntitySetMetadata() { Creatable = true, Updatable = true,Deletable=  true,Queryable = true,Addressable=false,Pageable=   true,RequiresFilter=false,Searchable =true,Subscribable = false}},
					
					{"SalesOrderCollection", new EntitySetMetadata() { Creatable = true, Updatable = true,Deletable=  true,Queryable = true,Addressable=true,Pageable=   true,RequiresFilter=false,Searchable =true,Subscribable = false}},
					
					{"ProductCollection", new EntitySetMetadata() { Creatable = true, Updatable = true,Deletable=  true,Queryable = true,Addressable=true,Pageable=   true,RequiresFilter=false,Searchable =true,Subscribable = false}}
					
					};

        public static Dictionary<string, EntitySetMetadata> SAPEntitySetAttributes
        {
            get
            {
                return sapEntitySetAttributes;
            }
        }
    }

	#endregion

	

	#region SalesOrderLineItem class

    public partial class SalesOrderLineItem
    {
		static Dictionary<string, PropertyMetadata> sapPropertyAttributes = new Dictionary<string, PropertyMetadata>() { 
					
					{"SoId", new PropertyMetadata() {  Label = "Sa. Ord. ID",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"SoItemPos", new PropertyMetadata() {  Label = "PO Item Pos",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ProductId", new PropertyMetadata() {  Label = "Product ID",Creatable = true,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Note", new PropertyMetadata() {  Label = "Description",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CurrencyCode", new PropertyMetadata() {  Label = "Currency",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"GrossAmount", new PropertyMetadata() {  Label = "Gross Amt.",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"GrossAmountExt", new PropertyMetadata() {  Label = "Amount",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"NetAmount", new PropertyMetadata() {  Label = "Net Amt.",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"NetAmountExt", new PropertyMetadata() {  Label = "Amount",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"TaxAmount", new PropertyMetadata() {  Label = "Tax Amt.",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"TaxAmountExt", new PropertyMetadata() {  Label = "Amount",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"DeliveryDate", new PropertyMetadata() {  Label = "Time Stamp",Creatable = true,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Quantity", new PropertyMetadata() {  Label = "Quantity",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"QuantityUnit", new PropertyMetadata() {  Label = "Qty. Unit",Creatable = true,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}}
					
				};
		
        public static Dictionary<string, PropertyMetadata> SAPPropertyAttributes
        {
            get
            {
                return sapPropertyAttributes;
            }
        }

	}

	#endregion
	
	

	#region BusinessPartner class

    public partial class BusinessPartner
    {
		static Dictionary<string, PropertyMetadata> sapPropertyAttributes = new Dictionary<string, PropertyMetadata>() { 
					
					{"BusinessPartnerID", new PropertyMetadata() {  Label = "Bus. Part. ID",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=true,ValueHelpCollectionName = "ContactCollection",ValueHelpCollectionKeyField="BusinessPartnerID",ValueHelpEntityNameWithNS="ZGWSAMPLE_SRV.Contact"}},
					
					{"BpRole", new PropertyMetadata() {  Label = "Bus. Part. Role",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"EmailAddress", new PropertyMetadata() {  Label = "E-Mail Address",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"PhoneNumber", new PropertyMetadata() {  Label = "Phone No.",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"FaxNumber", new PropertyMetadata() {  Label = "Phone No.",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"WebAddress", new PropertyMetadata() {  Label = "Description",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CompanyName", new PropertyMetadata() {  Label = "Company Name",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"LegalForm", new PropertyMetadata() {  Label = "Legal Form",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CurrencyCode", new PropertyMetadata() {  Label = "Currency",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"City", new PropertyMetadata() {  Label = "City",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"PostalCode", new PropertyMetadata() {  Label = "Postal Code",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Street", new PropertyMetadata() {  Label = "Street",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Building", new PropertyMetadata() {  Label = "Building",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Country", new PropertyMetadata() {  Label = "Country",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"AddressType", new PropertyMetadata() {  Label = "Address Type",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"AddressValStartDate", new PropertyMetadata() {  Label = "Time Stamp",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"AddressValEndDate", new PropertyMetadata() {  Label = "Time Stamp",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CreatedBy", new PropertyMetadata() {  Label = "Employee ID",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CreatedAt", new PropertyMetadata() {  Label = "Time Stamp",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ChangedBy", new PropertyMetadata() {  Label = "Employee ID",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ChangedAt", new PropertyMetadata() {  Label = "Time Stamp",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}}
					
				};
		
        public static Dictionary<string, PropertyMetadata> SAPPropertyAttributes
        {
            get
            {
                return sapPropertyAttributes;
            }
        }

	}

	#endregion
	
	

	#region Contact class

    public partial class Contact
    {
		static Dictionary<string, PropertyMetadata> sapPropertyAttributes = new Dictionary<string, PropertyMetadata>() { 
					
					{"AddressValEndDate", new PropertyMetadata() {  Label = "Time Stamp",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"AddressValStartDate", new PropertyMetadata() {  Label = "Time Stamp",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"AddressType", new PropertyMetadata() {  Label = "Address Type",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Country", new PropertyMetadata() {  Label = "Country",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Building", new PropertyMetadata() {  Label = "Building",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Street", new PropertyMetadata() {  Label = "Street",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"City", new PropertyMetadata() {  Label = "City",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"PostalCode", new PropertyMetadata() {  Label = "Postal Code",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"BusinessPartnerID", new PropertyMetadata() {  Label = "Bus. Part. ID",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Title", new PropertyMetadata() {  Label = "Title",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"FirstName", new PropertyMetadata() {  Label = "First Name",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Language", new PropertyMetadata() {  Label = "Language",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"MiddleName", new PropertyMetadata() {  Label = "Middle Name",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"LastName", new PropertyMetadata() {  Label = "Last Name",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Nickname", new PropertyMetadata() {  Label = "Nickname",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"EmailAddress", new PropertyMetadata() {  Label = "E-Mail Address",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Initials", new PropertyMetadata() {  Label = "Initials",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Sex", new PropertyMetadata() {  Label = "Sex",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"PhoneNumber", new PropertyMetadata() {  Label = "Phone No.",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"FaxNumber", new PropertyMetadata() {  Label = "Phone No.",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}}
					
				};
		
        public static Dictionary<string, PropertyMetadata> SAPPropertyAttributes
        {
            get
            {
                return sapPropertyAttributes;
            }
        }

	}

	#endregion
	
	

	#region SalesOrder class

    public partial class SalesOrder
    {
		static Dictionary<string, PropertyMetadata> sapPropertyAttributes = new Dictionary<string, PropertyMetadata>() { 
					
					{"LifecycleStatus", new PropertyMetadata() {  Label = "PO Lifecycle",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"TaxAmountExt", new PropertyMetadata() {  Label = "Amount",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"TaxAmount", new PropertyMetadata() {  Label = "Tax Amt.",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"NetAmountExt", new PropertyMetadata() {  Label = "Amount",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"NetAmount", new PropertyMetadata() {  Label = "Net Amt.",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"GrossAmountExt", new PropertyMetadata() {  Label = "Amount",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"GrossAmount", new PropertyMetadata() {  Label = "Gross Amt.",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CurrencyCode", new PropertyMetadata() {  Label = "Currency",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"BuyerName", new PropertyMetadata() {  Label = "Company Name",Creatable = true,Updatable=false,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"BuyerId", new PropertyMetadata() {  Label = "Bus. Part. ID",Creatable = true,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Note", new PropertyMetadata() {  Label = "Description",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ChangedByBp", new PropertyMetadata() {  Label = "Value",Creatable = true,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CreatedByBp", new PropertyMetadata() {  Label = "Value",Creatable = true,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ChangedAt", new PropertyMetadata() {  Label = "Time Stamp",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ChangedBy", new PropertyMetadata() {  Label = "Employee ID",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CreatedAt", new PropertyMetadata() {  Label = "Time Stamp",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CreatedBy", new PropertyMetadata() {  Label = "Employee ID",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"SoId", new PropertyMetadata() {  Label = "Sa. Ord. ID",Creatable = false,Updatable=false,Mandatory=false,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=true,ValueHelpCollectionName = "SalesOrderLineItemCollection",ValueHelpCollectionKeyField="SoId",ValueHelpEntityNameWithNS="ZGWSAMPLE_SRV.SalesOrderLineItem"}},
					
					{"BillingStatus", new PropertyMetadata() {  Label = "PO Confirmation",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"DeliveryStatus", new PropertyMetadata() {  Label = "PO Ordering",Creatable = false,Updatable=false,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}}
					
				};
		
        public static Dictionary<string, PropertyMetadata> SAPPropertyAttributes
        {
            get
            {
                return sapPropertyAttributes;
            }
        }

	}

	#endregion
	
	

	#region Product class

    public partial class Product
    {
		static Dictionary<string, PropertyMetadata> sapPropertyAttributes = new Dictionary<string, PropertyMetadata>() { 
					
					{"ProductId", new PropertyMetadata() {  Label = "Product ID",Creatable = true,Updatable=false,Mandatory=true,Filterable=true,IsKey=true,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"TypeCode", new PropertyMetadata() {  Label = "Prod. Type Code",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Category", new PropertyMetadata() {  Label = "Prod. Cat.",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Name", new PropertyMetadata() {  Label = "Description",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Description", new PropertyMetadata() {  Label = "Description",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"SupplierId", new PropertyMetadata() {  Label = "Bus. Part. ID",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"SupplierName", new PropertyMetadata() {  Label = "Company Name",Creatable = true,Updatable=true,Mandatory=false,Filterable=true,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"TaxTarifCode", new PropertyMetadata() {  Label = "Prod. Tax Code",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"MeasureUnit", new PropertyMetadata() {  Label = "Qty. Unit",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"WeightMeasure", new PropertyMetadata() {  Label = "Wt. Measure",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"WeightUnit", new PropertyMetadata() {  Label = "Qty. Unit",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Price", new PropertyMetadata() {  Label = "Price",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"CurrencyCode", new PropertyMetadata() {  Label = "Currency",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Width", new PropertyMetadata() {  Label = "Dimensions",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Depth", new PropertyMetadata() {  Label = "Dimensions",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"Height", new PropertyMetadata() {  Label = "Dimensions",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"DimUnit", new PropertyMetadata() {  Label = "Dim. Unit",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}},
					
					{"ProductPicUrl", new PropertyMetadata() {  Label = "Pic URL",Creatable = true,Updatable=true,Mandatory=false,Filterable=false,IsKey=false,Sortable=true,FilterRestriction="",IsValueHelpPresent=false,ValueHelpCollectionName = "",ValueHelpCollectionKeyField="",ValueHelpEntityNameWithNS=""}}
					
				};
		
        public static Dictionary<string, PropertyMetadata> SAPPropertyAttributes
        {
            get
            {
                return sapPropertyAttributes;
            }
        }

	}

	#endregion
	
	

}
