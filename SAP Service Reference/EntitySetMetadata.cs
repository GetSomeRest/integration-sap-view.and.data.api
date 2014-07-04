using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdnWebAPI
{
	/// <summary>
    /// The class provides the placeholders for the odata entity metadata
    /// </summary>
    public class EntitySetMetadata
    {
	    /// <summary>
        /// this parameter determines if the odata entity is creatable
		/// </summary>
        internal bool Creatable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity is updatable
		/// </summary>
        internal bool Updatable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity is deletable
		/// </summary>
        internal bool Deletable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity is queryable
		/// </summary>
        internal bool Queryable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity is searchable
		/// </summary>
        internal bool Searchable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity requires filter to be queried
		/// </summary>
        internal bool RequiresFilter { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity is enabled for paging
		/// </summary>
        internal bool Pageable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity can be subscribed
		/// </summary>
        internal bool Subscribable { get; set; }

		/// <summary>
        /// this parameter determines if the odata entity is addressable
		/// </summary>
        internal bool Addressable { get; set; }
    }
}
