//
// System.Web.Configuration.RootProfilePropertySettingsCollection
//
// Authors:
//	Chris Toshok (toshok@ximian.com)
//
// (C) 2005 Novell, Inc (http://www.novell.com)
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if NET_2_0

using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	public sealed class RootProfilePropertySettingsCollection : ProfilePropertySettingsCollection
	{
		protected override ConfigurationElement CreateNewElement ()
		{
			return new ProfilePropertySettings ();
		}

		protected override Object GetElementKey (ConfigurationElement element)
		{
			return ((ProfilePropertySettings) element).Name;
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}
		
		public override bool Equals (object obj)
		{
			RootProfilePropertySettingsCollection col = obj as RootProfilePropertySettingsCollection;
			if (col == null)
				return false;

			if (GetType () != col.GetType ())
				return false;

			if (Count != col.Count)
				return false;

			for (int n = 0; n < Count; n++) {
				if (!BaseGet (n).Equals (col.BaseGet (n)))
					return false;
			}
			return true;
		}

		public override int GetHashCode ()
		{
			int code = 0;
			for (int n = 0; n < Count; n++)
				code += BaseGet (n).GetHashCode ();
			return code;
		}

		[ConfigurationProperty ("", IsDefaultCollection = true)]
		[ConfigurationCollection (typeof (ProfileGroupSettingsCollection), AddItemName = "group")]
		public ProfileGroupSettingsCollection GroupSettings {
			get { return (ProfileGroupSettingsCollection) base [""]; }
		}

		protected override void Reset (ConfigurationElement parentElement)
		{
			base.Reset (parentElement);

			RootProfilePropertySettingsCollection root = (RootProfilePropertySettingsCollection) parentElement;
			if (root == null)
				return;

			GroupSettings.ResetInternal (root.GroupSettings);
		}
	}
}

#endif
