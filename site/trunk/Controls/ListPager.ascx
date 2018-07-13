<%@ Control Language="c#" AutoEventWireup="True" CodeFile="ListPager.ascx.cs" Inherits="kpfw.ListPager" %>

<div runat="server" id="Content">
	<div class="pagerLabel">Page:</div>
	<div class="pageNumbers">
		<asp:Repeater runat="server" ID="Pages" OnItemCommand="Pages_ItemCommand">
			<ItemTemplate>
				<asp:Label runat="server" CssClass="active" Text='<%# Eval("Text") %>' Visible='<%# Eval("Active") %>' />
				<asp:LinkButton runat="server" Text='<%# Eval("Text") %>' CommandName='Page' CommandArgument='<%# Eval("Number") %>' Visible='<%# Eval("NotActive") %>' />
			</ItemTemplate>
		</asp:Repeater>
	</div>
</div>
