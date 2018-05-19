Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports System.Web.UI.WebControls
Imports DevExpress.ExpressApp.Web
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxTabControl
Imports DevExpress.ExpressApp.Web.Editors.ASPx

Namespace WebSolution.Module.Web
	Public Class GridMasterDetailViewController
		Inherits ViewController(Of ListView)
		Protected Overrides Overloads Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			If View.Model.MasterDetailMode = MasterDetailMode.ListViewAndDetailView Then
				Dim listEditor As ASPxGridListEditor = TryCast(View.Editor, ASPxGridListEditor)
				If listEditor IsNot Nothing Then
					listEditor.Grid.SettingsDetail.ShowDetailRow = True
					listEditor.Grid.Templates.DetailRow = New ASPxGridViewDetailRowTemplate(View)
				End If
			End If
		End Sub
		Private Class ASPxGridViewDetailRowTemplate
			Implements ITemplate
			Private masterListViewCore As ListView
			Public Sub New(ByVal masterListView As ListView)
				masterListViewCore = masterListView
			End Sub
			Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
				Dim templateContainer As GridViewDetailRowTemplateContainer = CType(container, GridViewDetailRowTemplateContainer)
				Dim pageControl As ASPxPageControl = RenderHelper.CreateASPxPageControl()
				Dim masterObject As Object = masterListViewCore.ObjectSpace.GetObject(templateContainer.Grid.GetRow(templateContainer.VisibleIndex))
				pageControl.EnableCallBacks = True
				pageControl.Width = Unit.Percentage(100)
				pageControl.ContentStyle.Paddings.Padding = Unit.Pixel(0)
				container.Controls.Add(pageControl)
				For Each mi As IMemberInfo In masterListViewCore.ObjectTypeInfo.Members
					If mi.IsList AndAlso mi.IsPublic Then
						Dim os As IObjectSpace = WebApplication.Instance.CreateObjectSpace()
						Dim listViewId As String = DevExpress.ExpressApp.Model.NodeGenerators.ModelNestedListViewNodesGeneratorHelper.GetNestedListViewId(mi)
						Dim cs As CollectionSourceBase = New PropertyCollectionSource(os, mi.ListElementType, os.GetObject(masterObject), mi, CollectionSourceMode.Proxy)
						Dim detailsListView As ListView = WebApplication.Instance.CreateListView(listViewId, cs, False)

						Dim detailsFrame As Frame = WebApplication.Instance.CreateFrame(TemplateContext.NestedFrame)
						detailsFrame.SetView(detailsListView)
						detailsFrame.CreateTemplate()

						Dim detailsTemplateControl As Control = CType(detailsFrame.Template, Control)
						detailsTemplateControl.ID = String.Format("detailsTemplateControl_{0}", mi.Name)
						Dim page As New TabPage(mi.DisplayName)
						page.Controls.Add(detailsTemplateControl)
						pageControl.TabPages.Add(page)
					End If
				Next mi
			End Sub
		End Class
	End Class
End Namespace