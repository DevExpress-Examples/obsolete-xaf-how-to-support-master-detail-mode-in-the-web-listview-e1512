Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace WebSolution.Module
	Public Class Child
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _name As String
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _name, value)
			End Set
		End Property
		Private _master As Master
		<Association("Master-Children")> _
		Public Property Master() As Master
			Get
				Return _master
			End Get
			Set(ByVal value As Master)
				SetPropertyValue("Master", _master, value)
			End Set
		End Property
	End Class
	<DefaultClassOptions, DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, True, NewItemRowPosition.None)> _
	Public Class Master
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _name As String
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _name, value)
			End Set
		End Property

		<Association("Master-Children"), DisplayName("Children")> _
		Public ReadOnly Property Children() As XPCollection(Of Child)
			Get
				Return GetCollection(Of Child)("Children")
			End Get
		End Property
		Private customCollection_Renamed As XPCollection(Of Child)
		<DisplayName("CustomCollection")> _
		Public ReadOnly Property CustomCollection() As XPCollection(Of Child)
			Get
				If customCollection_Renamed Is Nothing Then
					customCollection_Renamed = New XPCollection(Of Child)(Session)
				End If
				Return customCollection_Renamed
			End Get
		End Property
	End Class
End Namespace