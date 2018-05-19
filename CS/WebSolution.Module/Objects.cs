using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace WebSolution.Module {
    public class Child : BaseObject {
        public Child(Session session) : base(session) { }
        private string _name;
        public string Name {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }
        private Master _master;
        [Association("Master-Children")]
        public Master Master {
            get { return _master; }
            set { SetPropertyValue("Master", ref _master, value); }
        }
    }
    [DefaultClassOptions]
    [DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, true, NewItemRowPosition.None)]
    public class Master : BaseObject {
        public Master(Session session) : base(session) { }
        private string _name;
        public string Name {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }

        [Association("Master-Children")]
        [DisplayName("Children")]
        public XPCollection<Child> Children {
            get {
                return GetCollection<Child>("Children");
            }
        }
        private XPCollection<Child> customCollection;
        [DisplayName("CustomCollection")]
        public XPCollection<Child> CustomCollection {
            get {
                if (customCollection == null)
                    customCollection = new XPCollection<Child>(Session);
                return customCollection;
            }
        }
    }
}