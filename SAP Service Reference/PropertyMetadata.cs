using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdnWebAPI
{
    /// <summary>
    /// The class provides the place holders for property metadata
    /// </summary>
    public class PropertyMetadata
    {
	    /// <summary>
        /// label of the property
		/// </summary>
        internal string Label { get; set; }

		/// <summary>
        /// this parameter determines if the property is creatable
		/// </summary>
        internal bool Creatable { get; set; }

        /// <summary>
        ///this parameter determines if the property is updatable
		/// </summary>
        internal bool Updatable { get; set; }

		/// <summary>
        ///this parameter determines if filter can be applied to the property
		/// </summary>
        internal bool Filterable { get; set; }

		/// <summary>
        ///this parameter determines if property values can be sorted
		/// </summary>
        internal bool Sortable { get; set; }

		/// <summary>
        ///this parameter determines if the property is a mandatory parameter of the entity
		/// </summary>
        internal bool Mandatory { get; set; }

		/// <summary>
        ///this parameter determines if the property's filter restriction
		/// </summary>
        internal string FilterRestriction { get; set; }

		/// <summary>
        ///this parameter determines if the property is a key parameter of the entity
		/// </summary>
        internal bool IsKey { get; set; }

		/// <summary>
        ///this parameter determines if the property has value help maintained in backend
		/// </summary>
        internal bool IsValueHelpPresent { get; set; }

		/// <summary>
        ///value help entity name of the property
		/// </summary>
        public string ValueHelpEntityNameWithNS { get; set; }

		/// <summary>
        ///collection name of the value help
		/// </summary>
		public string ValueHelpCollectionName{ get; set; }

		/// <summary>
        ///name of the field in value help collection which provides the value of the property
		/// </summary>
        public string ValueHelpCollectionKeyField { get; set; }
    }
}
