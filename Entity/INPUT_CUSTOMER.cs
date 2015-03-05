using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Unvired.Kernel.Model;
using Unvired.Kernel.Database;
using System.Xml.Linq;
using System.Reflection;
#if UNVIRED_WIN8
using Unvired.Kernel.SQLite;
#endif

namespace Entity
{
	public class INPUT_CUSTOMER {
		private List< object>  ITEM_LIST = new List< object> ();
		
		
		public string MAX_HITS
	  	{
			get;
			set;
	    }

		public string CUSTOMER_NUMBER
	  	{
			get;
			set;
	    }

		public string MC_NAME
	  	{
			get;
			set;
	    }

		
		public void AddItem(object value) {
			ITEM_LIST.Add(value);
	    }
	
		public void RemoveItem(object value) {
		     ITEM_LIST.Remove(value);
		}
		
	    public void RemoveAllItem() {
	        ITEM_LIST.Clear();
	    }
		
		public string GetXML() {
		    XDocument xmlDocument = new XDocument();
		    XElement root = new XElement("INPUT_CUSTOMER");
		    IEnumerable<PropertyInfo>  propInfos = this.GetType().GetRuntimeProperties();
			foreach (var prop in propInfos)
			{
				string value = (prop.GetValue(this) == null) ? "" : prop.GetValue(this).ToString();
				root.Add(new XElement(prop.Name, value));
			}
		    xmlDocument.Add(root);
		    
		    XElement itemElement = null;
		    foreach(var item in ITEM_LIST) {
				itemElement = new XElement("item",new XAttribute("name", item.GetType().Name));
				propInfos = item.GetType().GetRuntimeProperties();
				foreach(var prop in propInfos) {
				    string value = (prop.GetValue(item) == null) ? "" : prop.GetValue(item).ToString();
				    itemElement.Add(new XElement(prop.Name, value));
				}
				root.Add(itemElement);
		     }
		    return xmlDocument.ToString();
		}
	}
}

