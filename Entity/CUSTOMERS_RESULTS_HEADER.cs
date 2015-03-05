using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Unvired.Kernel.Model;
using Unvired.Kernel.Database;
#if UNVIRED_WIN8
using Unvired.Kernel.SQLite;
#endif

namespace Entity {
	/*
	This class represents Business Entity Header for 
	*/	
	public class CUSTOMERS_RESULTS_HEADER : DataStructure {
		
		
		// Addresses: Address type
		private string _ADDR_TYPE;

		// Customer Number 1
		private string _KUNNR;

		// Number of contact person
		private int? _PARNR;

		// Name 1
		private string _NAME;

		// First name
		private string _FIRSTNAME;

		// Street
		private string _STREET;

		// City
		private string _CITY1;

		// Name (field NAME1) in upper case for matchcode
		private string _MC_NAME;

		// First name in upper case for search help
		private string _MC_FIRSTNAME;

		// Search Term 1
		private string _SORT1;

		// Search Term 2
		private string _SORT2;

		// City name in upper case for search help
		private string _MC_CITY1;

		// City postal code
		private string _POST_CODE1;

		// Street name in upper case for search help
		private string _MC_STREET;

		// House Number
		private string _HOUSE_NUM1;

		// Country Key
		private string _COUNTRY;

		// Region (State, Province, County)
		private string _REGION;

		// Last name
		private string _NAME_L_LNG;

		// First name
		private string _NAME_F_LNG;

		// City
		private string _CITY1_LNG;

		// Street
		private string _STREET_LNG;

		// PO Box postal code
		private string _POST_CODE2;

		// PO Box
		private string _PO_BOX;

	
		internal static bool IsHeader {
			get {
			    return true;
			}
	    }
		
		public string ADDR_TYPE { 
			get {
				return _ADDR_TYPE;
			}
			set {
				_ADDR_TYPE = value;
				RaisePropertyChanged("ADDR_TYPE");
			}
		}

		[Unique]
	
		public string KUNNR { 
			get {
				return _KUNNR;
			}
			set {
				_KUNNR = value;
				RaisePropertyChanged("KUNNR");
			}
		}

		public int? PARNR { 
			get {
				return _PARNR;
			}
			set {
				_PARNR = value;
				RaisePropertyChanged("PARNR");
			}
		}

		public string NAME { 
			get {
				return _NAME;
			}
			set {
				_NAME = value;
				RaisePropertyChanged("NAME");
			}
		}

		public string FIRSTNAME { 
			get {
				return _FIRSTNAME;
			}
			set {
				_FIRSTNAME = value;
				RaisePropertyChanged("FIRSTNAME");
			}
		}

		public string STREET { 
			get {
				return _STREET;
			}
			set {
				_STREET = value;
				RaisePropertyChanged("STREET");
			}
		}

		public string CITY1 { 
			get {
				return _CITY1;
			}
			set {
				_CITY1 = value;
				RaisePropertyChanged("CITY1");
			}
		}

		public string MC_NAME { 
			get {
				return _MC_NAME;
			}
			set {
				_MC_NAME = value;
				RaisePropertyChanged("MC_NAME");
			}
		}

		public string MC_FIRSTNAME { 
			get {
				return _MC_FIRSTNAME;
			}
			set {
				_MC_FIRSTNAME = value;
				RaisePropertyChanged("MC_FIRSTNAME");
			}
		}

		public string SORT1 { 
			get {
				return _SORT1;
			}
			set {
				_SORT1 = value;
				RaisePropertyChanged("SORT1");
			}
		}

		public string SORT2 { 
			get {
				return _SORT2;
			}
			set {
				_SORT2 = value;
				RaisePropertyChanged("SORT2");
			}
		}

		public string MC_CITY1 { 
			get {
				return _MC_CITY1;
			}
			set {
				_MC_CITY1 = value;
				RaisePropertyChanged("MC_CITY1");
			}
		}

		public string POST_CODE1 { 
			get {
				return _POST_CODE1;
			}
			set {
				_POST_CODE1 = value;
				RaisePropertyChanged("POST_CODE1");
			}
		}

		public string MC_STREET { 
			get {
				return _MC_STREET;
			}
			set {
				_MC_STREET = value;
				RaisePropertyChanged("MC_STREET");
			}
		}

		public string HOUSE_NUM1 { 
			get {
				return _HOUSE_NUM1;
			}
			set {
				_HOUSE_NUM1 = value;
				RaisePropertyChanged("HOUSE_NUM1");
			}
		}

		public string COUNTRY { 
			get {
				return _COUNTRY;
			}
			set {
				_COUNTRY = value;
				RaisePropertyChanged("COUNTRY");
			}
		}

		public string REGION { 
			get {
				return _REGION;
			}
			set {
				_REGION = value;
				RaisePropertyChanged("REGION");
			}
		}

		public string NAME_L_LNG { 
			get {
				return _NAME_L_LNG;
			}
			set {
				_NAME_L_LNG = value;
				RaisePropertyChanged("NAME_L_LNG");
			}
		}

		public string NAME_F_LNG { 
			get {
				return _NAME_F_LNG;
			}
			set {
				_NAME_F_LNG = value;
				RaisePropertyChanged("NAME_F_LNG");
			}
		}

		public string CITY1_LNG { 
			get {
				return _CITY1_LNG;
			}
			set {
				_CITY1_LNG = value;
				RaisePropertyChanged("CITY1_LNG");
			}
		}

		public string STREET_LNG { 
			get {
				return _STREET_LNG;
			}
			set {
				_STREET_LNG = value;
				RaisePropertyChanged("STREET_LNG");
			}
		}

		public string POST_CODE2 { 
			get {
				return _POST_CODE2;
			}
			set {
				_POST_CODE2 = value;
				RaisePropertyChanged("POST_CODE2");
			}
		}

		public string PO_BOX { 
			get {
				return _PO_BOX;
			}
			set {
				_PO_BOX = value;
				RaisePropertyChanged("PO_BOX");
			}
		}

	}
}

