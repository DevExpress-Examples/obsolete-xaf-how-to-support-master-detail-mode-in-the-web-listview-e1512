using System;
using System.Web.UI;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace WebSolution.Module.Web {
    public class GridMasterDetailViewController : ViewController<ListView> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Model.MasterDetailMode == MasterDetailMode.ListViewAndDetailView) {
                ASPxGridListEditor listEditor = View.Editor as ASPxGridListEditor;
                if (listEditor != null) {
                    listEditor.Grid.SettingsDetail.ShowDetailRow = true;
                    listEditor.Grid.Templates.DetailRow = new ASPxGridViewDetailRowTemplate(View);
                }
            }
        }
        class ASPxGridViewDetailRowTemplate : ITemplate {
            private ListView masterListViewCore;
            public ASPxGridViewDetailRowTemplate(ListView masterListView) {
                masterListViewCore = masterListView;
            }
            public void InstantiateIn(Control container) {
                GridViewDetailRowTemplateContainer templateContainer = (GridViewDetailRowTemplateContainer)container;
                ASPxPageControl pageControl = RenderHelper.CreateASPxPageControl();
                object masterObject = masterListViewCore.ObjectSpace.GetObject(templateContainer.Grid.GetRow(templateContainer.VisibleIndex));
                pageControl.EnableCallBacks = true;
                pageControl.Width = Unit.Percentage(100);
                pageControl.ContentStyle.Paddings.Padding = Unit.Pixel(0);
                container.Controls.Add(pageControl);
                foreach (IMemberInfo mi in masterListViewCore.ObjectTypeInfo.Members) {
                    if (mi.IsList && mi.IsPublic) {
                        IObjectSpace os = WebApplication.Instance.CreateObjectSpace();
                        string listViewId = DevExpress.ExpressApp.Model.NodeGenerators.ModelNestedListViewNodesGeneratorHelper.GetNestedListViewId(mi);
                        CollectionSourceBase cs = new PropertyCollectionSource(os, mi.ListElementType, os.GetObject(masterObject), mi, CollectionSourceMode.Proxy);
                        ListView detailsListView = WebApplication.Instance.CreateListView(listViewId, cs, false);
                        
                        Frame detailsFrame = WebApplication.Instance.CreateFrame(TemplateContext.NestedFrame);
                        detailsFrame.SetView(detailsListView);
                        detailsFrame.CreateTemplate();

                        Control detailsTemplateControl = (Control)detailsFrame.Template;
                        detailsTemplateControl.ID = string.Format("detailsTemplateControl_{0}", mi.Name);
                        TabPage page = new TabPage(mi.DisplayName);
                        page.Controls.Add(detailsTemplateControl);
                        pageControl.TabPages.Add(page);
                    }
                }
            }
        }
    }
}