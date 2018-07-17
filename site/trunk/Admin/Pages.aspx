<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Pages.aspx.cs" Inherits="kpfw.Admin.Pages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="interior cf">
        <div class="inner">
            <asp:PlaceHolder runat="server" ID="ListPnl">
                <h1>Page Management</h1>
                <asp:Button runat="server" ID="btnAddPage" Text="Add Page" OnClick="btnAddPage_Click" />
                <div class="chart">
                    <div class="chart-wrap">
                        <asp:GridView runat="server" ID="grdPages" AutoGenerateColumns="false" OnSelectedIndexChanged="grdPages_SelectedIndexChanged" DataKeyNames="Id">
                            <HeaderStyle CssClass="head" />
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:HyperLinkField HeaderText="URL" DataNavigateUrlFields="Path" DataNavigateUrlFormatString="/{0}" DataTextField="Path" Target="_blank" />
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" ControlStyle-CssClass="btn" SelectText="Select" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Button runat="server" Text="Add Page" OnClick="btnAddPage_Click" />
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="ItemPnl" Visible="false">
                <h1>Add/Edit Page</h1>
                <asp:ValidationSummary ValidationGroup="Page" runat="server" ShowMessageBox="true" ShowSummary="true" DisplayMode="BulletList" CssClass="error" />
                <div class="item cf">
                    <div class="fifteeni r">
                        <label>Page Name: <span class="req">*
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ValidationGroup="Page" ErrorMessage="Page Name is required" Text="Required" Display="Dynamic" /></span></label>
                    </div>
                    <div class="halfi">
                        <asp:TextBox runat="server" ID="txtName" />
                    </div>
                </div>
                <div class="item cf">
                    <div class="fifteeni r">
                        <label>URL: <span class="req">*
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUrl" ValidationGroup="Page" ErrorMessage="Page URL is required" Text="Required" Display="Dynamic" /></span></label>
                    </div>
                    <div class="halfi">
                        <asp:TextBox runat="server" ID="txtUrl" />
                    </div>
                </div>
                <div class="item cf">
                    <div class="fifteeni r">
                        <label>Content:</label>
                    </div>
                    <div class="eightyfivei mdEditor">
                        <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" ClientIDMode="Static" ValidateRequestMode="Disabled" />
                    </div>
                </div>
                <div class="actions cf">
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" ValidationGroup="Page" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" Runat="Server">
    <script src="/js/simplemde/simplemde.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/js/simplemde/simplemde.min.css" type="text/css" />
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function (event) {
            var simplemde = new SimpleMDE({ element: document.getElementById("txtContent") });
        });
    </script>
    <style>
        .mdEditor{
            background-color: #eee;
        }
    </style>
</asp:Content>

