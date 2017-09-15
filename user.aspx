<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Admin.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Admin_UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<script>
		$(document).ready(function () {
			$("#pnlUserList").addClass("highlight");
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="header-content clearfix">
		<h2>User List
		</h2>
		<div class="breadcrumb-wrapper hidden-xs">
			<span class="label">You are here:</span>
			<ol class="breadcrumb">
				<li>
					<i class="fa fa-home mrr"></i>
					<a href="/dashboard">Dashboard</a>
					<i class="fa fa-angle-right "></i>
				</li>
				<li class="active mll">User List</li>
			</ol>
		</div>
	</div>
	<div class="note-info alert" data-dismiss="">
		<a href="javascript:void(0)" class="collapsed note_info btn btn-danger btn-sm" data-target="#demo" data-toggle="collapse">Hide</a>
		<p><strong>Some information about this Page:</strong></p>
		<ul class="admin-message collapse in" id="demo">

			<li><b>(1) </b>Drop Down for active/inactive table data selection. </li>
			<li><b>(2)</b> Active/Inactive button for make it table data active/inactive.</li>
			<li><b>(3)</b> Export Data button for exporting all table details in excel sheet.</li>
			<li><b>(4)</b> Search Textbox for searching perticular row according First name, Last name and Mobile no.</li>

		</ul>
	</div>
	<div class="contbox">

		<div class="row">
			<div class="col-md-12">
				<div class="btn_row">
					<div class="row">
						<div class="col-md-6">
							<asp:DropDownList ID="ddlVisibility" CssClass="form-control drp_dwn inline" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVisibility_SelectedIndexChanged">
								<asp:ListItem Value="3" Selected="True">All</asp:ListItem>
								<asp:ListItem Value="0">Active</asp:ListItem>
								<asp:ListItem Value="1">Inactive</asp:ListItem>
							</asp:DropDownList>
							<div class="action">
								<span class="sall">
									<asp:CheckBox ID="cb_all" runat="server" CssClass="cbAll" />
								</span>
								<%-- <asp:LinkButton ID="btn_delete" OnClientClick="return confirm('Are you sure? You want to delete.')" CssClass="btn btn-danger btn-xs" runat="server" Text="Delete" OnClick="btn_delete_Click" />--%>
								<asp:LinkButton ID="btn_visible" runat="server" CssClass="btn btn-primary btn-xs" OnClientClick="return confirm('Are you sure? You want to Active')" Text="Active" OnClick="btn_visible_Click"></asp:LinkButton>
								<asp:LinkButton ID="btn_hide" CssClass="btn btn-info btn-xs" OnClientClick="return confirm('Are you sure? You want to Inactive.')" runat="server" Text="Inactive" OnClick="btn_hide_Click" />
								<asp:LinkButton ID="btnExport" runat="server" Text="Export Data" OnClick="btnExport_Click" CssClass="btn btn-success btn-xs"></asp:LinkButton>
							</div>
						</div>
						<div class="col-md-6">
							<div class="sch">
								<asp:TextBox ID="txtSearch" CssClass="form-control itext" runat="server" placeholder="Search By Keyword" Width="250"></asp:TextBox>
								<asp:LinkButton ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-xs" />
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

		<div class="table-responsive">
			<asp:GridView CssClass="hobtable" ID="gd" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowDeleting="gd_RowDeleting" OnPageIndexChanging="OnPaging" OnSelectedIndexChanged="gd_SelectedIndexChanged">
				<Columns>
					<%--  <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Delete" CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are you sure?')"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
					<asp:TemplateField HeaderText="Select">
						<HeaderTemplate>
							<asp:CheckBox ID="cb_all" runat="server" CssClass="cbAll" />Select All
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox ID="cb_select" runat="server" CssClass="cbSelect" /><%#Container.DataItemIndex+1 %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Name">
						<ItemTemplate>
							<asp:Label ID="lblName" runat="server" Text=' <%# Eval("FirstName")  +" "+ Eval("LastName")%>'></asp:Label>
							<asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<%--   <asp:TemplateField HeaderText="Landline No">
                        <ItemTemplate>
                            <asp:Label ID="lblLandlineNo" runat="server" Text=' <%# Eval("LandlineNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
					<asp:TemplateField HeaderText="Mobile No">
						<ItemTemplate>
							<asp:Label ID="lblMobileNo" runat="server" Text=' <%# Eval("MobileNo") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="DOB">
						<ItemTemplate>
							<asp:Label ID="lblDOB" runat="server" Text=' <%# Eval("DOB","{0: dd MMM yyyy}") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Gender">
						<ItemTemplate>
							<asp:Label ID="lblGender" runat="server" Text=' <%# Eval("Gender") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>


				</Columns>
			</asp:GridView>


		</div>
		<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
	</div>




</asp:Content>

